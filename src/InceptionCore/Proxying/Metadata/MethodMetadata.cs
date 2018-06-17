using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using InceptionCore.Proxying.Generators;

namespace InceptionCore.Proxying.Metadata
{
    [DebuggerDisplay("{Method.Name}")]
    abstract class MethodMetadata : MemberMetadata
    {
        readonly MethodInfoFieldMetadata _metadataField;

        public MethodMetadata(MethodInfo method)
            : base(method)
        {
            Method = method;

            ParameterTypes = InitializeParameterTypes(method);

            _metadataField = new MethodInfoFieldMetadata(method);
        }

        public MethodInfo Method { get; }

        public abstract MethodAttributes MethodAttributes { get; }

        public FieldMetadata MetadataField => _metadataField;

        public Type[] ParameterTypes { get; }

        public Type ReturnType => Method.ReturnType;

        static Type[] InitializeParameterTypes(MethodInfo method)
        {
            var parameters = method.GetParameters();
            var parameterCount = parameters.Length;

            var result = new Type[parameterCount];

            for (var i = 0; i < parameterCount; i++)
            {
                result[i] = parameters[i].ParameterType;
            }

            return result;
        }

        public abstract MethodGenerator CreateGenerator(
            FieldMetadataFieldBuilderMap instanceFieldBuilders,
            MethodMetadataFieldBuilderMap methodMetadataFieldBuilders,
            FieldBuilder dispatcherField);
    }
}