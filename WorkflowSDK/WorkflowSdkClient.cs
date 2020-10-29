using System;
using System.Threading.Tasks;
using WorkflowSDK.Business;
using WorkflowSDK.Core;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.Workflow;
using WorkflowSDK.Log;

namespace WorkflowSDK
{
    public class WorkflowSdkClient : IWorkflowSdkClient
    {
        private readonly MainWorkflowManager _mainWorkflowManager;

        public IWorkflowManager WorkflowManager { get; }
        public IStepFactory StepFactory { get; }
        public ILogger Logger { get; }

        public WorkflowSdkClient(
            IWorkflowManager workflowManager,
            IStepFactory stepFactory,
            ILogger logger)
        {
            _mainWorkflowManager = new MainWorkflowManager(stepFactory, workflowManager);

            WorkflowManager = workflowManager;
            StepFactory = stepFactory;
            Logger = logger;
        }

        public async Task Start<TData, TStep>(TData data, Action<IWorkflow> onCompletedWorkflow) 
            where TData : new() 
            where TStep : Step
        {
            Logger.Log(LogLevel.Trace, "Main Workflow started.");
            var task = _mainWorkflowManager.Start<TData, TStep>(data, onCompletedWorkflow);
            await task;

            if (task.IsCompletedSuccessfully)
            {
                Logger.Log(LogLevel.Trace, "Main Workflow completed successfully.");
            }

            if (task.Exception != null)
            {
                Logger.LogFatalException(task.Exception);
            }

            if (task.IsFaulted)
            {
                Logger.LogFatalException(FatalException.GetFatalException("Main workflow stoped unexpeted"));
            }


        }
        public void Dispose()
        {
            WorkflowManager?.Dispose();
        }
    }
}
