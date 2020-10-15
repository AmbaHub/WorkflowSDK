namespace WorkflowSDK.Core.Model.Workflow
{
    public class WorkflowStatus
    {
        public Step Next { get; internal set; }
        public WorkflowState Current { get; } = new WorkflowState();
        public WorkflowState Previous { get; } = new WorkflowState();
    }
}