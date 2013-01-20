using System;
using System.Collections.Generic;

namespace Inception.Proxying
{
	public class FluentProxyDefinition
	{
		private readonly Type _type;

		private Type _targetType;

		private readonly List<InterfaceDefinition> _interfaces =
 			new List<InterfaceDefinition>();

		internal FluentProxyDefinition(Type type)
		{
			_type = type;
		}

		public void WithTarget<T>()
		{
			var targetType = typeof(T);

			if (!_type.IsAssignableFrom(targetType))
			{
				throw new InvalidCastException("Specified target type must derive from the base type.");
			}

			_targetType = targetType;
		}

		public void ApplyInterface<TInterface>()
		{
			_interfaces.Add(new NonTargetedInterfaceDefinition(typeof(TInterface)));
		}

		public void ApplyMixin<TInterface, TImplementation>()
			where TImplementation : TInterface
		{
			_interfaces.Add(new MixinInterfaceDefinition(typeof(TInterface), typeof(TImplementation), true));
		}

		public void ApplyDuckType<TInterface>()
		{
			_interfaces.Add(new DuckTypeInterfaceDefinition(typeof(TInterface)));
		}

		internal ProxyDefinition ToProxyDefinition()
		{
			var interfaces = _interfaces.ToArray();

			if (_type.IsInterface && _targetType != null)
			{
				return new TargetedInterfaceProxyDefinition(_type, _targetType, interfaces);
			}

			if (_targetType != null)
			{
				return new TargetedClassProxyDefinition(_type, interfaces);
			}

			if (_type.IsInterface)
			{
				return new InterfaceProxyDefinition(_type, interfaces);
			}

			return new ClassProxyDefinition(_type, interfaces);
		}
	}
}
