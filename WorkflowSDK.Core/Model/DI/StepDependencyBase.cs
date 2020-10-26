using System.Collections.Generic;
using System.Linq;

namespace WorkflowSDK.Core.Model.DI
{
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