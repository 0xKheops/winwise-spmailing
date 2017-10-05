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

namespace Winwise.SPMailing.Workflows.SPMailingDefinition_Generation {
    public sealed partial class SPMailingDefinition_Generation {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent() {
            this.CanModifyActivities = true;
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken2 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind19 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind20 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind21 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind22 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind23 = new System.Workflow.ComponentModel.ActivityBind();
            this.SetManualState = new System.Workflow.Activities.SetStateActivity();
            this.SetGenerationState1 = new System.Workflow.Activities.SetStateActivity();
            this.ifElseBranchActivity6 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity5 = new System.Workflow.Activities.IfElseBranchActivity();
            this.logAutomaticGenerationWaitingState_Init = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.SetRoutingState1 = new System.Workflow.Activities.SetStateActivity();
            this.logAutomaticGenerationWaitingState_ItemChanged = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.AutomaticGenerationWaitingState_ItemChanged = new Microsoft.SharePoint.WorkflowActions.OnWorkflowItemChanged();
            this.SetGenerationState2 = new System.Workflow.Activities.SetStateActivity();
            this.logAutomaticGenerationWaitingState_DelayElapsed = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.WaitForDelay = new System.Workflow.Activities.DelayActivity();
            this.ifElseActivity3 = new System.Workflow.Activities.IfElseActivity();
            this.UpdateWorkflow = new System.Workflow.Activities.CodeActivity();
            this.SetRoutingState2 = new System.Workflow.Activities.SetStateActivity();
            this.logManualGenerationState_ItemChanged = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.ManualGenerationState_onWorkflowItemChanged = new Microsoft.SharePoint.WorkflowActions.OnWorkflowItemChanged();
            this.logManualGenerationState_Init = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.SetWaitingState = new System.Workflow.Activities.SetStateActivity();
            this.GenerateMailing = new System.Workflow.Activities.CodeActivity();
            this.logAutomaticGenerationState_Init = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateActivity1 = new System.Workflow.Activities.SetStateActivity();
            this.logWorkflowActivated = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.GenerationInitialState_onWorkflowActivated = new Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated();
            this.AutomaticGenerationWaitingState_Init = new System.Workflow.Activities.StateInitializationActivity();
            this.AutomaticGenerationWaitingState_ItemChangedEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.AutomaticGenerationWaitingState_DelayEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.GenerationRoutingState_Init = new System.Workflow.Activities.StateInitializationActivity();
            this.ManualGenerationState_ItemChangedEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.ManualGenerationState_Init = new System.Workflow.Activities.StateInitializationActivity();
            this.AutomaticGenerationState_Init = new System.Workflow.Activities.StateInitializationActivity();
            this.GenerationInitialState_Activation = new System.Workflow.Activities.EventDrivenActivity();
            this.AutomaticGenerationWaitingState = new System.Workflow.Activities.StateActivity();
            this.GenerationRoutingState = new System.Workflow.Activities.StateActivity();
            this.ManualGenerationState = new System.Workflow.Activities.StateActivity();
            this.AutomaticGenerationState = new System.Workflow.Activities.StateActivity();
            this.GenerationInitialState = new System.Workflow.Activities.StateActivity();
            // 
            // SetManualState
            // 
            this.SetManualState.Name = "SetManualState";
            this.SetManualState.TargetStateName = "ManualGenerationState";
            // 
            // SetGenerationState1
            // 
            this.SetGenerationState1.Name = "SetGenerationState1";
            this.SetGenerationState1.TargetStateName = "AutomaticGenerationState";
            // 
            // ifElseBranchActivity6
            // 
            this.ifElseBranchActivity6.Activities.Add(this.SetManualState);
            this.ifElseBranchActivity6.Name = "ifElseBranchActivity6";
            // 
            // ifElseBranchActivity5
            // 
            this.ifElseBranchActivity5.Activities.Add(this.SetGenerationState1);
            ruleconditionreference1.ConditionName = "IsAutomaticGeneration";
            this.ifElseBranchActivity5.Condition = ruleconditionreference1;
            this.ifElseBranchActivity5.Name = "ifElseBranchActivity5";
            // 
            // logAutomaticGenerationWaitingState_Init
            // 
            this.logAutomaticGenerationWaitingState_Init.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logAutomaticGenerationWaitingState_Init.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind1.Name = "SPMailingDefinition_Generation";
            activitybind1.Path = "logHistoryDescription";
            activitybind2.Name = "SPMailingDefinition_Generation";
            activitybind2.Path = "logHistoryOutcome";
            this.logAutomaticGenerationWaitingState_Init.Name = "logAutomaticGenerationWaitingState_Init";
            this.logAutomaticGenerationWaitingState_Init.OtherData = "";
            activitybind3.Name = "SPMailingDefinition_Generation";
            activitybind3.Path = "userId";
            this.logAutomaticGenerationWaitingState_Init.MethodInvoking += new System.EventHandler(this.logAutomaticGenerationWaitingState_Init_MethodInvoking);
            this.logAutomaticGenerationWaitingState_Init.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.logAutomaticGenerationWaitingState_Init.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.logAutomaticGenerationWaitingState_Init.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // SetRoutingState1
            // 
            this.SetRoutingState1.Name = "SetRoutingState1";
            this.SetRoutingState1.TargetStateName = "GenerationRoutingState";
            // 
            // logAutomaticGenerationWaitingState_ItemChanged
            // 
            this.logAutomaticGenerationWaitingState_ItemChanged.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logAutomaticGenerationWaitingState_ItemChanged.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind4.Name = "SPMailingDefinition_Generation";
            activitybind4.Path = "logHistoryDescription";
            activitybind5.Name = "SPMailingDefinition_Generation";
            activitybind5.Path = "logHistoryOutcome";
            this.logAutomaticGenerationWaitingState_ItemChanged.Name = "logAutomaticGenerationWaitingState_ItemChanged";
            this.logAutomaticGenerationWaitingState_ItemChanged.OtherData = "";
            activitybind6.Name = "SPMailingDefinition_Generation";
            activitybind6.Path = "userId";
            this.logAutomaticGenerationWaitingState_ItemChanged.MethodInvoking += new System.EventHandler(this.logAutomaticGenerationWaitingState_ItemChanged_MethodInvoking);
            this.logAutomaticGenerationWaitingState_ItemChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.logAutomaticGenerationWaitingState_ItemChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.logAutomaticGenerationWaitingState_ItemChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // AutomaticGenerationWaitingState_ItemChanged
            // 
            this.AutomaticGenerationWaitingState_ItemChanged.AfterProperties = null;
            this.AutomaticGenerationWaitingState_ItemChanged.BeforeProperties = null;
            correlationtoken1.Name = "workflowToken";
            correlationtoken1.OwnerActivityName = "SPMailingDefinition_Generation";
            this.AutomaticGenerationWaitingState_ItemChanged.CorrelationToken = correlationtoken1;
            this.AutomaticGenerationWaitingState_ItemChanged.Name = "AutomaticGenerationWaitingState_ItemChanged";
            // 
            // SetGenerationState2
            // 
            this.SetGenerationState2.Name = "SetGenerationState2";
            this.SetGenerationState2.TargetStateName = "AutomaticGenerationState";
            // 
            // logAutomaticGenerationWaitingState_DelayElapsed
            // 
            this.logAutomaticGenerationWaitingState_DelayElapsed.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logAutomaticGenerationWaitingState_DelayElapsed.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind7.Name = "SPMailingDefinition_Generation";
            activitybind7.Path = "logHistoryDescription";
            activitybind8.Name = "SPMailingDefinition_Generation";
            activitybind8.Path = "logHistoryOutcome";
            this.logAutomaticGenerationWaitingState_DelayElapsed.Name = "logAutomaticGenerationWaitingState_DelayElapsed";
            this.logAutomaticGenerationWaitingState_DelayElapsed.OtherData = "";
            activitybind9.Name = "SPMailingDefinition_Generation";
            activitybind9.Path = "userId";
            this.logAutomaticGenerationWaitingState_DelayElapsed.MethodInvoking += new System.EventHandler(this.logAutomaticGenerationWaitingState_DelayElapsed_MethodInvoking);
            this.logAutomaticGenerationWaitingState_DelayElapsed.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.logAutomaticGenerationWaitingState_DelayElapsed.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.logAutomaticGenerationWaitingState_DelayElapsed.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            // 
            // WaitForDelay
            // 
            this.WaitForDelay.Name = "WaitForDelay";
            activitybind10.Name = "SPMailingDefinition_Generation";
            activitybind10.Path = "delayBetweenGenerations";
            this.WaitForDelay.SetBinding(System.Workflow.Activities.DelayActivity.TimeoutDurationProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            // 
            // ifElseActivity3
            // 
            this.ifElseActivity3.Activities.Add(this.ifElseBranchActivity5);
            this.ifElseActivity3.Activities.Add(this.ifElseBranchActivity6);
            this.ifElseActivity3.Name = "ifElseActivity3";
            // 
            // UpdateWorkflow
            // 
            this.UpdateWorkflow.Name = "UpdateWorkflow";
            this.UpdateWorkflow.ExecuteCode += new System.EventHandler(this.GenerationRoutingState_RefreshWorkflowFields_ExecuteCode);
            // 
            // SetRoutingState2
            // 
            this.SetRoutingState2.Name = "SetRoutingState2";
            this.SetRoutingState2.TargetStateName = "GenerationRoutingState";
            // 
            // logManualGenerationState_ItemChanged
            // 
            this.logManualGenerationState_ItemChanged.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logManualGenerationState_ItemChanged.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind11.Name = "SPMailingDefinition_Generation";
            activitybind11.Path = "logHistoryDescription";
            activitybind12.Name = "SPMailingDefinition_Generation";
            activitybind12.Path = "logHistoryOutcome";
            this.logManualGenerationState_ItemChanged.Name = "logManualGenerationState_ItemChanged";
            this.logManualGenerationState_ItemChanged.OtherData = "";
            activitybind13.Name = "SPMailingDefinition_Generation";
            activitybind13.Path = "userId";
            this.logManualGenerationState_ItemChanged.MethodInvoking += new System.EventHandler(this.logManualGenerationState_ItemChanged_MethodInvoking);
            this.logManualGenerationState_ItemChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.logManualGenerationState_ItemChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            this.logManualGenerationState_ItemChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            // 
            // ManualGenerationState_onWorkflowItemChanged
            // 
            this.ManualGenerationState_onWorkflowItemChanged.AfterProperties = null;
            this.ManualGenerationState_onWorkflowItemChanged.BeforeProperties = null;
            correlationtoken2.Name = "workflowToken";
            correlationtoken2.OwnerActivityName = "SPMailingDefinition_Generation";
            this.ManualGenerationState_onWorkflowItemChanged.CorrelationToken = correlationtoken2;
            this.ManualGenerationState_onWorkflowItemChanged.Name = "ManualGenerationState_onWorkflowItemChanged";
            // 
            // logManualGenerationState_Init
            // 
            this.logManualGenerationState_Init.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logManualGenerationState_Init.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind14.Name = "SPMailingDefinition_Generation";
            activitybind14.Path = "logHistoryDescription";
            activitybind15.Name = "SPMailingDefinition_Generation";
            activitybind15.Path = "logHistoryOutcome";
            this.logManualGenerationState_Init.Name = "logManualGenerationState_Init";
            this.logManualGenerationState_Init.OtherData = "";
            activitybind16.Name = "SPMailingDefinition_Generation";
            activitybind16.Path = "userId";
            this.logManualGenerationState_Init.MethodInvoking += new System.EventHandler(this.logManualGenerationState_Init_MethodInvoking);
            this.logManualGenerationState_Init.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            this.logManualGenerationState_Init.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            this.logManualGenerationState_Init.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            // 
            // SetWaitingState
            // 
            this.SetWaitingState.Name = "SetWaitingState";
            this.SetWaitingState.TargetStateName = "AutomaticGenerationWaitingState";
            // 
            // GenerateMailing
            // 
            this.GenerateMailing.Name = "GenerateMailing";
            this.GenerateMailing.ExecuteCode += new System.EventHandler(this.GenerateMailing_ExecuteCode);
            // 
            // logAutomaticGenerationState_Init
            // 
            this.logAutomaticGenerationState_Init.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logAutomaticGenerationState_Init.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind17.Name = "SPMailingDefinition_Generation";
            activitybind17.Path = "logHistoryDescription";
            activitybind18.Name = "SPMailingDefinition_Generation";
            activitybind18.Path = "logHistoryOutcome";
            this.logAutomaticGenerationState_Init.Name = "logAutomaticGenerationState_Init";
            this.logAutomaticGenerationState_Init.OtherData = "";
            activitybind19.Name = "SPMailingDefinition_Generation";
            activitybind19.Path = "userId";
            this.logAutomaticGenerationState_Init.MethodInvoking += new System.EventHandler(this.logAutomaticGenerationState_Init_MethodInvoking);
            this.logAutomaticGenerationState_Init.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            this.logAutomaticGenerationState_Init.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
            this.logAutomaticGenerationState_Init.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind19)));
            // 
            // setStateActivity1
            // 
            this.setStateActivity1.Name = "setStateActivity1";
            this.setStateActivity1.TargetStateName = "GenerationRoutingState";
            // 
            // logWorkflowActivated
            // 
            this.logWorkflowActivated.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logWorkflowActivated.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind20.Name = "SPMailingDefinition_Generation";
            activitybind20.Path = "logHistoryDescription";
            activitybind21.Name = "SPMailingDefinition_Generation";
            activitybind21.Path = "logHistoryOutcome";
            this.logWorkflowActivated.Name = "logWorkflowActivated";
            this.logWorkflowActivated.OtherData = "";
            activitybind22.Name = "SPMailingDefinition_Generation";
            activitybind22.Path = "userId";
            this.logWorkflowActivated.MethodInvoking += new System.EventHandler(this.logWorkflowActivated_MethodInvoking);
            this.logWorkflowActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind22)));
            this.logWorkflowActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind20)));
            this.logWorkflowActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind21)));
            // 
            // GenerationInitialState_onWorkflowActivated
            // 
            this.GenerationInitialState_onWorkflowActivated.CorrelationToken = correlationtoken1;
            this.GenerationInitialState_onWorkflowActivated.EventName = "OnWorkflowActivated";
            this.GenerationInitialState_onWorkflowActivated.Name = "GenerationInitialState_onWorkflowActivated";
            activitybind23.Name = "SPMailingDefinition_Generation";
            activitybind23.Path = "workflowProperties";
            this.GenerationInitialState_onWorkflowActivated.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.GenerationInitialState_onWorkflowActivated_Invoked);
            this.GenerationInitialState_onWorkflowActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind23)));
            // 
            // AutomaticGenerationWaitingState_Init
            // 
            this.AutomaticGenerationWaitingState_Init.Activities.Add(this.logAutomaticGenerationWaitingState_Init);
            this.AutomaticGenerationWaitingState_Init.Name = "AutomaticGenerationWaitingState_Init";
            // 
            // AutomaticGenerationWaitingState_ItemChangedEvent
            // 
            this.AutomaticGenerationWaitingState_ItemChangedEvent.Activities.Add(this.AutomaticGenerationWaitingState_ItemChanged);
            this.AutomaticGenerationWaitingState_ItemChangedEvent.Activities.Add(this.logAutomaticGenerationWaitingState_ItemChanged);
            this.AutomaticGenerationWaitingState_ItemChangedEvent.Activities.Add(this.SetRoutingState1);
            this.AutomaticGenerationWaitingState_ItemChangedEvent.Name = "AutomaticGenerationWaitingState_ItemChangedEvent";
            // 
            // AutomaticGenerationWaitingState_DelayEvent
            // 
            this.AutomaticGenerationWaitingState_DelayEvent.Activities.Add(this.WaitForDelay);
            this.AutomaticGenerationWaitingState_DelayEvent.Activities.Add(this.logAutomaticGenerationWaitingState_DelayElapsed);
            this.AutomaticGenerationWaitingState_DelayEvent.Activities.Add(this.SetGenerationState2);
            this.AutomaticGenerationWaitingState_DelayEvent.Name = "AutomaticGenerationWaitingState_DelayEvent";
            // 
            // GenerationRoutingState_Init
            // 
            this.GenerationRoutingState_Init.Activities.Add(this.UpdateWorkflow);
            this.GenerationRoutingState_Init.Activities.Add(this.ifElseActivity3);
            this.GenerationRoutingState_Init.Name = "GenerationRoutingState_Init";
            // 
            // ManualGenerationState_ItemChangedEvent
            // 
            this.ManualGenerationState_ItemChangedEvent.Activities.Add(this.ManualGenerationState_onWorkflowItemChanged);
            this.ManualGenerationState_ItemChangedEvent.Activities.Add(this.logManualGenerationState_ItemChanged);
            this.ManualGenerationState_ItemChangedEvent.Activities.Add(this.SetRoutingState2);
            this.ManualGenerationState_ItemChangedEvent.Name = "ManualGenerationState_ItemChangedEvent";
            // 
            // ManualGenerationState_Init
            // 
            this.ManualGenerationState_Init.Activities.Add(this.logManualGenerationState_Init);
            this.ManualGenerationState_Init.Name = "ManualGenerationState_Init";
            // 
            // AutomaticGenerationState_Init
            // 
            this.AutomaticGenerationState_Init.Activities.Add(this.logAutomaticGenerationState_Init);
            this.AutomaticGenerationState_Init.Activities.Add(this.GenerateMailing);
            this.AutomaticGenerationState_Init.Activities.Add(this.SetWaitingState);
            this.AutomaticGenerationState_Init.Name = "AutomaticGenerationState_Init";
            // 
            // GenerationInitialState_Activation
            // 
            this.GenerationInitialState_Activation.Activities.Add(this.GenerationInitialState_onWorkflowActivated);
            this.GenerationInitialState_Activation.Activities.Add(this.logWorkflowActivated);
            this.GenerationInitialState_Activation.Activities.Add(this.setStateActivity1);
            this.GenerationInitialState_Activation.Name = "GenerationInitialState_Activation";
            // 
            // AutomaticGenerationWaitingState
            // 
            this.AutomaticGenerationWaitingState.Activities.Add(this.AutomaticGenerationWaitingState_DelayEvent);
            this.AutomaticGenerationWaitingState.Activities.Add(this.AutomaticGenerationWaitingState_ItemChangedEvent);
            this.AutomaticGenerationWaitingState.Activities.Add(this.AutomaticGenerationWaitingState_Init);
            this.AutomaticGenerationWaitingState.Name = "AutomaticGenerationWaitingState";
            // 
            // GenerationRoutingState
            // 
            this.GenerationRoutingState.Activities.Add(this.GenerationRoutingState_Init);
            this.GenerationRoutingState.Name = "GenerationRoutingState";
            // 
            // ManualGenerationState
            // 
            this.ManualGenerationState.Activities.Add(this.ManualGenerationState_Init);
            this.ManualGenerationState.Activities.Add(this.ManualGenerationState_ItemChangedEvent);
            this.ManualGenerationState.Name = "ManualGenerationState";
            // 
            // AutomaticGenerationState
            // 
            this.AutomaticGenerationState.Activities.Add(this.AutomaticGenerationState_Init);
            this.AutomaticGenerationState.Name = "AutomaticGenerationState";
            // 
            // GenerationInitialState
            // 
            this.GenerationInitialState.Activities.Add(this.GenerationInitialState_Activation);
            this.GenerationInitialState.Name = "GenerationInitialState";
            // 
            // SPMailingDefinition_Generation
            // 
            this.Activities.Add(this.GenerationInitialState);
            this.Activities.Add(this.AutomaticGenerationState);
            this.Activities.Add(this.ManualGenerationState);
            this.Activities.Add(this.GenerationRoutingState);
            this.Activities.Add(this.AutomaticGenerationWaitingState);
            this.CompletedStateName = null;
            this.DynamicUpdateCondition = null;
            this.InitialStateName = "GenerationInitialState";
            this.Name = "SPMailingDefinition_Generation";
            this.CanModifyActivities = false;

        }

        #endregion

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logAutomaticGenerationWaitingState_ItemChanged;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logManualGenerationState_ItemChanged;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logAutomaticGenerationWaitingState_Init;

        private StateInitializationActivity AutomaticGenerationWaitingState_Init;

        private SetStateActivity SetWaitingState;

        private StateActivity AutomaticGenerationWaitingState;

        private SetStateActivity SetGenerationState2;

        private CodeActivity GenerateMailing;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logAutomaticGenerationWaitingState_DelayElapsed;

        private DelayActivity WaitForDelay;

        private EventDrivenActivity AutomaticGenerationWaitingState_DelayEvent;

        private SetStateActivity SetManualState;

        private SetStateActivity SetGenerationState1;

        private IfElseBranchActivity ifElseBranchActivity6;

        private IfElseBranchActivity ifElseBranchActivity5;

        private IfElseActivity ifElseActivity3;

        private CodeActivity UpdateWorkflow;

        private StateInitializationActivity GenerationRoutingState_Init;

        private StateActivity GenerationRoutingState;

        private SetStateActivity SetRoutingState1;

        private SetStateActivity SetRoutingState2;

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowItemChanged ManualGenerationState_onWorkflowItemChanged;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logManualGenerationState_Init;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logAutomaticGenerationState_Init;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logWorkflowActivated;

        private EventDrivenActivity ManualGenerationState_ItemChangedEvent;

        private StateInitializationActivity ManualGenerationState_Init;

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowItemChanged AutomaticGenerationWaitingState_ItemChanged;

        private EventDrivenActivity AutomaticGenerationWaitingState_ItemChangedEvent;

        private SetStateActivity setStateActivity1;

        private StateInitializationActivity AutomaticGenerationState_Init;

        private StateActivity ManualGenerationState;

        private StateActivity AutomaticGenerationState;

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated GenerationInitialState_onWorkflowActivated;

        private EventDrivenActivity GenerationInitialState_Activation;

        private StateActivity GenerationInitialState;




























































































    }
}
