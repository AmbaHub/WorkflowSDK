using System;
using System.Runtime.Serialization;
using WorkflowSDK.Core.Model.Validation;

namespace WorkflowSDK.Core.Model.DI
{
    [DataContract(Namespace = "")]
    public class StepDependencyPack
    {
        [DataMember]
        public Type StepType { get; set; }
        [DataMember]
        public object[] StepDependencies { get; set; }
        [DataMember]
        public StepSettings StepSettings { get; set; }
        [DataMember]
        public WorkflowDataValidator[] WorkflowDataValidators { get; set; }

    }

   
}