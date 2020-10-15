using WorkflowSDK.Core.Model.StepModel;

namespace WorkflowSDK.Core.Model.WorkflowModel
{
    public class WorkflowState
    {
        public IWorkflow Workflow { get; set; }
        public Step Step { get; set; }
      
    }
   
}
