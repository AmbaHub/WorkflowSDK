using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.Validation;
using WorkflowSDK.Core.Model.Workflow;
using WorkflowSDK.Log;

namespace WorkflowSDK.Core.Tests.TestObjects
{
    public class TestStep : Step<TestWorkflow>
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


        protected override WorkflowState Run(TestWorkflow workflow)
        {
            throw new System.NotImplementedException();
        }
    }
}
