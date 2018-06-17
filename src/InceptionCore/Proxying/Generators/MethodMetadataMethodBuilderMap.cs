using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using InceptionCore.Proxying.Metadata;

namespace InceptionCore.Proxying.Generators
{
    class MethodMetadataMethodBuilderMap : Dictionary<MethodMetadata, MethodBuilder>
    {
        public MethodMetadataMethodBuilderMap(int capacity)
            : base(capacity)
        {
        }
    }
}