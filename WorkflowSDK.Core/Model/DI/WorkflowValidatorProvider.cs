using System.Collections.Generic;
using WorkflowSDK.Core.Model.Validation;

namespace WorkflowSDK.Core.Model.DI
{
    public class WorkflowValidatorProvider : StepDependencyBase, IWorkflowValidatorProvider
    {
        public WorkflowValidatorProvider(List<StepDependencyPack> stepDependencyPacks) : base(stepDependencyPacks)
        {

        }

        public WorkflowDataValidator[] GetValidators<T>() where T : Step => GetStepDependencyPack<T>().WorkflowDataValidators;
    }
}