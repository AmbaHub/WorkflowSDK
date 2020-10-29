using WorkflowSDK.Core.Model;

namespace WorkflowSDK.Core
{
    public interface IStepFactory
    {
        T Build<T>() where T : Step;
    }
}