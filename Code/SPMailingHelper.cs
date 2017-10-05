using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Web;
using Microsoft.SharePoint.Administration;
using System.Diagnostics;
using System.Xml;

namespace Winwise.SPMailing {
    /// <summary>
    /// General utility class
    /// </summary>
    class SPMailingHelper {

        #region Utilities

        /// <summary>
        /// Returns a localized String from Winwise.SPMailing.resx
        /// </summary>
        /// <param name="web"></param>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static String GetLocalizedString(SPWeb web, String resourceKey) {
            String source = String.Format("$Resources:Winwise.SPMailing,{0};", resourceKey);
            return SPUtility.GetLocalizedString(source, "Winwise.SPMailing", web.Language);
        }

        /// <summary>
        /// Returns a list from a site relative url
        /// </summary>
        /// <param name="web"></param>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public static SPList GetListFromWeb(SPWeb web, String relativeUrl) {
            return web.GetList(SPUrlUtility.CombineUrl(web.ServerRelativeUrl, relativeUrl));
        }

        #endregion

        #region Lookups

        /// <summary>
        /// Returns the lookup field associated to the key supplied when creating it using EnsureLookupField
        /// </summary>
        /// <param name="web"></param>
        /// <param name="lookupPropertyKey"></param>
        /// <returns></returns>
        public static Guid GetLookupFieldId(SPWeb web, String lookupPropertyKey) {

            if (!web.Properties.ContainsKey(lookupPropertyKey))
                throw new Exception(String.Format(SPMailingHelper.GetLocalizedString(web, "Error_Lookup_PropertyKeyNotFound"), lookupPropertyKey));

            String strFieldId = web.Properties[lookupPropertyKey];
            return new Guid(strFieldId);

        }

        /// <summary>
        /// Creates a lookup field and stores it's ID in the web's property bag using supplied key
        /// </summary>
        /// <param name="web"></param>
        /// <param name="sourceList"></param>
        /// <param name="lookupList"></param>
        /// <param name="lookupPropertyKey"></param>
        /// <param name="displayNameResKey"></param>
        /// <param name="descriptionResKey"></param>
        /// <param name="required"></param>
        /// <param name="allowMultipleValues"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public static SPFieldLookup EnsureLookupField(SPWeb web, SPList sourceList, SPList lookupList, String lookupPropertyKey, String displayNameResKey, String descriptionResKey, Boolean required, Boolean allowMultipleValues, Boolean readOnly) {

            if (!web.Properties.ContainsKey(lookupPropertyKey)) {

                Guid newFieldId = Guid.NewGuid();

                //Hack : Generates the lookup field using a XmlSchema to ensure localization (other methods don't seem to work if feature activation is done by script)
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<Field />");
                xmlDoc.DocumentElement.SetAttribute("ID", newFieldId.ToString());
                xmlDoc.DocumentElement.SetAttribute("DisplayName", String.Format("$Resources:Winwise.SPMailing,{0};", displayNameResKey));
                xmlDoc.DocumentElement.SetAttribute("Description", String.Format("$Resources:Winwise.SPMailing,{0};", descriptionResKey));
                xmlDoc.DocumentElement.SetAttribute("Type", allowMultipleValues ? "LookupMulti" : "Lookup");
                xmlDoc.DocumentElement.SetAttribute("Required", required.ToString().ToUpper());
                xmlDoc.DocumentElement.SetAttribute("List", lookupList.ID.ToString());
                xmlDoc.DocumentElement.SetAttribute("WebId", web.ID.ToString());
                xmlDoc.DocumentElement.SetAttribute("StaticName", lookupPropertyKey);
                xmlDoc.DocumentElement.SetAttribute("Name", lookupPropertyKey);
                xmlDoc.DocumentElement.SetAttribute("Group", "SPMailing");
                xmlDoc.DocumentElement.SetAttribute("ReadOnly", readOnly.ToString().ToUpper());
                xmlDoc.DocumentElement.SetAttribute("ShowInDisplayForm", "TRUE");
                xmlDoc.DocumentElement.SetAttribute("ShowField", "Title");
                xmlDoc.DocumentElement.SetAttribute("EnforceUniqueValues", "FALSE");
                if (allowMultipleValues)
                    xmlDoc.DocumentElement.SetAttribute("Mult", "TRUE");
                sourceList.Fields.AddFieldAsXml(xmlDoc.OuterXml);

                //Saves the ID of the column in the web's property bag
                SPPropertyBag properties = web.Properties;
                properties.Add(lookupPropertyKey, newFieldId.ToString());
                properties.Update();

            }

            //Retrieves the ID of the column in the web's property bag
            String strFieldId = web.Properties[lookupPropertyKey];
            Guid fieldId = new Guid(strFieldId);

            //Returns the field using it's ID
            return sourceList.Fields[fieldId] as SPFieldLookup;

        }

        #endregion

        #region Log

        public static void BeginLog(SPWeb web, StringBuilder log, String message) {
            log.AppendLine(String.Format("<b>{0} - {1}</b><br /><ul>", HttpUtility.HtmlEncode(DateTime.Now.ToString(web.Locale)), HttpUtility.HtmlEncode(message)));
        }

        public static void EndLog(StringBuilder log) {
            log.AppendLine("</ul>");
        }

        public static void AppendToLog(StringBuilder log, String htmlMessage, Exception ex, LogLevel level) {

            String img = String.Empty;
            switch (level) {
                case LogLevel.Warning:
                    img = "<img src='/_layouts/images/warning16by16.gif' style='margin-right:2px' />";
                    break;
                case LogLevel.Error:
                    img = "<img src='/_layouts/images/error16by16.gif' style='margin-right:2px' />";
                    break;
                case LogLevel.Information:
                default:
                    img = "<img src='/_layouts/images/info16by16.gif' style='margin-right:2px' />";
                    break;
            }
            String error = null;

#if DEBUG
            error = (ex == null) ? null : String.Format("<br /><span>{0}</span><br /><span style='font-style:italic'>{1}</span>", HttpUtility.HtmlEncode(ex.Message), HttpUtility.HtmlEncode(ex.StackTrace).Replace(Environment.NewLine, "<br />"));
#else
            error = (ex == null) ? null : String.Format("<br /><span>{0}</span><br />", HttpUtility.HtmlEncode(ex.Message));
#endif

            log.AppendLine(String.Format("<li><span style='vertical-align:middle'>{0}</span><span style='vertical-align:middle'>{1}</span>{2}</li>", img, htmlMessage, error));

        }

        public enum LogLevel {
            None,
            Information,
            Warning,
            Error
        }

        #endregion

        #region Workflow Lists

        /// <summary>
        /// Creates a workflow task list if it does not exist
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        internal static SPList EnsureWorkflowTasksList(SPWeb web) {

            String url = "Lists/WorkflowTasks";

            SPList tasksList = null;

            //Retrieves an instance to the web's task list
            try {
                tasksList = web.GetList(SPUrlUtility.CombineUrl(web.ServerRelativeUrl, url));
            } catch { }

            //Creates if does not exist
            if (tasksList == null) {
                String title = GetLocalizedString(web, "Workflow_TaskList_Title");
                String desc = GetLocalizedString(web, "Workflow_TaskList_Description"); ;
                Guid tasksListId = web.Lists.Add(title, desc, url, "00bfea71-a83e-497e-9ba0-7a5c597d0107", (Int32)SPListTemplateType.Tasks, null);
                tasksList = web.Lists[tasksListId];
                tasksList.NoCrawl = true;
                tasksList.Update();
            }

            return tasksList;

        }

        /// <summary>
        /// Creates a workflow history list if it does not exist
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        internal static SPList EnsureWorkflowHistoryList(SPWeb web) {

            String url = "Lists/WorkflowHistory";

            SPList historyList = null;

            //Retrieves an instance to the web's task list
            try {
                historyList = web.GetList(SPUrlUtility.CombineUrl(web.ServerRelativeUrl, url));
            } catch { }

            //Creates if does not exist
            if (historyList == null) {
                String title = GetLocalizedString(web, "Workflow_HistoryList_Title");
                String desc = GetLocalizedString(web, "Workflow_HistoryList_Description"); ;
                Guid historyListId = web.Lists.Add(title, desc, url, "00bfea71-4ea5-48d4-a4ad-305cf7030140", (Int32)SPListTemplateType.WorkflowHistory, null);
                historyList = web.Lists[historyListId];
                historyList.NoCrawl = true;
                historyList.Hidden = true;
                historyList.Update();
            }

            return historyList;

        }

        #endregion

    }
}
