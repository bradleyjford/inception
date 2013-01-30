using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Inception.Proxying.Metadata;

namespace Inception.Proxying.Generators
{
    internal class MethodMetadataMethodBuilderMap : Dictionary<MethodMetadata, MethodBuilder>
    {
        public MethodMetadataMethodBuilderMap(int capacity)
            : base(capacity)
        {
            
        }
    }
}
