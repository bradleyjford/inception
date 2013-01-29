using System;

namespace Inception.Proxying
{
	public class DuckTypeInterfaceDefinition : InterfaceDefinition, IEquatable<DuckTypeInterfaceDefinition>
	{
		public DuckTypeInterfaceDefinition(Type type) 
			: base(type)
		{
		}

		public bool Equals(DuckTypeInterfaceDefinition other)
		{
			if (other == null) return false;

			return Type == other.Type;
		}

		public override bool Equals(object other)
		{
			var interfaceDefinition = other as DuckTypeInterfaceDefinition;

			return Equals(interfaceDefinition);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hash = HashCodeUtility.Seed;

				hash = HashCodeUtility.Hash(hash, typeof(DuckTypeInterfaceDefinition));
				hash = HashCodeUtility.Hash(hash, Type);

				return hash;
			}
		}
	}
}
