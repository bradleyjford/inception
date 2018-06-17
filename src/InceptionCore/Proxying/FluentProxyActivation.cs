using System;
using System.Collections.Generic;
using InceptionCore.Reflection;

namespace InceptionCore.Proxying
{
    public class FluentProxyActivation : FluentProxyDefinition, IFluentInterface
    {
        readonly List<IInterceptor> _interceptors = new List<IInterceptor>();

        IProxyDispatcher _dispatcher;

        internal FluentProxyActivation(Type type)
            : base(type)
        {
        }

        internal ArgumentCollection ConstructorArguments { get; } = new ArgumentCollection();

        internal IProxyDispatcher Dispatcher
        {
            get
            {
                if (_dispatcher == null)
                {
                    _dispatcher = new ProxyDispatcher(_interceptors.ToArray());
                }

                return _dispatcher;
            }
        }

        public void DispatchInvocationsWith(IProxyDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void AddInterceptor(IInterceptor interceptor)
        {
            if (_dispatcher != null)
            {
                throw new InvalidOperationException("Dispatcher already configured.");
            }

            _interceptors.Add(interceptor);
        }

        public void WithConstructorArgument(string parameterName, object value)
        {
            ConstructorArguments.Add(parameterName, value);
        }
    }
}