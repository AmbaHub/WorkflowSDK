using System;
using System.Collections.Generic;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;

namespace WorkflowSDK.Core.Tests.TestObjects
{
    public class TestStepDependencyProvider : IStepDependencyProvider
    {
        private readonly Dictionary<Type, object[]> _stepDependencyDictionary;

        public TestStepDependencyProvider(Dictionary<Type, object[]> stepDependencyDictionary)
        {
            _stepDependencyDictionary = stepDependencyDictionary;
        }
        public object[] GetStepDependencies<T>() where T : Step
        {
            return _stepDependencyDictionary[typeof(T)];
        }
    }
}
