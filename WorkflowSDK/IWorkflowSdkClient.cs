using System;
using System.Threading.Tasks;
using WorkflowSDK.Core;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.Workflow;
using WorkflowSDK.Log;

namespace WorkflowSDK
{
    public interface IWorkflowSdkClient : IDisposable
    {
        Task Start<TData, TStep>(TData data)
            where TStep : Step
            where TData : new();
        Task Start<TData, TStep>(TData data, Action<IWorkflow> onCompletedWorkflow)
                where TStep : Step
                where TData : new();
    }
}