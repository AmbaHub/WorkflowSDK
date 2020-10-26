using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace WorkflowSDK.Core.Model.DI
{
    public class StepDependencyProvider : StepDependencyBase, IStepDependencyProvider
    {
        public StepDependencyProvider(List<StepDependencyPack> stepDependencyPacks) : base(stepDependencyPacks)
        {

        }

        public object[] GetStepDependencies<T>() where T : Step => GetStepDependencyPack<T>().StepDependencies;
    }
}
