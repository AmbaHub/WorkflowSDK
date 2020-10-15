using System;

namespace WorkflowSDK.Core.Model
{
    public abstract class FatalException : Exception
    {
        internal Exception InnerMainException { get; set; }

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
