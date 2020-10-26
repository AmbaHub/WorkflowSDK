using System;
using System.Globalization;
using NUnit.Framework;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.Workflow;
using WorkflowSDK.Core.Tests.TestObjects;

namespace WorkflowSDK.Core.Tests
{
    [TestFixture]
    public class SmokeTest
    {
        private IWorkflowSdkClient WorkflowSdkClient { get; set; }
        [OneTimeSetUp]
        public void Setup()
        {
            WorkflowSdkClient = TestDependencyInjection.GetClientForSmokeTests();
        }
        [Test]
        public void Test()
        {
            var testData = new TestDataClass {Data = DateTime.Today.ToString(CultureInfo.InvariantCulture)};
            var wf = WorkflowSdkClient.WorkflowManager.CreateWorkflow(testData);
            var step = WorkflowSdkClient.StepFactory.Build<TestStep>();

            var flow = wf.Run<IWorkflow<TestDataClass>, TestStep>(step).Result;
        }
    }
}
