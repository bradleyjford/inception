using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Inception.Proxying.Metadata;

namespace Inception.Proxying.Generators
{
    internal class FieldMetadataFieldBuilderMap : Dictionary<FieldMetadata, FieldBuilder>
    {
        public FieldMetadataFieldBuilderMap(int capacity)
            : base(capacity)
        {
            
        }
    }
}
