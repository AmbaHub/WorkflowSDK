using System;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.Workflow;

namespace WorkflowSDK.Core.Tests.TestObjects
{
    public class TestStepDependency
    {
        public Func<Workflow<TestDataClass>, (IWorkflow workflow, Step next)> StepAction { get; set; }
    }
}
