using WorkflowSDK.Core;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Log;

namespace WorkflowSDK
{
    public class WorkflowSdkSetup
    {
        public IStepFactory StepFactory { get; }

        public WorkflowSdkSetup(StepDependencyProvider stepDependencyProvider, ILogger logger, IWorkflowManager workflowManager)
        {
            StepFactory = new StepFactory(
                logger,
                workflowManager,
                stepDependencyProvider,
                stepDependencyProvider,
                stepDependencyProvider);
        }
    }
}
