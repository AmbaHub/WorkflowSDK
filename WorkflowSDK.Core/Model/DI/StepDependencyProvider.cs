using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using WorkflowSDK.Core.Model.Validation;

namespace WorkflowSDK.Core.Model.DI
{
  
    public class WorkflowValidatorProvider : StepDependencyBase, IWorkflowValidatorProvider
    {
        public WorkflowValidatorProvider(List<StepDependencyPack> stepDependencyPacks) : base(stepDependencyPacks)
        {

        }

        public WorkflowDataValidator[] GetValidators<T>() where T : Step => GetStepDependencyPack<T>().WorkflowDataValidators;
    }

    public class StepSettingProvider : StepDependencyBase, IStepSettingsProvider
    {
        public StepSettingProvider(List<StepDependencyPack> stepDependencyPacks) : base(stepDependencyPacks)
        {

        }

        public StepSettings GetStepSettings<T>() where T : Step => GetStepDependencyPack<T>().StepSettings;
    }
    public class StepDependencyProvider : StepDependencyBase, IStepDependencyProvider
    {
        public StepDependencyProvider(List<StepDependencyPack> stepDependencyPacks) : base(stepDependencyPacks)
        {

        }

        public object[] GetStepDependencies<T>() where T : Step => GetStepDependencyPack<T>().StepDependencies;
    }
    public abstract class StepDependencyBase
    {
        private readonly List<StepDependencyPack> _stepDependencyPacks;

        protected StepDependencyBase(List<StepDependencyPack> stepDependencyPacks)
        {
            _stepDependencyPacks = stepDependencyPacks;
        }
        protected StepDependencyPack GetStepDependencyPack<T>() where T : Step
        {
            var type = typeof(T);
            var pack = _stepDependencyPacks.SingleOrDefault(x => x.StepType == type);

            if (pack == null)
                throw FatalException.GetFatalException(string.Empty);

            return pack;
        }
    }




}
