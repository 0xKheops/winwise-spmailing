using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace Winwise.SPMailing.Features.Web_Mailing_Dependencies {
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>
    [Guid("f0ae4132-2e1d-491e-a81e-fd556d15f8c7")]
    public class Web_Mailing_DependenciesEventReceiver : SPFeatureReceiver {

        public override void FeatureActivated(SPFeatureReceiverProperties properties) {

            if (!SPContext.Current.Web.CurrentUser.IsSiteAdmin)
                throw new Exception("Only a site collection administrator can activate this site feature because it contains mecanisms allowing users to query data from the whole site collection with elevated privileges. Visit http://spmailing.codeplex.com for more information.");

            //Ensures Site_Mailings feature is activated
            using (SPWeb web = properties.Feature.Parent as SPWeb) {
                using (SPSite site = new SPSite(web.Site.ID)) {
                    if (site.Features[SPMailingFeatureIds.SITE_MAILINGS] == null)
                        site.Features.Add(SPMailingFeatureIds.SITE_MAILINGS);
                }
            }

        }

    }
}
