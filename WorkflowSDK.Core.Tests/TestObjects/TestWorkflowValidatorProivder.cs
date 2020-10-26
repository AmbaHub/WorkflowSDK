using System;
using System.Collections.Generic;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.Validation;

namespace WorkflowSDK.Core.Tests.TestObjects
{
    public class TestWorkflowValidatorProvider : IWorkflowValidatorProvider
    {
        private readonly Dictionary<Type, WorkflowDataValidator[]> _dataValidators;

        public TestWorkflowValidatorProvider(Dictionary<Type, WorkflowDataValidator[]> dataValidators)
        {
            _dataValidators = dataValidators;
        }
        public WorkflowDataValidator[] GetValidators<T>() where T : Step
        {
            return _dataValidators[typeof(T)];
        }

       
    }
}
