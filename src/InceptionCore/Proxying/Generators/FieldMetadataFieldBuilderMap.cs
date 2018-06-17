using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using InceptionCore.Proxying.Metadata;

namespace InceptionCore.Proxying.Generators
{
    class FieldMetadataFieldBuilderMap : Dictionary<FieldMetadata, FieldBuilder>
    {
        public FieldMetadataFieldBuilderMap(int capacity)
            : base(capacity)
        {
        }
    }
}