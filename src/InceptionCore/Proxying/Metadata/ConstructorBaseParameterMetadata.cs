using System;

namespace InceptionCore.Proxying.Metadata
{
    sealed class ConstructorBaseParameterMetadata : ConstructorParameterMetadata
    {
        public ConstructorBaseParameterMetadata(int sequence, string name, Type parameterType)
            : base(sequence, name, parameterType)
        {
        }
    }
}