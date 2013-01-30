using System;
using System.Reflection;

namespace Inception.Proxying
{
    public interface IInvocation
    {
        object Target { get; }
        object Proxy { get; }
        MethodInfo Method { get; }
        object[] Arguments { get; }
        object ReturnValue { get; set; }

        void Proceed();
    }

    public interface IInvocation<T>
    {
        T Target { get; }
        T Proxy { get; }
        MethodInfo Method { get; }
        object[] Arguments { get; }
        object ReturnValue { get; set; }

        void Proceed();
    }
}