﻿using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.Validation;
using WorkflowSDK.Core.Model.Workflow;
using WorkflowSDK.Log;

namespace WorkflowSDK.Business
{
    public abstract class Instruction<T> : Step<IWorkflow<T>> where T : new()
    {
        protected MainWorkflowManager MainWorkflowManager { get; }

        protected Instruction(
            StepSettings stepSettings,
            MainWorkflowManager mainWorkflowManager,
            ILogger logger,
            WorkflowDataValidator[] workflowDataValidators) : base(stepSettings, logger, workflowDataValidators)
        {
            MainWorkflowManager = mainWorkflowManager;
        }
       
    }
}