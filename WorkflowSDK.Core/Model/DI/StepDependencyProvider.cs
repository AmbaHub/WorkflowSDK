using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using WorkflowSDK.Core.Model.Validation;

namespace WorkflowSDK.Core.Model.DI
{
    public class StepDependencyProvider :
        IStepDependencyProvider,
        IStepSettingsProvider,
        IWorkflowValidatorProvider
    {
        public readonly List<StepDependencyPack> DependencyPacks;

        public StepDependencyProvider(List<StepDependencyPack> dependencyPacks)
        {
            DependencyPacks = dependencyPacks;
        }
        public object[] GetStepDependencies<T>() where T : Step => GetStepDependencyPack<T>().StepDependencies;
        public StepSettings GetStepSettings<T>() where T : Step => GetStepDependencyPack<T>().StepSettings;
        public WorkflowDataValidator[] GetValidators<T>() where T : Step => GetStepDependencyPack<T>().WorkflowDataValidators;

        private StepDependencyPack GetStepDependencyPack<T>() where T : Step
        {
            var type = typeof(T);
            var pack = DependencyPacks.SingleOrDefault(x => x.StepType == type);

            if (pack == null)
                throw FatalException.GetFatalException(string.Empty);

            return pack;
        }

      
    }




}
