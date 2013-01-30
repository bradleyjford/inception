using System;

namespace Inception.Proxying.Metadata
{
    internal sealed class ConstructorBaseParameterMetadata : ConstructorParameterMetadata
    {
        public ConstructorBaseParameterMetadata(int sequence, string name, Type parameterType) 
            : base(sequence, name, parameterType)
        {
        }
    }
}
