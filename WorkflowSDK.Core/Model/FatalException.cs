using System;

namespace WorkflowSDK.Core.Model
{
    public class FatalException : Exception
    {
        internal Exception InnerMainException { get; set; }

        internal protected FatalException()
        {
                
        }

        public static FatalException GetFatalException(string message)
        {
            return new FatalException
            {
                InnerMainException = new Exception(message)
            };
        }
        public static FatalException<T> GetFatalException<T>(string message) where T : Exception, new()
        {
            return new FatalException<T>
            {
                InnerMainException = typeof(T).CreateInstance<T>(new object[]{message})
            };
        }
        public static void ArgumentException(string message)
        {
            throw new FatalException<ArgumentException>
            {
                MainException = new ArgumentException(message)
            };
        }
        public static void ArgumentNullException(object obj, string message)
        {
            if (obj == null)
            {
                throw new FatalException<ArgumentNullException>
                {
                    MainException = new ArgumentNullException(message)
                };
            }
        }

    }
    public class FatalException<T> : FatalException where T : Exception
    {
        public T MainException
        {
            get => (T)InnerMainException;
            set => InnerMainException = value;
        }
    }

}
