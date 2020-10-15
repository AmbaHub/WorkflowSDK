using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowSDK.Log
{
    public interface ILogger
    {
        void Log(string log);
        void Log(LogLevel logLevel, string log);
        void Log<T>(T log);
        void Log<T>(LogLevel logLevel,T log);
        void Log<T>();
        void Log<T>(LogLevel logLevel);
        void LogException(Exception ex);
        void LogFatalException(Exception ex);
    }
}
