using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Net.Mail;
using System.Web;
using System.Net.Mime;

namespace Winwise.SPMailing {

    /// <summary>
    /// Utility class used to send mailings
    /// </summary>
    class SPMailingSender {

        #region Sending

        /// <summary>
        /// Sends the specified mailing
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="mailingItem"></param>
        /// <param name="isTest"></param>
         public static void Send(SPMailingContext ctx, SPListItem mailingItem, Boolean isTest) {

            ctx.Web.AllowUnsafeUpdates = true;

            StringBuilder log = new StringBuilder((String)mailingItem[ctx.FieldIds.Log]);

            SPMailingHelper.BeginLog(ctx.Web, log, SPMailingHelper.GetLocalizedString(ctx.Web, "Log_BeginSend"));

            try {

                if (!isTest && mailingItem[ctx.FieldIds.Sent] != null && (Boolean)mailingItem[ctx.FieldIds.Sent] == true)
                    throw new Exception(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Error_MailingAlreadySent"));

                Uri webUrl = new Uri(ctx.Web.Url);

                //Récupération du contenu reformatté
                SPMailingMailMessageDefinition msgDef = SPMailingMailMessageDefinition.CreateMailing(ctx, mailingItem);

                SmtpClient smtp = new SmtpClient(ctx.Site.WebApplication.OutboundMailServiceInstance.Server.Address);

                MailAddress from = new MailAddress(String.IsNullOrEmpty(msgDef.FromAdress) ? ctx.Site.WebApplication.OutboundMailSenderAddress : msgDef.FromAdress, String.IsNullOrEmpty(msgDef.FromDisplayName) ? ctx.RootWeb.Title : msgDef.FromDisplayName);
                MailAddress replyTo = new MailAddress(String.IsNullOrEmpty(msgDef.ReplyToAdress) ? from.Address : msgDef.ReplyToAdress, String.IsNullOrEmpty(msgDef.ReplyToDisplayName) ? from.DisplayName : msgDef.ReplyToDisplayName);

                //Recipients selection
                List<String> recipients = new List<String>();
                if (isTest) {
                    recipients.Add(ctx.Web.CurrentUser.Email);
                } else {
                    String emails = mailingItem[ctx.FieldIds.Recipients] as String;
                    recipients.AddRange(emails.Split(';'));
                }

                //Sends a mail to each recipient
                foreach (String email in recipients)
                    if (!String.IsNullOrEmpty(email)) {
                        
                        MailAddress to = new MailAddress(email);
                        MailMessage mail = new MailMessage(from, to);

                        mail.ReplyTo = replyTo;
                        mail.Subject = msgDef.Subject;

                        AlternateView view = AlternateView.CreateAlternateViewFromString(msgDef.EmbeddedBody, null, MediaTypeNames.Text.Html);
                        foreach (SPMailingResource img in msgDef.EmbeddedImages)
                            view.LinkedResources.Add(img.GetLinkedResource());
                        mail.AlternateViews.Add(view);

                        try {
                            //sens the mail
                            smtp.Send(mail);
                            String msg = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Info_MailingSentTo"), email));
                            SPMailingHelper.AppendToLog(log, msg, null, SPMailingHelper.LogLevel.Information);
                        } catch (Exception ex) {
                            String err = HttpUtility.HtmlEncode(String.Format(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Warning_MailingCouldNotBeSentTo"), email));
                            SPMailingHelper.AppendToLog(log, err, ex, SPMailingHelper.LogLevel.Warning);
                        }
                    }

                if (!isTest) {
                    SPMailingHelper.EndLog(log);
                    mailingItem[ctx.FieldIds.Sent] = true;
                    mailingItem[ctx.FieldIds.Log] = log.ToString();
                    mailingItem.SystemUpdate();
                }

            } catch (Exception ex) {

                String msg = HttpUtility.HtmlEncode(SPMailingHelper.GetLocalizedString(ctx.Web, "Log_Error_SendError"));
                SPMailingHelper.AppendToLog(log, msg, ex, SPMailingHelper.LogLevel.Error);
                SPMailingHelper.EndLog(log);

                mailingItem[ctx.FieldIds.Log] = log.ToString();
                mailingItem.SystemUpdate();

                throw ex;
            }

        }

        #endregion

    }
}
