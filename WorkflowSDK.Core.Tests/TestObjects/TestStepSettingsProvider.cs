using System;
using System.Collections.Generic;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.DI;

namespace WorkflowSDK.Core.Tests.TestObjects
{
    public class TestStepSettingsProvider : IStepSettingsProvider
    {
        private readonly Dictionary<Type, StepSettings> _stepSettingsDictionary;
        public TestStepSettingsProvider(Dictionary<Type, StepSettings> stepSettingsDictionary)
        {
            _stepSettingsDictionary = stepSettingsDictionary;
        }
        public StepSettings GetStepSettings<T>() where T : Step
        {
            return _stepSettingsDictionary[typeof(T)];
        }
    }
}
