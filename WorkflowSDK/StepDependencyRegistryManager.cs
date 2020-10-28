using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using Autofac;
using Autofac.Builder;
using Newtonsoft.Json;
using WorkflowSDK.Log;
using System.Configuration;

namespace WorkflowSDK.Core.Model.DI
{
    public class DependencyRegistryManager
    {
        private readonly ContainerBuilder _containerBuilder;

        public DependencyRegistryManager(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
          
        }

        public void RegisterDependencies(string configFilePath)
        {
            var text = File.ReadAllText(configFilePath);
            var json = JsonConvert.DeserializeObject<IEnumerable<StepDependencyPack>>(text).ToList();

            _containerBuilder.RegisterInstance(new StepDependencyProvider(json)).As<IStepDependencyProvider>();
            _containerBuilder.RegisterInstance(new WorkflowValidatorProvider(json)).As<IWorkflowValidatorProvider>();
            _containerBuilder.RegisterInstance(new StepSettingProvider(json)).As<IStepSettingsProvider>();
            _containerBuilder.RegisterInstance(new WorkflowManager()).As<IWorkflowManager>();
            _containerBuilder.RegisterInstance(new Logger()).As<ILogger>();

            _containerBuilder.RegisterType<StepFactory>().As<IStepFactory>();
            _containerBuilder.RegisterType<WorkflowSdkClient>().As<IWorkflowSdkClient>();
        }
       
    }

}
