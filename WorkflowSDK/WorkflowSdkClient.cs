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
    public class WorkflowSdkClient : IWorkflowSdkClient
    {
        public IWorkflowManager WorkflowManager { get; }
        public IStepFactory StepFactory { get; }
        public ILogger Logger { get; }

        public WorkflowSdkClient(IWorkflowManager workflowManager, IStepFactory stepFactory, ILogger logger)
        {
            WorkflowManager = workflowManager;
            StepFactory = stepFactory;
            Logger = logger;
        }

        public void Dispose()
        {
            WorkflowManager?.Dispose();
        }
    }
}
