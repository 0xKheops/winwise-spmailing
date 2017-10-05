using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web;

namespace Winwise.SPMailing.Layouts.Winwise.SPMailing {
    public partial class GenerateMailing : LayoutsPageBase {

        protected void Page_Load(object sender, EventArgs e) {

            using (SPLongOperation lo = new SPLongOperation(this)) {
                try {
                    
                    lo.LeadingHTML = SPMailingHelper.GetLocalizedString(Web, "GenerateMailing_Processing_LeadingHTML");
                    lo.TrailingHTML = SPMailingHelper.GetLocalizedString(Web, "GenerateMailing_Processing_TrailingHTML");

                    lo.Begin();

                    //Récupération de la definition de mail
                    SPListItem definition = SPMailingContext.Current.MailingDefinitions.GetItemById(ItemId);

                    //Génération d'un mailing à partir de la définition
                    SPListItem mailing = SPMailingGenerator.GenerateMailing(SPMailingContext.Current, definition);

                    String itemUrl = String.Format("{0}?ID={1}", SPMailingContext.Current.Mailings.Forms[PAGETYPE.PAGE_DISPLAYFORM].ServerRelativeUrl, mailing.ID);

                    lo.End(itemUrl);
                } catch (Exception ex) {
                    lo.End("/_layouts/error.aspx", SPRedirectFlags.Default, HttpContext.Current, "ErrorText=" + HttpUtility.UrlEncode(ex.Message));
                }
            }

        }

        protected Int32 ItemId {
            get { return Int32.Parse(this.Request["ID"]); }
        }

    }
}
