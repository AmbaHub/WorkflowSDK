using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowSDK.Core.Model.Validation;

namespace WorkflowSDK.Core.Model.DI
{
    public class StepDependencyProvider : IStepDependencyProvider, IStepSettingsProvider, IWorkflowValidatorProvider
    {
        private readonly List<StepDependencyPack> _dependencyPacks;

        public StepDependencyProvider(List<StepDependencyPack> dependencyPacks)
        {
            _dependencyPacks = dependencyPacks;
        }
        public object[] GetStepDependencies<T>() where T : Step => GetStepDependencyPack<T>().StepDependencies;
        public StepSettings GetStepSettings<T>() where T : Step => GetStepDependencyPack<T>().StepSettings;
        public WorkflowDataValidator[] GetValidators<T>() where T : Step => GetStepDependencyPack<T>().WorkflowDataValidators;

        private StepDependencyPack GetStepDependencyPack<T>() where T : Step
        {
            var type = typeof(T);
            var pack = _dependencyPacks.SingleOrDefault(x => x.StepType == type);

            if (pack == null)
            {
                throw FatalException.GetFatalException(string.Empty);
            }

            return pack;
        }
    }

}
