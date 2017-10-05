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
using System.Threading;

namespace Winwise.SPMailing.Workflows.SPMailingDefinition_Generation {
    public sealed partial class SPMailingDefinition_Generation : StateMachineWorkflowActivity {

        public SPMailingDefinition_Generation() {
            InitializeComponent();
        }

        public Boolean automaticGeneration = default(Boolean);
        public TimeSpan delayBetweenGenerations = default(TimeSpan);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        public Int32 userId = -1;
        public String logHistoryOutcome = default(String);
        public String logHistoryDescription = default(String);

        private void GenerationInitialState_onWorkflowActivated_Invoked(object sender, ExternalDataEventArgs e) {
            RefreshWorkflowFields();
        }

        private void RefreshWorkflowFields() {

            SPMailingContext ctx = SPMailingContext.GetContext(workflowProperties.Web);
            automaticGeneration = (Boolean)workflowProperties.Item[ctx.FieldIds.AutoGenerate];
            Int32 daysOffset = Convert.ToInt32(workflowProperties.Item[ctx.FieldIds.DaysOffset]);

#if DEBUG
            delayBetweenGenerations = new TimeSpan(0, daysOffset, 0);
#else
            delayBetweenGenerations = new TimeSpan(daysOffset, 0, 0, 0);
#endif

        }


        private void GenerationRoutingState_RefreshWorkflowFields_ExecuteCode(object sender, EventArgs e) {
            RefreshWorkflowFields();
        }


        private void GenerateMailing_ExecuteCode(object sender, EventArgs e) {

            SPMailingContext ctx = SPMailingContext.GetContext(workflowProperties.Web);
            SPMailingGenerator.GenerateMailing(ctx, workflowProperties.Item);

        }

        private void logWorkflowActivated_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_WorkflowActivated");
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Initializing");
            userId = workflowProperties.OriginatorUser.ID;
        }

        private void logManualGenerationState_Init_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_ManualGenerationState_Init");
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Manual");
            userId = -1;
        }

        private void logAutomaticGenerationState_Init_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_AutomaticGenerationState_Init");
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Automatic");
            userId = -1;
        }

        private void logAutomaticGenerationWaitingState_Init_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = String.Format(SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_AutomaticGenerationWaitingState_Init"), delayBetweenGenerations);
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Automatic");
            userId = -1;
        }

        private void logAutomaticGenerationWaitingState_DelayElapsed_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_AutomaticGenerationWaitingState_DelayElapsed");
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Automatic");
            userId = -1;
        }

        private void logAutomaticGenerationWaitingState_ItemChanged_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = "modif quand auto"; // SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_ItemChanged");
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Updating");
            SPFieldUserValue luUser = new SPFieldUserValue(workflowProperties.Web, workflowProperties.Item[SPBuiltInFieldId.Editor] as String);
            userId = luUser.LookupId;
        }

        private void logManualGenerationState_ItemChanged_MethodInvoking(object sender, EventArgs e) {
            logHistoryDescription = "modif quand manuel";//SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Log_ItemChanged");
            logHistoryOutcome = SPMailingHelper.GetLocalizedString(workflowProperties.Web, "Workflow_Outcome_Updating");
            SPFieldUserValue luUser = new SPFieldUserValue(workflowProperties.Web, workflowProperties.Item[SPBuiltInFieldId.Editor] as String);
            userId = luUser.LookupId;
        }

    }
}
