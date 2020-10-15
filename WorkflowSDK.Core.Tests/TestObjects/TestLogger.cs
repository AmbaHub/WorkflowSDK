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

        public void Log<T>()
        {
        }

        public void Log(Exception ex)
        {
        }
    }
}
