using WorkflowSDK.Core;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Log;

namespace WorkflowSDK
{
    public interface IWorkflowSdkClient
    {
        IWorkflowManager WorkflowManager { get; }
        IStepFactory StepFactory { get; }
        ILogger Logger { get; }
    }
    public class WorkflowSdkClient : IWorkflowSdkClient
    {
        
        
    }
}
