using System;
using System.Collections.Generic;
using WorkflowSDK.Core.Model.Workflow;

namespace WorkflowSDK.Core
{
    public interface IWorkflowManager : IDisposable
    {
        IEnumerable<IWorkflow> AllWorkflows { get; }
        IWorkflow<T> CreateWorkflow<T>(T workflowData) where T : new();
        IWorkflow<T> CreateWorkflow<T>(T workflowData, string key) where T : new();
        IWorkflow<T> GetWorkflow<T>() where T : new();
        IWorkflow<T> GetWorkflow<T>(string key) where T : new();
        void RemoveWorkflow<TF, TD>() where TF : IWorkflow<TD> where TD : new();
        void RemoveWorkflow<TF, TD>(string key) where TF : IWorkflow<TD> where TD : new();
    }
}