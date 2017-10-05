using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;

namespace Winwise.SPMailing.Workflows.Workflow_MailingDefinition {
    public sealed partial class Workflow_MailingDefinition : SequentialWorkflowActivity {
        public Workflow_MailingDefinition() {
            InitializeComponent();
        }

        #region Fields

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        public Boolean automaticGeneration = default(Boolean);
        public TimeSpan delayBetweenGenerations = default(TimeSpan);
        public String logHistoryOutcome = default(String);
        public String logHistoryDescription = default(String);
        public Int32 userId = default(Int32);

        #endregion

        #region Business Logic

        private void GenerateMailing_ExecuteCode(object sender, EventArgs e) {

            SPMailingContext ctx = SPMailingContext.GetContext(workflowProperties.Web);
            SPMailingGenerator.GenerateMailing(ctx, workflowProperties.Item);

        }

        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e) {

            SPMailingContext ctx = SPMailingContext.GetContext(workflowProperties.Web);

            automaticGeneration = (Boolean)workflowProperties.Item[ctx.FieldIds.AutoGenerate];

            Int32 daysOffset = Convert.ToInt32(workflowProperties.Item[ctx.FieldIds.DaysOffset]);

            if (daysOffset <= 0)
                throw new Exception(SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Error_DaysOffsetNotSet"));

#if DEBUG
            delayBetweenGenerations = new TimeSpan(0, 1, 0);
#else
            delayBetweenGenerations = new TimeSpan(daysOffset, 0, 0, 0);
#endif

        }

        #endregion

        #region Tracking

        #endregion

        private void logWorkflowActivated_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_WorkflowActivated");
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Initializing");
            userId = workflowProperties.OriginatorUser.ID;
        }

        private void logAutomaticGeneration_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_AutomaticGeneration");
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Generating");
            userId = -1;
        }

        private void logWaitForDaysOffset_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = String.Format(SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_WaitForDaysOffset"), delayBetweenGenerations);
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Waiting");
            userId = -1;
        }

        private void logManualGeneration_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_ManualGeneration");
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Running");
            userId = -1;
        }

        private void logItemChanged_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_ItemChanged");
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Terminating");
            userId = -1;
        }

        private void logWorkflowTerminating_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_Terminating");
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Terminating");
            userId = -1;
        }
    }
}
