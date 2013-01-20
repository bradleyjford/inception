using System;
using Inception.Reflection;

namespace Inception.InversionOfControl
{
	public class Registration : IRegistration
	{
		private readonly string _name;
		private readonly Type _baseType;
		private Type _concreteType;
		private Type _lifecycleType;
		private IContainerActivator _activator;
		private readonly ArgumentCollection _constructorArguments = 
			new ArgumentCollection();

		public Registration(Type baseType, string name)
		{
			_name = name;
			_baseType = baseType;
		}

		public string Name
		{
			get { return _name; }
		}

		public Type BaseType
		{
			get { return _baseType; }
		}

		public Type ConcreteType
		{
			get { return _concreteType; }
			set { _concreteType = value; }
		}

		public Type LifecycleType
		{
			get { return _lifecycleType; }
			set { _lifecycleType = value; }
		}

		public IContainerActivator Activator
		{
			get { return _activator; }
			set { _activator = value; }
		}

		public ArgumentCollection ConstructorArguments
		{
			get { return _constructorArguments; }
		}
	}
}
