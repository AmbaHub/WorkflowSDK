using System;
using WorkflowSDK.Core.Model.Workflow;

namespace WorkflowSDK.Core.Model.Validation
{
    public abstract class WorkflowDataValidator
    {
        public abstract bool IsValid(IWorkflow workflow);
        public abstract Action<IWorkflow> OnValidationFailed { get; }
    }
    public class WorkflowDataValidator<T> : WorkflowDataValidator where T : IWorkflow, new()
    {
        private readonly Func<T, bool> _validationFunction;
        private readonly Action<T> _onFailedValidation;

        public WorkflowDataValidator(Func<T, bool> validationFunction, Action<T> onFailedValidation)
        {
            _validationFunction = validationFunction;
            _onFailedValidation = onFailedValidation;
        }

        public WorkflowDataValidator(Func<T, bool> validationFunction)
        {
            _validationFunction = validationFunction;
            _onFailedValidation = null;
        }

        public sealed override Action<IWorkflow> OnValidationFailed => wf =>
        {
            if (wf is T w)
                _onFailedValidation?.Invoke(w);
        };

        public sealed override bool IsValid(IWorkflow workflow)
        {
            if (workflow is T flow)
                return _validationFunction?.Invoke(flow) ?? false;
            return false;
        }
    }
}
