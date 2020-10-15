﻿using System;
using System.Collections.Generic;
using WorkflowSDK.Core.Model.StepModel;
using WorkflowSDK.Core.Model.WorkflowModel;
using WorkflowSDK.Log;

namespace WorkflowSDK.Core.Model.DI
{
    public interface IStepFactory
    {
        T Build<T>() where T : Step;
    }

    public class StepFactory : IStepFactory
    {
        private readonly ILogger _logger;
        private readonly IWorkflowValidatorProvider _workflowValidatorProvider;
        private readonly IStepSettingsProvider _stepSettingsProvider;
        private readonly IStepDependencyProvider _stepDependencyProvider;

        public StepFactory(
            ILogger logger, 
            IWorkflowValidatorProvider workflowValidatorProvider,
            IStepSettingsProvider stepSettingsProvider, 
            IStepDependencyProvider stepDependencyProvider)
        {
            _logger = logger;
            _workflowValidatorProvider = workflowValidatorProvider;
            _stepSettingsProvider = stepSettingsProvider;
            _stepDependencyProvider = stepDependencyProvider;
        }

        public T Build<T>() where T : Step
        {
            var parameters = new List<object>
            {
                _stepSettingsProvider.GetStepSettings<T>(),
                _logger,
                this,
                _workflowValidatorProvider.GetValidators<T>()
            };

            parameters.Insert(0,_stepDependencyProvider.GetStepDependencies<T>());

            if (Activator.CreateInstance(typeof(T), parameters.ToArray()) is T step) return step;

            throw new Exception();
        }
    }
}
