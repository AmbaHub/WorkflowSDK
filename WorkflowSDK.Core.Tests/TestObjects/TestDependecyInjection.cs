using System;
using System.Collections.Generic;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.Validation;
using WorkflowSDK.Core.Model.Workflow;
using WorkflowSDK.Log;

namespace WorkflowSDK.Core.Tests.TestObjects
{
    public class TestDependencyInjection
    {
        public static IWorkflowSdkClient GetClientForSmokeTests()
        {
            var workflowManager = new WorkflowManager();
            var testLogger = new TestLogger();

            var testWorkflowValidatorProvider = new TestWorkflowValidatorProvider(new Dictionary<Type, WorkflowDataValidator[]>
            {
                {typeof(TestStep), new WorkflowDataValidator[] { }}
            });

            var testStepDependencyProvider = new TestStepDependencyProvider(new Dictionary<Type, object[]>
            {
                {typeof(TestStep), new object[] {
                    new TestStepDependency{
                        StepAction = wf =>
                    {
                         wf.WorkflowData.Data += "s";
                         return new WorkflowState
                         {
                             Workflow = wf,
                             Step = null
                         };

                    }
                } }}
            });

            var testStepSettingsProvider = new TestStepSettingsProvider(new Dictionary<Type, StepSettings>
            {
                {typeof(TestStep), new StepSettings()}
            });

            var stepFactory = new StepFactory(testLogger, workflowManager, testWorkflowValidatorProvider,
                testStepSettingsProvider, testStepDependencyProvider);

            return new WorkflowSdkClient(workflowManager, stepFactory, testLogger);
        }
        
    }
}
