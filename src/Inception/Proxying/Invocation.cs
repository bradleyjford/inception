using System;
using System.Reflection;

namespace Inception.Proxying
{
    public class Invocation : IInvocation
    {
        private readonly object _target;
        private readonly IProxy _proxy;
        private readonly MethodInfo _method;
        private readonly object[] _arguments;
        private readonly ProxyTargetInvocation _proxyTargetInvocation;
        private readonly IInterceptor[] _interceptors;

        private int _currentInterceptorIndex;
        private object _returnValue;

        public Invocation(
            object target,
            IProxy proxy,
            MethodInfo method, 
            object[] arguments,
            ProxyTargetInvocation proxyTargetInvocation,
            IInterceptor[] interceptors)
        {
            _target = target;
            _proxy = proxy;
            _method = method;
            _arguments = arguments;
            _proxyTargetInvocation = proxyTargetInvocation;
            _interceptors = interceptors;
        }

        public void ExecuteInterceptors()
        {
            _currentInterceptorIndex = 0;

            if (_interceptors.Length > 0)
            {
                _interceptors[0].Intercept(this);
            }
        }

        public void Proceed()
        {
            if (_currentInterceptorIndex == _interceptors.Length - 1)
            {
                if (_proxyTargetInvocation != null)
                {
                    _returnValue = _proxyTargetInvocation(_arguments);
                }
            }
            else
            {
                _interceptors[_currentInterceptorIndex++].Intercept(this);
            }
        }

        public object Target
        {
            get { return _target; }
        }

        public object Proxy
        {
            get { return _proxy; }
        }

        public MethodInfo Method
        {
            get { return _method; }
        }

        public object[] Arguments
        {
            get { return _arguments; }
        }

        public object ReturnValue
        {
            get { return _returnValue; }
            set { _returnValue = value; }
        }
    }
}