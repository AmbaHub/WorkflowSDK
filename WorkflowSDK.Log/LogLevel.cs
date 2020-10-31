using System;

namespace WorkflowSDK.Log
{
    [Flags]
    public enum LogLevel 
    {
        Default,
        Debug,
        Trace,
        Info,
        System,
        Warning,
        Error
    }
}