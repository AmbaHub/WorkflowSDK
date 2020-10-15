using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.StepModel;

namespace WorkflowSDK.Core.Model.WorkflowModel
{
    public class WorkflowStatus
    {
        public Step Next { get; internal set; }
        public WorkflowState Current { get; } = new WorkflowState();
        public WorkflowState Previous { get; } = new WorkflowState();
        
    }
}