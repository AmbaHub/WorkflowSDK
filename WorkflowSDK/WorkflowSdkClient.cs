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
        private readonly ILogger _logger;

        public WorkflowSdkClient(
            IWorkflowManager workflowManager,
            IStepFactory stepFactory,
            ILogger logger)
        {
            _logger = logger;
            _mainWorkflowManager = new MainWorkflowManager(stepFactory, workflowManager);
        }

        public async Task Start<TData, TStep>(TData data) 
            where TData : new() 
            where TStep : Step
        {
            _logger.Log(LogLevel.Info, "Main Workflow started without result handling.");
            await Start<TData, TStep>(data, wf => { });
        }

        public async Task Start<TData, TStep>(TData data, Action<object> onCompletedWorkflow) 
            where TData : new() 
            where TStep : Step
        {
            _logger.Log(LogLevel.Trace, "Main Workflow started.");
            var task = _mainWorkflowManager.Start<TData, TStep>(data, onCompletedWorkflow);
            await task;

            if (task.IsCompletedSuccessfully)
            {
                _logger.Log(LogLevel.Trace, "Main Workflow completed successfully.");
            }

            if (task.IsCanceled)
            {
                _logger.Log(LogLevel.Warning & LogLevel.Trace, "Main Workflow was canceled.");
            }

            if (task.Exception != null)
            {
                _logger.LogFatalException(task.Exception);
            }

            if (task.IsFaulted)
            {
                _logger.LogFatalException(FatalException.GetFatalException("Main workflow stopped unexpected."));
            }

        }
        public void Dispose()
        {
            _mainWorkflowManager?.Dispose();
        }
    }
}
