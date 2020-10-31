using System.Threading.Tasks;
using WorkflowSDK.Core.Model;
using WorkflowSDK.Core.Model.Workflow;

namespace WorkflowSDK.Business
{
    public abstract class BusinessWorkflow
    {
        internal readonly IWorkflow _workflow;

        protected BusinessWorkflow(IWorkflow workflow)
        {
            _workflow = workflow;
        }
        public void RunInBackground(Step step) => _workflow.RunInBackground(step);
        public async Task<TD> AwaitProcess<TD, TS>()
            where TD : new()
            where TS : Step<IWorkflow<TD>>
        {
            var result = await _workflow.AwaitProcessAsync<IWorkflow<TD>, TS>();
            return result.WorkflowData;
        }
    }
    public sealed class BusinessWorkflow<T> : BusinessWorkflow where T : new()
    {
        public BusinessWorkflow(IWorkflow<T> workflow) : base(workflow)
        {

        }
    }
}