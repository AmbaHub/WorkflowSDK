using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WorkflowSDK.Core.Model.DI;

namespace WorkflowSDK
{
    public static class StepDependencyProviderFileManager
    {
        public static StepDependencyProvider Load(string filePath)
        {
            var json = JsonConvert.DeserializeObject<IEnumerable<StepDependencyPack>>(filePath).ToList();
            return new StepDependencyProvider(json);
        }

        public static void Save(this StepDependencyProvider provider, string filePath)
        {
            var json = JsonConvert.SerializeObject(provider.DependencyPacks);
            File.WriteAllText(filePath, json);
        }
    }

}
