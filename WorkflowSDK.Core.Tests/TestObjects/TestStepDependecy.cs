using System;
using WorkflowSDK.Core.Model.Workflow;

namespace WorkflowSDK.Core.Tests.TestObjects
{
    public class TestStepDependency
    {
        public Func<Workflow<TestDataClass>,WorkflowState> StepAction { get; set; }
    }
}
