using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.StepModel;
using WorkflowSDK.Core.Model.Validation;
using WorkflowSDK.Core.Model.WorkflowModel;
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
            IStepFactory stepFactory,
            WorkflowDataValidator[] workflowDataValidators) : 
            base(stepSettings, logger, stepFactory, workflowDataValidators)
        {
            _testStepDependency = testStepDependency;
        }


        protected override WorkflowState Run(TestWorkflow workflow)
        {
            throw new System.NotImplementedException();
        }
    }
}
