using System;
using System.Runtime.Serialization;
using WorkflowSDK.Core.Model.Validation;

namespace WorkflowSDK.Core.Model.DI
{
    public class StepDependencyPack
    {
        public Type StepType { get; protected internal set; }
        public object[] StepDependencies { get; set; }
        public StepSettings StepSettings { get; set; }
        public WorkflowDataValidator[] WorkflowDataValidators { get; set; }
    }

    public class StepDependencyPack<T> : StepDependencyPack where T : Step
    {
        public StepDependencyPack()
        {
            StepType = typeof(T);
        }
    }



}