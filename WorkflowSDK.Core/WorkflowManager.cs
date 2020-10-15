using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.Workflow;

namespace WorkflowSDK.Core
{
    public interface IWorkflowManager
    {
        IStepFactory StepFactory { get; }
        IEnumerable<IWorkflow> AllWorkflows { get; }
        IWorkflow<T> CreateWorkflow<T>(T workflowData) where T : new();
        IWorkflow<T> CreateWorkflow<T>(T workflowData, string key) where T : new();
        IWorkflow<T> GetWorkflow<T>(string key) where T : new();
    }
    public class WorkflowManager : IWorkflowManager, IDisposable
    {
        private readonly Dictionary<string, IWorkflow> _workflows = new Dictionary<string, IWorkflow>();
        public IEnumerable<IWorkflow> AllWorkflows => _workflows.Select(x => x.Value);
        public IStepFactory StepFactory { get; }

        public WorkflowManager(IStepFactory stepFactory)
        {
            StepFactory = stepFactory;
        }

        public IWorkflow<T> CreateWorkflow<T>(T workflowData) where T : new()
        {
            FatalException.ArgumentNullException(workflowData, nameof(workflowData));

            var wf = new InternalWorkflowObject<T>
            {
                WorkflowData = workflowData
            };
            var key = workflowData.GetType().GenerateKey(null);
            _workflows.Add(key, wf);
            return wf;
        }
        public IWorkflow<T> CreateWorkflow<T>(T workflowData, string key) where T : new()
        {
            FatalException.ArgumentNullException(workflowData, nameof(workflowData));

            var wf = new InternalWorkflowObject<T>
            {
                WorkflowData = workflowData
            };
            key = workflowData.GetType().GenerateKey(key);
            _workflows.Add(key, wf);
            return wf;
        }
        public IWorkflow<T> GetWorkflow<T>(string key) where T : new()
        {
            key = typeof(T).GenerateKey(key);

            if (_workflows.TryGetValue(key, out var value))
            {
                return (IWorkflow<T>)value;
            }

            return null;
        }

        public void Dispose()
        {
            foreach (var (k, v) in _workflows)
                v.Dispose();

            _workflows.Clear();
        }
    }
}
