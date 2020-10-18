using WorkflowSDK.Core;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Log;

namespace WorkflowSDK
{
    public interface IWorkflowSdkClient
    {
        IWorkflowManager WorkflowManager { get; }
        IStepFactory StepFactory { get; }
    }
    public class WorkflowSdkClient : IWorkflowSdkClient
    {
        public IWorkflowManager WorkflowManager { get; }
        public IStepFactory StepFactory { get; }

        public WorkflowSdkClient(IWorkflowManager workflowManager, IStepFactory stepFactory)
        {
            WorkflowManager = workflowManager;
            StepFactory = stepFactory;
        }

        public WorkflowSdkClient(string jsonFilePath)
        {
            WorkflowManager = new WorkflowManager();
            var stepDependencyProvider = StepDependencyProviderFileManager.Load(jsonFilePath);
            var logger = new Logger();
            StepFactory = new StepFactory(
                logger,
                WorkflowManager,
                stepDependencyProvider,
                stepDependencyProvider,
                stepDependencyProvider);
        }
    }
}
