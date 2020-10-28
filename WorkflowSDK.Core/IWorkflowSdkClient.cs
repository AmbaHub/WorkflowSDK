using System;
using WorkflowSDK.Core;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Log;

namespace WorkflowSDK
{
    public interface IWorkflowSdkClient : IDisposable
    {
        IWorkflowManager WorkflowManager { get; }
        IStepFactory StepFactory { get; }
        ILogger Logger { get; }
    }
}