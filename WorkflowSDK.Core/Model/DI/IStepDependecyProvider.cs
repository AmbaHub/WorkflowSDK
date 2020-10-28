namespace WorkflowSDK.Core.Model.DI
{
    public interface IStepDependencyProvider
    {

        object[] GetStepDependencies<T>() where T : Step;
    }
}
