using System;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.Validation;
using WorkflowSDK.Core.Model.Workflow;
using WorkflowSDK.Log;

namespace WorkflowSDK.Core.Tests.TestObjects
{
    public class TestStep : Step<Workflow<TestDataClass>>
    {
        private readonly TestStepDependency _testStepDependency;
        public TestStep(
            TestStepDependency testStepDependency,
            StepSettings stepSettings,
            ILogger logger,
            IWorkflowManager workflowManager,
            IStepFactory stepFactory,
            WorkflowDataValidator[] workflowDataValidators) :
            base(stepSettings, logger, workflowManager, stepFactory, workflowDataValidators)
        {
            _testStepDependency = testStepDependency;
        }


        //protected override WorkflowState Run(Workflow<TestDataClass> workflow)
        //{
        //    var workflowState = _testStepDependency.StepAction?.Invoke(workflow);
        //    if (workflowState != null)
        //        workflowState.Step = this;
        //    return workflowState;
        //}
        protected override (IWorkflow workflow, Step next) Run(Workflow<TestDataClass> workflow)
        {
            throw new NotImplementedException();
        }
    }
}
