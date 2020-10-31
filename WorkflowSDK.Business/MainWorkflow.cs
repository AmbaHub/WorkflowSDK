using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSDK.Core;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.Workflow;
using WorkflowSDK.Log;

namespace WorkflowSDK.Business
{
    public class MainWorkflowManager : IDisposable
    {
        private readonly Dictionary<Type, Step> _stepMemory = new Dictionary<Type, Step>();

        private readonly IStepFactory _stepFactory;
        private readonly IWorkflowManager _workflowManager;

        public MainWorkflowManager(
            IStepFactory stepFactory,
            IWorkflowManager workflowManager)
        {
            _stepFactory = stepFactory;
            _workflowManager = workflowManager;
        }

        public async Task Start<TData, TStep>(TData data, Action<IWorkflow> onCompletedWorkflow)
            where TStep : Step
            where TData : new()
        {
            var workflow = _workflowManager.CreateWorkflow(data);
            var step = _stepFactory.Build<TStep>();
            var result = await workflow.Run<IWorkflow, TStep>(step);

            onCompletedWorkflow.Invoke(result);
        }

        public T GetNextStep<T>() where T : Step
        {
            var type = typeof(T);
            if (_stepMemory.Any(s => s.Value is T)) 
                return (T) _stepMemory[type];

            var step = _stepFactory.Build<T>();
            _stepMemory.Add(type, step);
            return step;

        }
        public void StopWorkflow<T>(string key) where T : new()
        {
            var wf = _workflowManager.GetWorkflow<T>(key);


            if (wf != null)
            {
                wf.WorkflowStatus.Current.Step.StepSettings.ExitFromFlow = true;
                wf.Dispose();
            }
        }
        public void StopWorkflow<T>() where T : new()
        {
            var wf = _workflowManager.GetWorkflow<T>();


            if (wf != null)
            {
                wf.WorkflowStatus.Current.Step.StepSettings.ExitFromFlow = true;
                wf.Dispose();
            }
        }

        public void Dispose()
        {
            _stepMemory.Clear();
            _workflowManager?.Dispose();
        }
    }
}
