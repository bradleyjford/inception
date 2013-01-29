using System;
using System.Linq;

namespace Inception.Proxying
{
	public abstract class ProxyDefinition : IProxyDefinitionElement, IEquatable<ProxyDefinition>
	{
		private const string ProxyTypeNameFormat = @"<Proxy{0}>{1}";

		private readonly Type _primaryType;
		private readonly InterfaceDefinition[] _interfaces;

	    protected ProxyDefinition(Type primaryType, InterfaceDefinition[] interfaces)
		{
			_primaryType = primaryType;
			_interfaces = interfaces ?? new InterfaceDefinition[0];
		}

		public Type Type
		{
			get { return _primaryType; }
		}

		public InterfaceDefinition[] Interfaces
		{
			get { return _interfaces; }
		}

		public string ProxyTypeName
		{
			get { return String.Format(ProxyTypeNameFormat, GetHashCode(), _primaryType.Name); }
		}

		public bool Equals(ProxyDefinition other)
		{
			if (other == null) return false;
			if (GetType() != other.GetType()) return false;

			var interfacesEqual = Interfaces.SequenceEqual(other.Interfaces);

			return Type == other.Type && interfacesEqual;
		}

		public override bool Equals(object other)
		{
			var definition = other as ProxyDefinition;

			return Equals(definition);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hash = HashCodeUtility.Seed;

				hash = HashCodeUtility.Hash(hash, GetType());
				hash = HashCodeUtility.Hash(hash, _primaryType);
				hash = HashCodeUtility.Hash(hash, _interfaces);

				return hash;
			}
		}
	}
}
