using System.Collections.Generic;

namespace WorkflowSDK.Core.Model.DI
{
    public class StepSettingProvider : StepDependencyBase, IStepSettingsProvider
    {
        public StepSettingProvider(List<StepDependencyPack> stepDependencyPacks) : base(stepDependencyPacks)
        {

        }

        public StepSettings GetStepSettings<T>() where T : Step => GetStepDependencyPack<T>().StepSettings;
    }
}