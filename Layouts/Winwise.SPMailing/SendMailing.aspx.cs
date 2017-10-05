using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web;

namespace Winwise.SPMailing.Layouts.Winwise.SPMailing {
    public partial class SendMailing : LayoutsPageBase {

        protected void Page_Load(object sender, EventArgs e) {

            String returnUrl = null;
            
            if (HttpContext.Current.Request.UrlReferrer != null)
                returnUrl = HttpContext.Current.Request.UrlReferrer.ToString();
            else
                returnUrl = SPUrlUtility.CombineUrl(Web.ServerRelativeUrl, SPMailingContext.Current.Mailings.Forms[PAGETYPE.PAGE_DISPLAYFORM].Url + "?ID=" + ItemId);

            using (SPLongOperation lo = new SPLongOperation(this)) {
                try {

                    lo.LeadingHTML = SPMailingHelper.GetLocalizedString(Web, "SendMailing_Processing_LeadingHTML");
                    lo.TrailingHTML = SPMailingHelper.GetLocalizedString(Web, "SendMailing_Processing_TrailingHTML");

                    lo.Begin();

                    //Retrieves the mailing item
                    SPListItem mailingItem = SPMailingContext.Current.Mailings.GetItemById(ItemId);

                    //creates mailing business object
                    //SPMailingMail mail = SPMailingMail.CreateMailing(SPMailingContext.Current, mailingItem);

                    //Sends the mailing
                    SPMailingSender.Send(SPMailingContext.Current, mailingItem, IsTest);

                    lo.End(returnUrl);

                } catch (Exception ex) {
                    lo.End("/_layouts/error.aspx", SPRedirectFlags.Default, HttpContext.Current, "ErrorText=" + HttpUtility.UrlEncode(ex.Message));
                }
            }

        }

        protected Int32 ItemId {
            get { return Int32.Parse(this.Request["ID"]); }
        }

        protected Boolean IsTest {
            get {
                Boolean isTest = false;
                if (!Boolean.TryParse(Request.Params["IsTest"], out isTest))
                    return false;
                return isTest;
            }
        }

    }
}
