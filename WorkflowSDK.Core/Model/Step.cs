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
        protected readonly ILogger Logger;
        protected readonly IStepFactory StepFactory;
        protected readonly IWorkflowManager WorkflowManager;
        public StepSettings StepSettings { get; }

        protected internal Step(StepSettings stepSettings, ILogger logger, IWorkflowManager workflowManager, IStepFactory stepFactory, WorkflowDataValidator[] workflowDataValidators)
        {
            WorkflowManager = workflowManager;
            _workflowDataValidators = workflowDataValidators;
            Logger = logger;
            StepFactory = stepFactory;
            StepSettings = stepSettings;
        }
        protected abstract IWorkflow Run(IWorkflow workflow);

        internal async Task<IWorkflow> RunAsync(IWorkflow workflow)
        {
            FatalException.ArgumentNullException(workflow, nameof(workflow));

            return await Task.Run(() =>
                InvokeFunction(() =>
                {
                    Logger.Log(workflow);

                    if (Validate(workflow))
                        return Run(workflow);

                    if (workflow.WorkflowStatus.Next != null)
                        workflow.WorkflowStatus.Next.StepSettings.ExitFromFlow = true;
                    else
                        return null;
                    return workflow;
                },
                () =>
                {
                    if (StepSettings.RunPreviousOnError)
                        return workflow.RunPrevious().Result;

                    if (StepSettings.OnFailedStep != null)
                        Task.Run(() => InvokeAction(() => StepSettings.OnFailedStep.Invoke()));

                    return null;
                }));
        }
        private bool Validate(IWorkflow workflow)
        {
            var isValid = true;
            if (_workflowDataValidators == null || _workflowDataValidators.Any(v => v == null)) return true;

            foreach (var validator in _workflowDataValidators)
            {
                if (StepSettings.ExitOnFirstFailedValidation && !isValid) break;
                if (validator.IsValid(workflow)) continue;

                isValid = false;

                if (StepSettings.ThrowOnFailedValidation)
                    throw new FatalException<Exception>();

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
                Logger.LogFatalException(fe);
                throw;
            }
            catch (Exception e)
            {
                Logger.LogException(e);
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
                Logger.LogFatalException(fe);
                throw;
            }
            catch (Exception e)
            {
                Logger.LogException(e);
                onErrorAction?.Invoke();
                if (StepSettings.ThrowOnError) throw;
            }

            return default;
        }
    }

    public abstract class Step<T> : Step where T : IWorkflow
    {
        protected abstract WorkflowState Run(T workflow);
        protected sealed override IWorkflow Run(IWorkflow workflow)
        {
            var result = Run((T)workflow);
            result.Workflow.WorkflowStatus.Next = result.Step;
            return result.Workflow;
        }

        protected Step(
            StepSettings stepSettings,
            ILogger logger,
            IWorkflowManager workflowManager,
            IStepFactory stepFactory,
            WorkflowDataValidator[] workflowDataValidators) :
            base(stepSettings, logger, workflowManager, stepFactory, workflowDataValidators)
        {



        }

    }
}
