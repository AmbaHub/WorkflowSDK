using System;
using WorkflowSDK.Log;

namespace WorkflowSDK.Core.Tests.TestObjects
{
    public class TestLogger : ILogger
    {
        public void Log(string log)
        {
        }

        public void Log(LogLevel logLevel, string log)
        {
        }

        public void Log<T>(T log)
        {
        }

        public void Log<T>(LogLevel logLevel, T log)
        {
            
        }

        public void Log<T>()
        {
        }

        public void Log<T>(LogLevel logLevel)
        {
        }

        public void LogException(Exception ex)
        {
        }

        public void LogFatalException(Exception ex)
        {
        }

        public void Log(Exception ex)
        {
        }
    }
}
