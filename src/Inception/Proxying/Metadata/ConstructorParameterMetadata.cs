using System;
using System.Diagnostics;

namespace Inception.Proxying.Metadata
{
    [DebuggerDisplay("{Name} ({ParameterType})")]
    internal abstract class ConstructorParameterMetadata
    {
        private readonly int _sequence;
        private readonly string _name;
        private readonly Type _parameterType;

        public ConstructorParameterMetadata(int sequence, string name, Type parameterType)
        {
            _sequence = sequence;
            _name = name;
            _parameterType = parameterType;
        }

        public int Sequence
        {
            get { return _sequence; }
        }

        public string Name
        {
            get { return _name; }
        }

        public Type ParameterType
        {
            get { return _parameterType; }
        }
    }
}
