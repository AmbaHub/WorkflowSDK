using WorkflowSDK.Core.Model.Validation;

namespace WorkflowSDK.Core.Model.DI
{
    public interface IWorkflowValidatorProvider
    {
        WorkflowDataValidator[] GetValidators<T>() where T : Step;
    }
}
