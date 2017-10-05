using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace Winwise.SPMailing.Features.Web_Mailings
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("fe1d3fa6-46a3-4875-9e70-f4d92aceff16")]
    public class Web_MailingsEventReceiver : SPFeatureReceiver
    {

        /// <summary>
        /// Creates lookup columns
        /// </summary>
        /// <param name="properties"></param>
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            using (SPWeb web = properties.Feature.Parent as SPWeb)
            {

                //Retrieves lists by their url
                SPList lCategories = SPMailingHelper.GetListFromWeb(web, "Lists/Categories");
                SPList lCategoryTemplates = SPMailingHelper.GetListFromWeb(web, "CategoryTemplates");
                SPList lContactRecipients = SPMailingHelper.GetListFromWeb(web, "Lists/ContactRecipients");
                SPList lMailings = SPMailingHelper.GetListFromWeb(web, "Lists/Mailings");
                SPList lMailingDefinitions = SPMailingHelper.GetListFromWeb(web, "Lists/MailingDefinitions");
                SPList lMailingTemplates = SPMailingHelper.GetListFromWeb(web, "MailingTemplates");
                SPList lRecipientsLists = SPMailingHelper.GetListFromWeb(web, "Lists/RecipientsLists");
                
                //Creates local lookup columns
                SPMailingHelper.EnsureLookupField(web, lCategories, lCategoryTemplates, SPMailingFieldIds.CATEGORY_TEMPLATE_PROPERTY_KEY, "Field_CategoryTemplate_Title", "Field_CategoryTemplate_Description", true, false, false);
                SPMailingHelper.EnsureLookupField(web, lMailingDefinitions, lCategories, SPMailingFieldIds.CATEGORIES_PROPERTY_KEY, "Field_Categories_Title", "Field_Categories_Description", false, true, false);
                SPMailingHelper.EnsureLookupField(web, lMailingDefinitions, lRecipientsLists, SPMailingFieldIds.RECIPIENTS_LISTS_PROPERTY_KEY, "Field_RecipientsLists_Title", "Field_RecipientsLists_Description", false, true, false);
                SPMailingHelper.EnsureLookupField(web, lMailingDefinitions, lMailingTemplates, SPMailingFieldIds.MAILING_TEMPLATE_PROPERTY_KEY, "Field_MailingTemplate_Title", "Field_MailingTemplate_Description", true, false, false);
                SPMailingHelper.EnsureLookupField(web, lMailings, lMailingDefinitions, SPMailingFieldIds.MAILING_DEFINITION_PROPERTY_KEY, "Field_MailingDefinition_Title", "Field_MailingDefinition_Description",false, false, true);
                SPMailingHelper.EnsureLookupField(web, lRecipientsLists, lContactRecipients, SPMailingFieldIds.CONTACT_RECIPIENTS_PROPERTY_KEY, "Field_ContactRecipients_Title", "Field_ContactRecipients_Description", false, true, false);


                ///////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////// Workflow Association //////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////
                //Hack : if Site_Mailings has been automatically activated by feature dependency within the same thread, workflow template may not be yet available via API.
                //Reinstanciating site and web to make it available
                using (SPSite site2 = new SPSite(web.Site.ID)) {
                    using (SPWeb web2 = site2.OpenWeb(web.ID)) {

                        //Retrieves the list
                        SPList lMailingDefinitions2 = SPMailingHelper.GetListFromWeb(web2, "Lists/MailingDefinitions");

                        //Creates task and history list if necessary
                        SPList workflowTasks = SPMailingHelper.EnsureWorkflowTasksList(web2);
                        SPList workflowHistory = SPMailingHelper.EnsureWorkflowHistoryList(web2);

                        //Creates workflow association if necessary
                        if (lMailingDefinitions2.WorkflowAssociations.GetAssociationByBaseID(SPMailingWorkflowIds.MailingDefinition) == null) {
                            //Associates the workflow to the list 
                            SPWorkflowTemplate generationWorkflow = web2.WorkflowTemplates.GetTemplateByBaseID(SPMailingWorkflowIds.MailingDefinition);
                            SPWorkflowAssociation generationWorkflowAssociation = SPWorkflowAssociation.CreateListAssociation(generationWorkflow, SPMailingHelper.GetLocalizedString(web2, "WorkflowAssociation_MailingDefinition_Name"), workflowTasks, workflowHistory);
                            generationWorkflowAssociation.Name = SPMailingHelper.GetLocalizedString(web2, "WorkflowAssociation_MailingDefinition_Name");
                            generationWorkflowAssociation.Description = SPMailingHelper.GetLocalizedString(web2, "WorkflowAssociation_MailingDefinition_Description");
                            generationWorkflowAssociation.AutoStartCreate = true;
                            generationWorkflowAssociation.AutoStartChange = true;
                            generationWorkflowAssociation.AllowManual = true;
                            lMailingDefinitions2.WorkflowAssociations.Add(generationWorkflowAssociation);
                        }

                    }
                }

            }

        }

    }
}
