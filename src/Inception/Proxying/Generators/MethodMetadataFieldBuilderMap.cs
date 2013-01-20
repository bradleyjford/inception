using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Inception.Proxying.Metadata;

namespace Inception.Proxying.Generators
{
	internal class MethodMetadataFieldBuilderMap : Dictionary<MethodMetadata, FieldBuilder>
	{
		public MethodMetadataFieldBuilderMap(int capacity)
			: base(capacity)
		{
			
		}
	}
}
