using System.Collections.Generic;
using System.Linq;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.Workflow;

namespace WorkflowSDK.Core
{
    public class WorkflowManager : IWorkflowManager
    {
        private readonly Dictionary<string, IWorkflow> _workflows = new Dictionary<string, IWorkflow>();
        public IEnumerable<IWorkflow> AllWorkflows => _workflows.Select(x => x.Value);
        public IWorkflow<T> CreateWorkflow<T>(T workflowData) where T : new() => CreateWorkflow(workflowData, null);
        public IWorkflow<T> CreateWorkflow<T>(T workflowData, string key) where T : new()
        {
            FatalException.ArgumentNullException(workflowData, nameof(workflowData));

            var wf = new Workflow<T>
            {
                WorkflowData = workflowData
            };
            key = workflowData.GetType().GenerateKey(key);
            _workflows.Add(key, wf);
            return wf;
        }
        public IWorkflow<T> GetWorkflow<T>() where T : new() => GetWorkflow<T>(null);
        public IWorkflow<T> GetWorkflow<T>(string key) where T : new()
        {
            key = typeof(T).GenerateKey(key);

            if (_workflows.TryGetValue(key, out var value))
                if (value is IWorkflow<T> wf)
                    return wf;
                else
                    throw FatalException.GetFatalException(string.Empty);

            throw FatalException.GetFatalException(string.Empty);
        }

        public void RemoveWorkflow<TF, TD>() where TF : IWorkflow<TD> where TD : new() => RemoveWorkflow<TF, TD>(null);
        public void RemoveWorkflow<TF, TD>(string key)
            where TF : IWorkflow<TD>
            where TD : new()
        {
            key = typeof(TD).GenerateKey(key);

            if (_workflows.TryGetValue(key, out var workflow))
            {
                if (workflow is TF)
                    _workflows.Remove(key);
                else
                    throw FatalException.GetFatalException(string.Empty);
            }
            else
                throw FatalException.GetFatalException(string.Empty);
        }

        public void Dispose()
        {
            foreach (var (k, v) in _workflows)
                v.Dispose();

            _workflows.Clear();
        }
    }
}
