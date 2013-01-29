using System;

namespace Inception.Proxying
{
	public class ProxyFactory
	{
		private readonly ProxyTypeCache _typeCache;
		private readonly ProxyBuilder _builder;

		private readonly IProxyActivator _activator;

		public ProxyFactory(string proxyNamespace, string assemblyFileName)
			: this(proxyNamespace, assemblyFileName, CreateDefaultProxyActivator())
		{
		}

		private static IProxyActivator CreateDefaultProxyActivator()
		{
			var constructorSelector = new ProxyConstructorSelector();

			return new ProxyActivator(constructorSelector);
		}

		public ProxyFactory(string proxyNamespace, string assemblyFileName, IProxyActivator activator)
		{
			_activator = activator;
			_builder = new ProxyBuilder(proxyNamespace, assemblyFileName);

			_typeCache = new ProxyTypeCache(_builder);
		}

		public void SaveAssembly()
		{
			_builder.SaveAssembly();
		}

		public Type DefineProxy<T>(Action<FluentProxyDefinition> config)
			where T : class
		{
			var baseType = typeof(T);

			var specification = new FluentProxyDefinition(baseType);

			config(specification);

			var definition = specification.ToProxyDefinition();

			return DefineProxy(definition);
		}

		public Type DefineProxy(ProxyDefinition definition)
		{
			return _typeCache[definition];
		}

		public T CreateProxy<T>(Action<FluentProxyActivation> config)
		{
			var baseType = typeof(T);

		    return (T)CreateProxy(baseType, config);
		}

		public object CreateProxy(Type baseType, Action<FluentProxyActivation> config)
		{
			var specification = new FluentProxyActivation(baseType);

			config(specification);

			var definition = specification.ToProxyDefinition();

			var type = _typeCache[definition];

			specification.ConstructorArguments.Insert(0, "dispatcher", specification.Dispatcher);

			return _activator.CreateInstance(type, specification.ConstructorArguments);
		}
	}
}
