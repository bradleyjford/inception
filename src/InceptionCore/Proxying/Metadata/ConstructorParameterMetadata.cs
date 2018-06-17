using System;
using System.Diagnostics;

namespace InceptionCore.Proxying.Metadata
{
    [DebuggerDisplay("{Name} ({ParameterType})")]
    abstract class ConstructorParameterMetadata
    {
        public ConstructorParameterMetadata(int sequence, string name, Type parameterType)
        {
            Sequence = sequence;
            Name = name;
            ParameterType = parameterType;
        }

        public int Sequence { get; }

        public string Name { get; }

        public Type ParameterType { get; }
    }
}