using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkflowSDK.Core.Model.Workflow
{
    public interface IWorkflow : IDisposable
    {
        WorkflowStatus WorkflowStatus { get; }
        void RunInBackground(Step step, string key = null);
        Task<TF> AwaitProcessAsync<TF, TS>(string key = null)
            where TF : IWorkflow
            where TS : Step<TF>;
        Task<TFlow> Run<TFlow, TStep>(TStep step)
            where TFlow : IWorkflow
            where TStep : Step;
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