using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowSDK.Core;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.Validation;
using WorkflowSDK.Core.Model.Workflow;
using WorkflowSDK.Log;

namespace WorkflowSDK.Business
{
    public class MainWorkflowManager
    {
        private readonly IStepFactory _stepFactory;
        private readonly IWorkflowManager _workflowManager;

        public MainWorkflowManager(
            IStepFactory stepFactory,
            IWorkflowManager workflowManager)
        {
            _stepFactory = stepFactory;
            _workflowManager = workflowManager; 
        }
        
        public async void Start<TData, TStep>(TData data, Action<IWorkflow> onCompletedWorkflow)
            where TStep : Step
            where TData : new()
        {
            var wf = _workflowManager.CreateWorkflow(data);
            var step = _stepFactory.Build<TStep>();
            var result = await wf.Run<IWorkflow, TStep>(step);

            onCompletedWorkflow.Invoke(result);
        }

    }
    
    
}
