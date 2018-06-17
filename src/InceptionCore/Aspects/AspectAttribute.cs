using System;
using System.Reflection;

namespace InceptionCore.Aspects
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class AspectAttribute : Attribute
    {
        public int Order { get; set; } = 1;

        protected internal virtual void OnBeforeInvocation(
            object target, 
            MethodInfo method,
            ParameterInfo[] parameters, 
            object[] arguments)
        {
        }

        protected internal virtual void OnAfterInvocation(
            object target, 
            MethodInfo method,
            ParameterInfo[] parameters, 
            object[] arguments, 
            object returnValue)
        {
        }

        protected internal virtual bool OnInvocationException(
            object target, 
            MethodInfo method,
            ParameterInfo[] parameters, 
            object[] arguments, 
            Exception exception)
        {
            return false;
        }
    }
}
