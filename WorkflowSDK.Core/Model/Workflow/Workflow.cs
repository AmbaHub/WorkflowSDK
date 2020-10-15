using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowSDK.Core.Model.Workflow
{
    public abstract class Workflow : IWorkflow
    {
        protected internal object WorkflowDataObject { get; set; }
        protected internal Dictionary<string, Task<IWorkflow>> MemoryProcesses { get; set; }

        public WorkflowStatus WorkflowStatus { get; }

        protected internal Workflow()
        {
            MemoryProcesses = new Dictionary<string, Task<IWorkflow>>();
            WorkflowStatus = new WorkflowStatus();
        }
        public void RunInBackground(Step step, string key = null)
        {
            key = step.GetType().GenerateKey(key);

            if (MemoryProcesses.ContainsKey(key))
                FatalException.ArgumentException($"Key already exists for background process : {key}");

            var flow = this.RunWorkflowAsync(step);

            MemoryProcesses.Add(key, flow);
        }

        public async Task<TFlow> AwaitProcessAsync<TFlow, TStep>(string key = null) 
            where TFlow : IWorkflow 
            where TStep : Step<TFlow>
        {
            key = typeof(TStep).GenerateKey(key);

            if (MemoryProcesses.TryGetValue(key, out var value))
            {
                return value is Task<TFlow> task
                    ? await task
                    : throw FatalException.GetFatalException("");
            }

            throw FatalException.GetFatalException(""); ;
        }

        public async Task<TFlow> Run<TFlow, TStep>(TStep step)
            where TFlow : IWorkflow
            where TStep : Step<TFlow>
        {
            var result = await this.RunWorkflowAsync(step);
            if (result is TFlow correctResultType)
                return correctResultType;

            throw FatalException.GetFatalException("");
        }

        public async Task<IWorkflow> RunPrevious()
        {
            using (this)
            {
                var step = WorkflowStatus.Previous.Step;
                var workFlow = WorkflowStatus.Previous.Workflow;

                return await workFlow.RunWorkflowAsync(step);
            }
        }

        public IWorkflow Clone() => (Workflow)MemberwiseClone();
        public virtual void Dispose()
        {
            foreach (var (key, process) in MemoryProcesses)
                process.Dispose();

            MemoryProcesses.Clear();
            var content = WorkflowDataObject.GetContent();

            foreach (var c in content)
                if (c.Value is IDisposable disposable)
                    disposable.Dispose();

            if (WorkflowDataObject is IDisposable d) 
                d.Dispose();
        }
    }

    public abstract class Workflow<T> : Workflow, IWorkflow<T> where T : new()
    {
        public T WorkflowData
        {
            get => (T) WorkflowDataObject;
            set => WorkflowDataObject = value;
        }
        public virtual void CopyTo<TF>(IWorkflow<TF> other, bool overwrite) 
            where TF : new()
        {
            FatalException.ArgumentNullException(other, nameof(other));

            if (other is Workflow wf) 
                wf.MemoryProcesses = MemoryProcesses;

            other.WorkflowData.SetContent(WorkflowData.GetContent(), overwrite);
        }

        public virtual IWorkflow<TF> As<TF>() where TF : new()
        {
            using (this)
            {
                var wf = InternalExtensions.CreateInstance<IWorkflow<TF>>();
                wf.WorkflowData = new TF();
                CopyTo(wf,true);
                return wf;
            }
        }
      
    }
}
