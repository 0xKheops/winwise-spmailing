using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.SharePoint;
using System.Xml;
using System.Data;
using System.Web;
using System.Xml.Xsl;
using System.IO;
using System.Xml.XPath;
using Microsoft.SharePoint.Utilities;

namespace Winwise.SPMailing {
    /// <summary>
    /// Utility class for mailing generation
    /// </summary>
    class SPMailingGenerator {

        #region Fields

        private static Regex REG_DAYSOFFSET = new Regex(@"\[DAYSOFFSET\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex REG_HEADER = new Regex(@"\[HEADER\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex REG_FOOTER = new Regex(@"\[FOOTER\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex REG_CATEGORIES = new Regex(@"\[CATEGORIES\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        #endregion

        #region Utilities

        /// <summary>
        /// Creates a mailing item from a mailingDefinition item
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        public static SPListItem GenerateMailing(SPMailingContext ctx, SPListItem definition) {

            StringBuilder log = new StringBuilder();
            SPMailingHelper.BeginLog(ctx.Web, log, SPMailingHelper.GetLocalizedString(ctx.Web, "Log_BeginGeneration"));

            //Retrieves HTML template
            SPFieldLookupValue luMailingTemplate = new SPFieldLookupValue((String)definition[ctx.FieldIds.MailingTemplate]);
            if (luMailingTemplate.LookupId == 0)
                throw new Exception(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Error_NoMailingTemplate"));
            SPListItem template = ctx.MailingTemplates.GetItemById(luMailingTemplate.LookupId);
            String html = ctx.Web.GetFileAsString(template.File.Url);

            //Retrives all SharePoint principals found in associated recipients lists
            List<String> recipientContactEmails = new List<String>();
            List<SPUser> recipientUsers = new List<SPUser>();
            FillSharePointUserRecipients(ctx, definition, recipientUsers, recipientContactEmails, log);

            //Inserts the header
            String header = definition[ctx.FieldIds.Header] as String;
            html = REG_HEADER.Replace(html, header ?? String.Empty);

            //Inserts the footer
            String footer = definition[ctx.FieldIds.Footer] as String;
            html = REG_FOOTER.Replace(html, footer ?? String.Empty);

            //Inserts categories
            String htmlCategories = GetHtmlCategories(ctx, definition, recipientUsers, log);
            html = REG_CATEGORIES.Replace(html, htmlCategories ?? String.Empty);

            SPMailingHelper.EndLog(log);

            //Allows modification in case this method is used within a GET request
            ctx.Web.AllowUnsafeUpdates = true;

            //Creates the mailing item
            SPListItem mailing = ctx.Mailings.Items.Add();
            mailing[SPBuiltInFieldId.Title] = String.Format("{0} - {1}", definition.Title, DateTime.Now.ToString(ctx.Web.Locale));
            mailing[ctx.FieldIds.From_Adress] = definition[ctx.FieldIds.From_Adress];
            mailing[ctx.FieldIds.From_DisplayName] = definition[ctx.FieldIds.From_DisplayName];
            mailing[ctx.FieldIds.ReplyTo_Adress] = definition[ctx.FieldIds.ReplyTo_Adress];
            mailing[ctx.FieldIds.ReplyTo_DisplayName] = definition[ctx.FieldIds.ReplyTo_DisplayName];
            mailing[ctx.FieldIds.MailSubject] = definition[ctx.FieldIds.MailSubject];
            mailing[ctx.FieldIds.Recipients] = ConcatEmails(recipientContactEmails, recipientUsers);
            mailing[ctx.FieldIds.MailBody] = html;
            mailing[ctx.FieldIds.Sent] = false;
            mailing[ctx.FieldIds.Log] = log.ToString();
            mailing[ctx.FieldIds.MailingDefinition] = new SPFieldLookupValue(definition.ID, definition.Title);
            mailing.Update();

            return mailing;
        }

        /// <summary>
        /// Returns the list of all emails separated by semicolons
        /// </summary>
        /// <param name="recipientContactEmails"></param>
        /// <param name="recipientUsers"></param>
        /// <returns></returns>
        private static String ConcatEmails(List<String> recipientContactEmails, List<SPUser> recipientUsers) {

            List<String> emails = new List<String>();

            emails.AddRange(recipientContactEmails.Where(email => !String.IsNullOrEmpty(email)).Distinct());
            emails.AddRange(recipientUsers.Where(u => !String.IsNullOrEmpty(u.Email) && !emails.Contains(u.Email)).Select(u => u.Email).Distinct());

            return String.Join(";", emails.ToArray());

        }

        #region Generate Mailing private sub methods

        /// <summary>
        /// Returns the HTML corresponding to the categories part of a mailing
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="definition"></param>
        /// <param name="users"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private static string GetHtmlCategories(SPMailingContext ctx, SPListItem definition, List<SPUser> users, StringBuilder log) {

            SPUserToken userToken = ctx.Web.CurrentUser.UserToken;

            SPFieldUserValue luGenerationUser = new SPFieldUserValue(ctx.Web, definition[ctx.FieldIds.GenerationUser] as String);
            if (luGenerationUser != null && luGenerationUser.User != null)
                userToken = luGenerationUser.User.UserToken;

            StringBuilder htmlCategories = new StringBuilder();

            SPFieldLookupValueCollection luCategories = definition[ctx.FieldIds.Categories] as SPFieldLookupValueCollection;
            Int32 daysOffset = Convert.ToInt32(definition[ctx.FieldIds.DaysOffset]);

            if (luCategories != null)
                foreach (SPFieldLookupValue luCategory in luCategories)
                    AppendHtmlCategory(ctx, userToken, luCategory, htmlCategories, daysOffset, log, users);

            return htmlCategories.ToString();

        }

        /// <summary>
        /// Generates and appends the HTML for a specific category
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="userToken"></param>
        /// <param name="luCategory"></param>
        /// <param name="htmlCategories"></param>
        /// <param name="daysOffset"></param>
        /// <param name="log"></param>
        /// <param name="recipients"></param>
        private static void AppendHtmlCategory(SPMailingContext ctx, SPUserToken userToken, SPFieldLookupValue luCategory, StringBuilder htmlCategories, Int32 daysOffset, StringBuilder log, List<SPUser> recipients) {

            try {

                SPListItem category = ctx.Categories.GetItemById(luCategory.LookupId);

                XmlDocument xmlItems = GetItemsForCategory(ctx, userToken, category, daysOffset, log, recipients);

                if (xmlItems.DocumentElement.SelectNodes("item").Count == 0) {
                    String msg = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Info_EmptyCategory"), luCategory.LookupValue));
                    SPMailingHelper.AppendToLog(log, msg, null, SPMailingHelper.LogLevel.Information);
                    return;
                }

                //Retrieves the XSL Transformation to use for the category
                SPFieldLookupValue luCategoryTemplate = new SPFieldLookupValue((String)category[ctx.FieldIds.CategoryTemplate]);
                SPListItem categoryTemplate = ctx.CategoryTemplates.GetItemById(luCategoryTemplate.LookupId);
                String strXsl = ctx.Web.GetFileAsString(categoryTemplate.File.ServerRelativeUrl);
                XmlDocument xslDoc = new XmlDocument();
                xslDoc.LoadXml(strXsl);

                //Loads the XSL transformation
                XsltArgumentList args = new XsltArgumentList();
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xslDoc);

                IXPathNavigable navigator = xmlItems.CreateNavigator();
                using (TextWriter tw = new StringWriter(htmlCategories)) {
                    xslt.Transform(navigator, args, tw);
                }

            } catch (Exception ex) {
                String msg = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Error_CategoryGenerationError"), luCategory.LookupValue));
                SPMailingHelper.AppendToLog(log, msg, ex, SPMailingHelper.LogLevel.Error);
                return;
            }

        }

        /// <summary>
        /// Gets the XML corresponding to the query result for a category
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="userToken"></param>
        /// <param name="category"></param>
        /// <param name="retention"></param>
        /// <param name="log"></param>
        /// <param name="recipients"></param>
        /// <returns></returns>
        private static XmlDocument GetItemsForCategory(SPMailingContext ctx, SPUserToken userToken, SPListItem category, Int32 retention, StringBuilder log, List<SPUser> recipients) {

            String title = category[SPBuiltInFieldId.Title] as String;
            String label = category[ctx.FieldIds.Label] as String;
            String type = category[ctx.FieldIds.Type] as String;
            String url = category[ctx.FieldIds.Url] as String;
            String query = category[ctx.FieldIds.Query] as String;

            SPFieldLookupValue luCategoryTemplate = new SPFieldLookupValue(category[ctx.FieldIds.CategoryTemplate] as String);
            SPListItem categoryTemplate = ctx.CategoryTemplates.GetItemById(luCategoryTemplate.LookupId);
            String viewfields = categoryTemplate[ctx.FieldIds.ViewFields] as String;

            //Replaces the days offset filter by it's value
            if (!String.IsNullOrEmpty(query))
                query = REG_DAYSOFFSET.Replace(query, retention.ToString());

            XmlDocument xmlCategoryItems = new XmlDocument();
            xmlCategoryItems.LoadXml(@"<items />");
            xmlCategoryItems.DocumentElement.SetAttribute("CategoryTitle", title);
            xmlCategoryItems.DocumentElement.SetAttribute("CategoryLabel", label);

            switch (type) {
                case "List":
                    FillCategoryItemsFromListQuery(ctx, userToken, title, xmlCategoryItems, url, query, viewfields, log, recipients);
                    break;

                case "Site":
                default:
                    FillCategoryItemsFromSiteQuery(ctx, userToken, title, xmlCategoryItems, url, query, viewfields, log, recipients);
                    break;
            }

            return xmlCategoryItems;

        }

        /// <summary>
        /// Fills the XmlDocument with results of a List query
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="userToken"></param>
        /// <param name="categoryTitle"></param>
        /// <param name="xmlCategoryItems"></param>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="viewfields"></param>
        /// <param name="log"></param>
        /// <param name="recipients"></param>
        /// <returns></returns>
        private static Boolean FillCategoryItemsFromListQuery(SPMailingContext ctx, SPUserToken userToken, String categoryTitle, XmlDocument xmlCategoryItems, String url, String query, String viewfields, StringBuilder log, List<SPUser> recipients) {

            //Encapsulates ViewFields in a XmlDocument so we can loop on all required fields when filling the category xml
            XmlDocument xmlViewFields = new XmlDocument();
            xmlViewFields.LoadXml(String.Format("<ViewFields>{0}</ViewFields>", viewfields));

            //TODO log a warning if a field is specified by ID

            //Ensures PermMask field that is required to check permissions on the item
            if (xmlViewFields.DocumentElement.SelectNodes("FieldRef[@Name='PermMask']").Count == 0) {
                XmlElement elmt = xmlViewFields.DocumentElement.AppendChild(xmlViewFields.CreateElement("FieldRef")) as XmlElement;
                elmt.SetAttribute("Name", "PermMask");
            }

            if (url.StartsWith(ctx.Site.Url))
                url = url.Substring(ctx.Site.Url.Length);
            if (url.StartsWith("/"))
                url = url.Substring(1);

            //Retrieves an instance of the web using current (privileged) account in order to perform permission checks
            using (SPWeb eWeb = ctx.Site.OpenWeb(url)) {

                //Retrieves site using the impersonation account
                using (SPSite site = new SPSite(ctx.Site.ID, ctx.Site.Zone, userToken)) {

                    site.CatchAccessDeniedException = false;

                    //Retrieves web using the impersonation account
                    using (SPWeb web = site.OpenWeb(url)) {

                        SPList eList = null;
                        SPList list = null;

                        //Retrieves target list
                        try {
                            list = web.GetList(url);
                            eList = eWeb.GetList(url);
                        } catch (Exception ex) {
                            String message = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Error_ListNotFound"), categoryTitle, url));
                            SPMailingHelper.AppendToLog(log, message, ex, SPMailingHelper.LogLevel.Error);
                            return false;
                        }

                        //Builds the query
                        SPQuery camlQuery = new SPQuery();
                        camlQuery.Query = String.IsNullOrEmpty(query) ? String.Empty : query.Trim();
                        camlQuery.ViewFields = xmlViewFields.DocumentElement.InnerXml.Trim();

                        SPListItemCollection items = null;

                        //Query execution
                        try {
                            items = list.GetItems(camlQuery);
                        } catch (Exception ex) {
                            String message = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Error_QueryError"), categoryTitle));
                            SPMailingHelper.AppendToLog(log, message, ex, SPMailingHelper.LogLevel.Error);
                            return false;
                        }

                        if (items.Count == 0)
                            return false;

                        foreach (SPListItem item in items) {

                            SPListItem eItem = eList.GetItemById(item.ID);
                            //Checks that recipients can access target item
                            foreach (SPUser user in recipients)
                                if (!eItem.DoesUserHavePermissions(user, SPBasePermissions.ViewListItems)) {
                                    String message = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Warning_UserCannotAccessListItem"), categoryTitle, user.LoginName)) + String.Format("<a href='{0}'>{0}</a>.", SPUrlUtility.CombineUrl(web.Url, item.Url));
                                    SPMailingHelper.AppendToLog(log, message, null, SPMailingHelper.LogLevel.Warning);
                                }

                            //Creates an entry into the results XmlDocument
                            XmlElement elmt = xmlCategoryItems.DocumentElement.AppendChild(xmlCategoryItems.CreateElement("item")) as XmlElement;

                            //Copies attributes to the entry
                            foreach (XmlElement field in xmlViewFields.DocumentElement.SelectNodes("FieldRef")) {
                                String fieldName = field.GetAttribute("Name");
                                try {

                                    String fieldValue = item[fieldName] == null ? null : item[fieldName].ToString();

                                    Match matchImg = REG_IMG.Match(fieldValue);
                                    if (matchImg.Success) {
                                        XmlDocument xmlImg = new XmlDocument();
                                        xmlImg.LoadXml(fieldValue);
                                        elmt.SetAttribute(fieldName + ".src", xmlImg.DocumentElement.GetAttribute("src"));
                                        elmt.SetAttribute(fieldName + ".alt", xmlImg.DocumentElement.GetAttribute("alt"));
                                        elmt.SetAttribute(fieldName + ".width", xmlImg.DocumentElement.GetAttribute("width"));
                                        elmt.SetAttribute(fieldName + ".height", xmlImg.DocumentElement.GetAttribute("height"));
                                    }
                                    elmt.SetAttribute(fieldName, fieldValue == null ? null : fieldValue.ToString());
                                } catch (Exception ex) {
                                    String msg = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Warning_ListFieldNotFound"), categoryTitle, fieldName));
                                    SPMailingHelper.AppendToLog(log, msg, ex, SPMailingHelper.LogLevel.Warning);
                                }
                            }
                        }
                    }
                }
            }

            return true;

        }

        /// <summary>
        /// Fills the XmlDocument with results of a Site query
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="userToken"></param>
        /// <param name="categoryTitle"></param>
        /// <param name="xmlCategoryItems"></param>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="viewfields"></param>
        /// <param name="log"></param>
        /// <param name="recipients"></param>
        /// <returns></returns>
        private static Boolean FillCategoryItemsFromSiteQuery(SPMailingContext ctx, SPUserToken userToken, String categoryTitle, XmlDocument xmlCategoryItems, String url, String query, String viewfields, StringBuilder log, List<SPUser> recipients) {
            
            Uri uri = new Uri(url);

            using (SPSite site = new SPSite(ctx.Site.ID, ctx.Site.Zone, userToken)) {

                site.CatchAccessDeniedException = false;

                using (SPWeb web = site.OpenWeb(uri.AbsolutePath)) {

                    //Prepares the query
                    SPSiteDataQuery dataQuery = new SPSiteDataQuery();
                    dataQuery.Webs = "<Webs Scope='Recursive' />";
                    dataQuery.ViewFields = String.IsNullOrEmpty(viewfields) ? String.Empty : viewfields.Trim();
                    dataQuery.Query = String.IsNullOrEmpty(query) ? String.Empty : query.Trim();

                    Int32 rowCount = 0;

                    //Execute the query for each base type which avoids prompting the user for this
                    for (Int32 i = 0; i <= 5; i++) {

                        dataQuery.Lists = String.Format("<Lists BaseType='{0}' />", i);

                        //Query execution
                        DataTable result = null;
                        try {

                            result = web.GetSiteData(dataQuery);

                        } catch (Exception ex) {
                            String msg = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Error_QueryError"), categoryTitle));
                            SPMailingHelper.AppendToLog(log, msg, ex, SPMailingHelper.LogLevel.Error);
                            return false;
                        }

                        rowCount += result.Rows.Count;


                        //Add entries to the result set
                        foreach (DataRow dtr in result.Rows) {

                            Guid webId = new Guid((String)dtr["WebId"]);
                            Guid listId = new Guid((String)dtr["ListId"]);
                            Int32 itemId = Convert.ToInt32(dtr["ID"]);

                            //Permission check will be done using current (privileged) account
                            using (SPWeb eWeb = ctx.Site.OpenWeb(webId)) {
                                SPList list = eWeb.Lists[listId];
                                SPListItem item = list.GetItemById(itemId);

                                //Checks read permissions for each recipient
                                foreach (SPUser user in recipients)
                                    if (!item.DoesUserHavePermissions(user, SPBasePermissions.ViewListItems)) {
                                        String message = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Warning_UserCannotAccessListItem"), categoryTitle, user.LoginName)) + String.Format("<a href='{0}'>{0}</a>", SPUrlUtility.CombineUrl(web.Url, item.Url));
                                        SPMailingHelper.AppendToLog(log, message, null, SPMailingHelper.LogLevel.Warning);
                                    }


                                //Creates an entry in the result XmlDocument
                                XmlElement elmt = xmlCategoryItems.DocumentElement.AppendChild(xmlCategoryItems.CreateElement("item")) as XmlElement;
                                foreach (DataColumn dtc in result.Columns) {

                                    String fieldValue = dtr[dtc.ColumnName].ToString();

                                    Match matchImg = REG_IMG.Match(fieldValue);
                                    if (matchImg.Success) {
                                        XmlDocument xmlImg = new XmlDocument();
                                        xmlImg.LoadXml(fieldValue);
                                        elmt.SetAttribute(dtc.ColumnName + ".src", xmlImg.DocumentElement.GetAttribute("src"));
                                        elmt.SetAttribute(dtc.ColumnName + ".alt", xmlImg.DocumentElement.GetAttribute("alt"));
                                        elmt.SetAttribute(dtc.ColumnName + ".width", xmlImg.DocumentElement.GetAttribute("width"));
                                        elmt.SetAttribute(dtc.ColumnName + ".height", xmlImg.DocumentElement.GetAttribute("height"));
                                    }
                                    elmt.SetAttribute(dtc.ColumnName, fieldValue);
                                }
                            }

                        }

                    }

                    if (rowCount == 0)
                        return false;

                }
            }

            return true;

        }

        private static Regex REG_IMG = new Regex(@"^\<img[^\>]*\>$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        #endregion

        private static void FillSharePointUserRecipients(SPMailingContext ctx, SPListItem mailingDefinition, List<SPUser> recipientUsers, List<String> recipientContactEmails, StringBuilder log) {

            //Gets lookups to recipients list
            SPFieldLookupValueCollection luRecipientsLists = mailingDefinition[ctx.FieldIds.RecipientsLists] as SPFieldLookupValueCollection;

            if (luRecipientsLists != null) {

                //For each recipient list
                foreach (SPFieldLookupValue luRecipientsList in luRecipientsLists) {

                    //Retrieves associated recipients list
                    SPListItem recipientsList = ctx.RecipientsLists.GetItemById(luRecipientsList.LookupId);

                    //Adds SharePoint Users (or groups)
                    SPFieldUserValueCollection luRecipientsListUsers = recipientsList[ctx.FieldIds.PeopleAndGroupsRecipients] as SPFieldUserValueCollection;
                    if (luRecipientsListUsers != null)
                        foreach (SPFieldUserValue luRecipientsListUser in luRecipientsListUsers) {

                            if (luRecipientsListUser.User == null) { //SharePoint Group
                                try {

                                    //Adds all users in the group into the collection
                                    SPGroup oGroup = ctx.RootWeb.SiteGroups.GetByID(luRecipientsListUser.LookupId);
                                    foreach (SPUser user in oGroup.Users) {
                                        if (recipientUsers.Count(u => u.LoginName == user.LoginName) == 0) {
                                            if (String.IsNullOrEmpty(user.Email)) {
                                                String msg = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Warning_UserHasNoEmail"), user.LoginName));
                                                SPMailingHelper.AppendToLog(log, msg, null, SPMailingHelper.LogLevel.Warning);
                                            }
                                            recipientUsers.Add(user);
                                        }
                                    }

                                } catch {
                                    String msg = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Warning_UserNotFound"), luRecipientsListUser.LookupValue));
                                    SPMailingHelper.AppendToLog(log, msg, null, SPMailingHelper.LogLevel.Warning);
                                }
                            } else //SharePoint User : adds it to the collection
                                if (recipientUsers.Count(u => u.LoginName == luRecipientsListUser.User.LoginName) == 0) {
                                    if (String.IsNullOrEmpty(luRecipientsListUser.User.Email)) {
                                        String msg = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Warning_UserHasNoEmail"), luRecipientsListUser.User.LoginName));
                                        SPMailingHelper.AppendToLog(log, msg, null, SPMailingHelper.LogLevel.Warning);
                                    }
                                    recipientUsers.Add(luRecipientsListUser.User);
                                }

                        }

                    //Adds contact emails
                    SPFieldLookupValueCollection luContactRecipients = recipientsList[ctx.FieldIds.ContactRecipients] as SPFieldLookupValueCollection;
                    if (luContactRecipients != null) {

                        foreach (SPFieldLookupValue luContactRecipient in luContactRecipients) {

                            SPListItem contactRecipient = ctx.ContactRecipients.GetItemById(luContactRecipient.LookupId);
                            String email = contactRecipient[SPBuiltInFieldId.EMail] as String;

                            if (String.IsNullOrEmpty(email))
                                SPMailingHelper.AppendToLog(log, HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Warning_ContactHasNoEmail"), contactRecipient.Title, contactRecipient.ID)), null, SPMailingHelper.LogLevel.Warning);
                            else
                                if (!recipientContactEmails.Contains(email))
                                    recipientContactEmails.Add(email);

                        }

                    }

                }

            }

        }

        #endregion

    }
}
