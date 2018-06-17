using System;
using System.Reflection;

namespace InceptionCore.Proxying
{
    public interface IProxyDispatcher
    {
        object MethodInvocation(
            object target,
            MethodInfo method,
            object[] arguments,
            ProxyTargetInvocation proxyTargetInvocation);
    }
}