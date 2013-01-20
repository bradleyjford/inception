using System;
using Inception.Proxying;

namespace Inception.InversionOfControl.Configuration
{
	public interface IFluentForRegistration<in TBase> : IFluentInterface
	{
		IFluentForRegistration<TBase> Singleton();

		IFluentForRegistration<TBase> Proxy(ProxyFactory proxyFactory, IInterceptor interceptor);
		IFluentForRegistration<TBase> Proxy(ProxyFactory proxyFactory, IProxyDispatcher proxtDispatcher);

		IFluentForRegistration<TBase> WithCtorArgument(string parameterName, object value);

		IFluentForRegistration<TBase> Lifecycle<TLifecycle>() where TLifecycle : IContainerLifecycle;

		IFluentForRegistration<TBase> Named(string name);

		void Use<TConcrete>(TConcrete singletonInstance) where TConcrete : TBase;
		void Use<TConcrete>() where TConcrete : TBase;
		void Use<TConcrete>(Func<IContainer, TConcrete> activationDelegate) where TConcrete : TBase;		
	}
}
