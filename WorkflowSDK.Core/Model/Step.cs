using System;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSDK.Core.Model.DI;
using WorkflowSDK.Core.Model.Validation;
using WorkflowSDK.Core.Model.Workflow;
using WorkflowSDK.Log;

namespace WorkflowSDK.Core.Model
{
    public abstract class Step
    {
        private readonly WorkflowDataValidator[] _workflowDataValidators;
        private readonly ILogger _logger;
        public StepSettings StepSettings { get; }

        //todo - refactor constructor
        protected internal Step(
            StepSettings stepSettings,
            ILogger logger,
            WorkflowDataValidator[] workflowDataValidators)
        {
            _workflowDataValidators = workflowDataValidators;
            _logger = logger;
            StepSettings = stepSettings;
        }
        protected abstract IWorkflow Run(IWorkflow workflow);

        internal async Task<IWorkflow> RunAsync(IWorkflow workflow) //todo implement with cancellation token
        {
            FatalException.ArgumentNullException(workflow, nameof(workflow));

            return await Task.Run(() =>
                InvokeFunction(
                    () =>
                {
                    _logger.Log(workflow);

                    if (Validate(workflow))
                        return Run(workflow);

                    if (workflow.WorkflowStatus.Next == null)
                        return null;

                    workflow.WorkflowStatus.Next.StepSettings.ExitFromFlow = true;
                    return workflow;
                },
                () =>
                {
                    if (StepSettings.RunPreviousOnError)
                        return workflow.RunPrevious().Result;

                    if (StepSettings.OnFailedStep != null)
                        Task.Run(() => InvokeAction(StepSettings.OnFailedStep));

                    return null;
                }));
        }
        private bool Validate(IWorkflow workflow)
        {
            var isValid = true;
            if (_workflowDataValidators == null || _workflowDataValidators.Any(v => v == null))
                return true;

            foreach (var validator in _workflowDataValidators)
            {
                if (StepSettings.ExitOnFirstFailedValidation && !isValid) break;
                if (validator.IsValid(workflow)) continue;

                isValid = false;

                if (StepSettings.ThrowOnFailedValidation)
                    throw FatalException.GetFatalException(string.Empty);

                if (validator.OnValidationFailed == null) continue;

                InvokeAction(() => validator.OnValidationFailed.Invoke(workflow));
            }

            return isValid;
        }
        private void InvokeAction(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (FatalException fe)
            {
                _logger.LogFatalException(fe);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                if (StepSettings.ThrowOnError) throw;
            }

        }
        private T InvokeFunction<T>(Func<T> action, Func<T> onErrorAction)
        {
            try
            {
                return action.Invoke();
            }
            catch (FatalException fe)
            {
                _logger.LogFatalException(fe);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                onErrorAction?.Invoke();
                if (StepSettings.ThrowOnError) throw;
            }

            return default;
        }
    }

    public abstract class Step<T> : Step where T : IWorkflow
    {
        protected Step(StepSettings stepSettings, ILogger logger, WorkflowDataValidator[] workflowDataValidators) 
            : base(stepSettings, logger, workflowDataValidators)
        {

        }

        protected abstract (IWorkflow workflow, Step next) Run(T workflow);
        protected sealed override IWorkflow Run(IWorkflow workflow)
        {
            var (wf, next) = Run((T)workflow);
            wf.WorkflowStatus.Next = next;
            return wf;
        }




    }
}
