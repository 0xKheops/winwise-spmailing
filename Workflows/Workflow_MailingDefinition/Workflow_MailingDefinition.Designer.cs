using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Winwise.SPMailing.Workflows.Workflow_MailingDefinition {
    public sealed partial class Workflow_MailingDefinition {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent() {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference2 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind19 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind21 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind20 = new System.Workflow.ComponentModel.ActivityBind();
            this.WaitForDaysOffset = new System.Workflow.Activities.DelayActivity();
            this.logWaitForDaysOffset = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.GenerateMailing = new System.Workflow.Activities.CodeActivity();
            this.logAutomaticGeneration = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.sequenceActivity1 = new System.Workflow.Activities.SequenceActivity();
            this.terminateActivity1 = new System.Workflow.ComponentModel.TerminateActivity();
            this.logItemChanged = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.onWorkflowItemChanged = new Microsoft.SharePoint.WorkflowActions.OnWorkflowItemChanged();
            this.logManualGeneration = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.whileActivity1 = new System.Workflow.Activities.WhileActivity();
            this.eventDrivenActivity1 = new System.Workflow.Activities.EventDrivenActivity();
            this.ifElseBranchActivity2 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity1 = new System.Workflow.Activities.IfElseBranchActivity();
            this.eventHandlersActivity1 = new System.Workflow.Activities.EventHandlersActivity();
            this.ifElseActivity1 = new System.Workflow.Activities.IfElseActivity();
            this.logWorkflowTerminating = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.eventHandlingScopeActivity1 = new System.Workflow.Activities.EventHandlingScopeActivity();
            this.logWorkflowActivated = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.onWorkflowActivated = new Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated();
            // 
            // WaitForDaysOffset
            // 
            this.WaitForDaysOffset.Name = "WaitForDaysOffset";
            activitybind1.Name = "Workflow_MailingDefinition";
            activitybind1.Path = "delayBetweenGenerations";
            this.WaitForDaysOffset.SetBinding(System.Workflow.Activities.DelayActivity.TimeoutDurationProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // logWaitForDaysOffset
            // 
            this.logWaitForDaysOffset.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logWaitForDaysOffset.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind2.Name = "Workflow_MailingDefinition";
            activitybind2.Path = "logHistoryDescription";
            activitybind3.Name = "Workflow_MailingDefinition";
            activitybind3.Path = "logHistoryOutcome";
            this.logWaitForDaysOffset.Name = "logWaitForDaysOffset";
            this.logWaitForDaysOffset.OtherData = "";
            activitybind4.Name = "Workflow_MailingDefinition";
            activitybind4.Path = "userId";
            this.logWaitForDaysOffset.MethodInvoking += new System.EventHandler(this.logWaitForDaysOffset_MethodInvoking);
            this.logWaitForDaysOffset.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.logWaitForDaysOffset.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.logWaitForDaysOffset.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // GenerateMailing
            // 
            this.GenerateMailing.Name = "GenerateMailing";
            this.GenerateMailing.ExecuteCode += new System.EventHandler(this.GenerateMailing_ExecuteCode);
            // 
            // logAutomaticGeneration
            // 
            this.logAutomaticGeneration.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logAutomaticGeneration.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind5.Name = "Workflow_MailingDefinition";
            activitybind5.Path = "logHistoryDescription";
            activitybind6.Name = "Workflow_MailingDefinition";
            activitybind6.Path = "logHistoryOutcome";
            this.logAutomaticGeneration.Name = "logAutomaticGeneration";
            this.logAutomaticGeneration.OtherData = "";
            activitybind7.Name = "Workflow_MailingDefinition";
            activitybind7.Path = "userId";
            this.logAutomaticGeneration.MethodInvoking += new System.EventHandler(this.logAutomaticGeneration_MethodInvoking);
            this.logAutomaticGeneration.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.logAutomaticGeneration.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.logAutomaticGeneration.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // sequenceActivity1
            // 
            this.sequenceActivity1.Activities.Add(this.logAutomaticGeneration);
            this.sequenceActivity1.Activities.Add(this.GenerateMailing);
            this.sequenceActivity1.Activities.Add(this.logWaitForDaysOffset);
            this.sequenceActivity1.Activities.Add(this.WaitForDaysOffset);
            this.sequenceActivity1.Name = "sequenceActivity1";
            // 
            // terminateActivity1
            // 
            this.terminateActivity1.Name = "terminateActivity1";
            // 
            // logItemChanged
            // 
            this.logItemChanged.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logItemChanged.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind8.Name = "Workflow_MailingDefinition";
            activitybind8.Path = "logHistoryDescription";
            activitybind9.Name = "Workflow_MailingDefinition";
            activitybind9.Path = "logHistoryOutcome";
            this.logItemChanged.Name = "logItemChanged";
            this.logItemChanged.OtherData = "";
            activitybind10.Name = "Workflow_MailingDefinition";
            activitybind10.Path = "userId";
            this.logItemChanged.MethodInvoking += new System.EventHandler(this.logItemChanged_MethodInvoking);
            this.logItemChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.logItemChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.logItemChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            // 
            // onWorkflowItemChanged
            // 
            this.onWorkflowItemChanged.AfterProperties = null;
            this.onWorkflowItemChanged.BeforeProperties = null;
            correlationtoken1.Name = "workflowToken";
            correlationtoken1.OwnerActivityName = "Workflow_MailingDefinition";
            this.onWorkflowItemChanged.CorrelationToken = correlationtoken1;
            this.onWorkflowItemChanged.Name = "onWorkflowItemChanged";
            // 
            // logManualGeneration
            // 
            this.logManualGeneration.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logManualGeneration.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind11.Name = "Workflow_MailingDefinition";
            activitybind11.Path = "logHistoryDescription";
            activitybind12.Name = "Workflow_MailingDefinition";
            activitybind12.Path = "logHistoryOutcome";
            this.logManualGeneration.Name = "logManualGeneration";
            this.logManualGeneration.OtherData = "";
            activitybind13.Name = "Workflow_MailingDefinition";
            activitybind13.Path = "userId";
            this.logManualGeneration.MethodInvoking += new System.EventHandler(this.logManualGeneration_MethodInvoking);
            this.logManualGeneration.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.logManualGeneration.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            this.logManualGeneration.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            // 
            // whileActivity1
            // 
            this.whileActivity1.Activities.Add(this.sequenceActivity1);
            ruleconditionreference1.ConditionName = "AutomaticGeneration";
            this.whileActivity1.Condition = ruleconditionreference1;
            this.whileActivity1.Name = "whileActivity1";
            // 
            // eventDrivenActivity1
            // 
            this.eventDrivenActivity1.Activities.Add(this.onWorkflowItemChanged);
            this.eventDrivenActivity1.Activities.Add(this.logItemChanged);
            this.eventDrivenActivity1.Activities.Add(this.terminateActivity1);
            this.eventDrivenActivity1.Name = "eventDrivenActivity1";
            // 
            // ifElseBranchActivity2
            // 
            this.ifElseBranchActivity2.Activities.Add(this.logManualGeneration);
            this.ifElseBranchActivity2.Name = "ifElseBranchActivity2";
            // 
            // ifElseBranchActivity1
            // 
            this.ifElseBranchActivity1.Activities.Add(this.whileActivity1);
            ruleconditionreference2.ConditionName = "AutomaticGeneration";
            this.ifElseBranchActivity1.Condition = ruleconditionreference2;
            this.ifElseBranchActivity1.Name = "ifElseBranchActivity1";
            // 
            // eventHandlersActivity1
            // 
            this.eventHandlersActivity1.Activities.Add(this.eventDrivenActivity1);
            this.eventHandlersActivity1.Name = "eventHandlersActivity1";
            // 
            // ifElseActivity1
            // 
            this.ifElseActivity1.Activities.Add(this.ifElseBranchActivity1);
            this.ifElseActivity1.Activities.Add(this.ifElseBranchActivity2);
            this.ifElseActivity1.Name = "ifElseActivity1";
            // 
            // logWorkflowTerminating
            // 
            this.logWorkflowTerminating.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logWorkflowTerminating.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind14.Name = "Workflow_MailingDefinition";
            activitybind14.Path = "logHistoryDescription";
            activitybind15.Name = "Workflow_MailingDefinition";
            activitybind15.Path = "logHistoryOutcome";
            this.logWorkflowTerminating.Name = "logWorkflowTerminating";
            this.logWorkflowTerminating.OtherData = "";
            activitybind16.Name = "Workflow_MailingDefinition";
            activitybind16.Path = "userId";
            this.logWorkflowTerminating.MethodInvoking += new System.EventHandler(this.logWorkflowTerminating_MethodInvoking);
            this.logWorkflowTerminating.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            this.logWorkflowTerminating.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            this.logWorkflowTerminating.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            // 
            // eventHandlingScopeActivity1
            // 
            this.eventHandlingScopeActivity1.Activities.Add(this.ifElseActivity1);
            this.eventHandlingScopeActivity1.Activities.Add(this.eventHandlersActivity1);
            this.eventHandlingScopeActivity1.Name = "eventHandlingScopeActivity1";
            // 
            // logWorkflowActivated
            // 
            this.logWorkflowActivated.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logWorkflowActivated.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind17.Name = "Workflow_MailingDefinition";
            activitybind17.Path = "logHistoryDescription";
            activitybind18.Name = "Workflow_MailingDefinition";
            activitybind18.Path = "logHistoryOutcome";
            this.logWorkflowActivated.Name = "logWorkflowActivated";
            this.logWorkflowActivated.OtherData = "";
            activitybind19.Name = "Workflow_MailingDefinition";
            activitybind19.Path = "userId";
            this.logWorkflowActivated.MethodInvoking += new System.EventHandler(this.logWorkflowActivated_MethodInvoking);
            this.logWorkflowActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind19)));
            this.logWorkflowActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            this.logWorkflowActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
            activitybind21.Name = "Workflow_MailingDefinition";
            activitybind21.Path = "workflowId";
            // 
            // onWorkflowActivated
            // 
            this.onWorkflowActivated.CorrelationToken = correlationtoken1;
            this.onWorkflowActivated.EventName = "OnWorkflowActivated";
            this.onWorkflowActivated.Name = "onWorkflowActivated";
            activitybind20.Name = "Workflow_MailingDefinition";
            activitybind20.Path = "workflowProperties";
            this.onWorkflowActivated.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onWorkflowActivated1_Invoked);
            this.onWorkflowActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind21)));
            this.onWorkflowActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind20)));
            // 
            // Workflow_MailingDefinition
            // 
            this.Activities.Add(this.onWorkflowActivated);
            this.Activities.Add(this.logWorkflowActivated);
            this.Activities.Add(this.eventHandlingScopeActivity1);
            this.Activities.Add(this.logWorkflowTerminating);
            this.Name = "Workflow_MailingDefinition";
            this.CanModifyActivities = false;

        }

        #endregion

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logItemChanged;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logManualGeneration;

        private TerminateActivity terminateActivity1;

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowItemChanged onWorkflowItemChanged;

        private EventDrivenActivity eventDrivenActivity1;

        private EventHandlersActivity eventHandlersActivity1;

        private EventHandlingScopeActivity eventHandlingScopeActivity1;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logWaitForDaysOffset;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logAutomaticGeneration;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logWorkflowTerminating;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logWorkflowActivated;

        private CodeActivity GenerateMailing;

        private DelayActivity WaitForDaysOffset;

        private SequenceActivity sequenceActivity1;

        private WhileActivity whileActivity1;

        private IfElseBranchActivity ifElseBranchActivity2;

        private IfElseBranchActivity ifElseBranchActivity1;

        private IfElseActivity ifElseActivity1;

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated onWorkflowActivated;




































    }
}
