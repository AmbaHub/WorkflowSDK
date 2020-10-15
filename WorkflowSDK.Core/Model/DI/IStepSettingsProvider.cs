﻿using WorkflowSDK.Core.Model.StepModel;

namespace WorkflowSDK.Core.Model.DI
{
    public interface IStepSettingsProvider
    {
        StepSettings GetStepSettings<T>() where T : Step;
    }
}
