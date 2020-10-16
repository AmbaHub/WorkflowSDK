using System;
using WorkflowSDK.Core.Model.Validation;

namespace WorkflowSDK.Core.Model.DI
{
    public abstract class StepDependencyPack
    {
        internal abstract Type StepType { get; }

        public object[] StepDependencies { get; set; }
        public StepSettings StepSettings { get; set; }
        public WorkflowDataValidator[] WorkflowDataValidators { get; set; }

    }

    public class StepDependencyPack<T> : StepDependencyPack where T : Step
    {
        internal override Type StepType => typeof(T);
    }
}