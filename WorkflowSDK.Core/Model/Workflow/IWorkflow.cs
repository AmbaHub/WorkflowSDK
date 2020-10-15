using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkflowSDK.Core.Model.Workflow
{
    public interface IWorkflow : IDisposable
    {
        WorkflowStatus WorkflowStatus { get; }
        void RunInBackground(Step step, string key = null);
        Task<TFlow> AwaitProcessAsync<TFlow, TStep>(string key = null)
            where TFlow : IWorkflow
            where TStep : Step<TFlow>;
        Task<TFlow> Run<TFlow, TStep>(TStep step)
            where TFlow : IWorkflow
            where TStep : Step<TFlow>;
        Task<IWorkflow> RunPrevious();
        IWorkflow Clone();
    }

    public interface IWorkflow<T> : IWorkflow where T : new()
    {
        T WorkflowData { get; set; }
        void CopyTo<TF>(IWorkflow<TF> other, bool overwrite) where TF : new();
        IWorkflow<TF> As<TF>() where TF : new();
    }
}