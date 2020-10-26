using System;

namespace WorkflowSDK.Core.Model
{
    public class StepSettings
    {
        public bool ThrowOnError { get; set; }
        public bool ExitFromFlow { get; set; }
        public Action OnFailedStep { get; set; }
        public bool ExitOnFirstFailedValidation { get; set; }
        public bool ThrowOnFailedValidation { get; set; }
        public bool RunPreviousOnError { get; set; }

    }
}