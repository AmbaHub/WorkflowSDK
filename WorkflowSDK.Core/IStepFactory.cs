namespace WorkflowSDK.Core.Model.DI
{
    public interface IStepFactory
    {
        T Build<T>() where T : Step;
    }
}