using System;

namespace Inception.Proxying.Metadata
{
	internal class TargetFieldMetadata : FieldMetadata
	{
		public TargetFieldMetadata(string name, Type fieldType)
			: base(name, fieldType, PrivateReadonlyFieldAttributes)
		{
		}
	}
}
