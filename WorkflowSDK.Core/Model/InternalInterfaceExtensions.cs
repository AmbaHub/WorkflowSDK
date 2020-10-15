using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WorkflowSDK.Core.Model.Workflow;

namespace WorkflowSDK.Core.Model
{
    internal static class InternalInterfaceExtensions
    {
        internal static async Task<IWorkflow> RunWorkflowAsync(this IWorkflow workflow, Step step)
        {
            FatalException.ArgumentNullException(workflow, nameof(workflow));
            FatalException.ArgumentNullException(step, nameof(step));

            do
            {
                if (step == null || workflow == null) break;
                var previousBuffer = step;
                workflow.WorkflowStatus.Current.Step = step;
                workflow.WorkflowStatus.Current.Workflow = workflow;

                workflow = await step.RunAsync(workflow);

                if (workflow == null) break;
                workflow.WorkflowStatus.Previous.Step = previousBuffer;
                workflow.WorkflowStatus.Previous.Workflow = workflow.Clone();
                step = workflow.WorkflowStatus.Next;
            }
            while (!(step?.StepSettings?.ExitFromFlow ?? false));

            return workflow;
        }


        internal static IReadOnlyDictionary<string, object> GetContent<T>(this T workflowData)
        {
            FatalException.ArgumentNullException(workflowData, nameof(workflowData));
   
            return GetProperties(workflowData)
                .ToDictionary(p => p.Name, p => p.GetValue(workflowData));
        }

        internal static void SetContent<T>(this T workflowData, IReadOnlyDictionary<string, object> content, bool overwrite)
        {
            FatalException.ArgumentNullException(workflowData, nameof(workflowData));
            FatalException.ArgumentNullException(content, nameof(content));

            var properties = GetProperties(workflowData);

            foreach (var p in properties)
                foreach (var (key, value) in content)
                    if (
                        p.Name == key &&
                        p.PropertyType == value.GetType() &&
                        p.GetValue(workflowData) != null && !overwrite)
                    {
                        p.SetValue(workflowData, value);
                    }
        }

        internal static string GenerateKey(this Type type, string key)
        {
            return !string.IsNullOrEmpty(key)
                ? $"{type.FullName}.{key}"
                : $"{type.FullName}";
        }
        internal static T CreateInstance<T>()
        {
            try
            {
                return Activator.CreateInstance<T>();
            }
            catch (Exception e)
            {
                throw new FatalException { InnerMainException = e };
            }
        }
        internal static T CreateInstance<T>(this Type type, object[] args)
        {
            try
            {
                return (T) Activator.CreateInstance(type, args);
            }
            catch (Exception e)
            {
                throw new FatalException {InnerMainException = e};
            }
        }
        
        private static PropertyInfo[] GetProperties(object workflowData)
        {
            return workflowData
                .GetType()
                .GetProperties(BindingFlags.Public & BindingFlags.GetProperty & BindingFlags.SetProperty)
                .Where(p => p.GetCustomAttribute<IgnoreAttribute>() == null)
                .ToArray();
        }


    }
}