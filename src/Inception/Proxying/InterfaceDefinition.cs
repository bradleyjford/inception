using System;

namespace Inception.Proxying
{
	public abstract class InterfaceDefinition : IProxyDefinitionElement
	{
		private readonly Type _type;

		protected InterfaceDefinition(Type type)
		{
			_type = type;
		}

		public Type Type 
		{ 
			get { return _type; } 
		}

		public abstract override bool Equals(object other);

		public abstract override int GetHashCode();
	}
}
