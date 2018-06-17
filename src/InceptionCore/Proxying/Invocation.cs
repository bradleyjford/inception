using System;
using System.Reflection;

namespace InceptionCore.Proxying
{
    public class Invocation : IInvocation
    {
        readonly IInterceptor[] _interceptors;
        readonly IProxy _proxy;
        readonly ProxyTargetInvocation _proxyTargetInvocation;

        int _currentInterceptorIndex;

        public Invocation(
            object target,
            IProxy proxy,
            MethodInfo method,
            object[] arguments,
            ProxyTargetInvocation proxyTargetInvocation,
            IInterceptor[] interceptors)
        {
            Target = target;
            _proxy = proxy;
            Method = method;
            Arguments = arguments;
            _proxyTargetInvocation = proxyTargetInvocation;
            _interceptors = interceptors;
        }

        public void Proceed()
        {
            if (_currentInterceptorIndex == _interceptors.Length - 1)
            {
                if (_proxyTargetInvocation != null)
                {
                    ReturnValue = _proxyTargetInvocation(Arguments);
                }
            }
            else
            {
                _interceptors[_currentInterceptorIndex++].Intercept(this);
            }
        }

        public object Target { get; }

        public object Proxy => _proxy;

        public MethodInfo Method { get; }

        public object[] Arguments { get; }

        public object ReturnValue { get; set; }

        public void ExecuteInterceptors()
        {
            _currentInterceptorIndex = 0;

            if (_interceptors.Length > 0)
            {
                _interceptors[0].Intercept(this);
            }
        }
    }
}