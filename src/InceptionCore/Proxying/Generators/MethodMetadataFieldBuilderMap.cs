using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using InceptionCore.Proxying.Metadata;

namespace InceptionCore.Proxying.Generators
{
    class MethodMetadataFieldBuilderMap : Dictionary<MethodMetadata, FieldBuilder>
    {
        public MethodMetadataFieldBuilderMap(int capacity)
            : base(capacity)
        {
        }
    }
}