using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowSDK.Log;
using Autofac;

namespace WorkflowSDK.Core.Model.DI
{
    public interface IStepFactory
    {
        T Build<T>() where T : Step;
    }

    public class StepFactory : IStepFactory
    {
        private readonly ILogger _logger;
        private readonly IWorkflowManager _workflowManager;
        private readonly IWorkflowValidatorProvider _workflowValidatorProvider;
        private readonly IStepSettingsProvider _stepSettingsProvider;
        private readonly IStepDependencyProvider _stepDependencyProvider;

        public StepFactory(
            ILogger logger,
            IWorkflowManager workflowManager,
            IWorkflowValidatorProvider workflowValidatorProvider,
            IStepSettingsProvider stepSettingsProvider,
            IStepDependencyProvider stepDependencyProvider)
        {
            _logger = logger;
            _workflowManager = workflowManager;
            _workflowValidatorProvider = workflowValidatorProvider;
            _stepSettingsProvider = stepSettingsProvider;
            _stepDependencyProvider = stepDependencyProvider;
        }

        public T Build<T>() where T : Step
        {
            var type = typeof(T);

            var original = new List<object>()
            {
                _stepSettingsProvider.GetStepSettings<T>(),
                _logger,
                _workflowManager,
                this,
                _workflowValidatorProvider.GetValidators<T>(),
            };
            original.AddRange(_stepDependencyProvider.GetStepDependencies<T>());

            var parameters = type.GetConstructors()
                .Single(c => c.IsPublic)
                .GetParameters()
                .Select(x => x.ParameterType)
                .Select(t => original.SingleOrDefault(t.IsInstanceOfType))
                .ToArray();
            
            return type.CreateInstance<T>(parameters);
        }

    }

   



}
