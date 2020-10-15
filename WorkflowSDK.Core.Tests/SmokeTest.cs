using NUnit.Framework;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.StepModel;
using WorkflowSDK.Core.Tests.TestObjects;

namespace WorkflowSDK.Core.Tests
{
    [TestFixture]
    public class SmokeTest
    {
        [Test]
        public void Test()
        {
            var logger = new TestLogger();
            var step = new TestStep(new TestStepDependency(), new StepSettings(),logger , new StepFactory(logger, ), )
        }
        
    }
}
