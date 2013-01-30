using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using Inception.Proxying.Generators;

namespace Inception.Proxying.Metadata
{
    [DebuggerDisplay("{Method.Name}")]
    internal abstract class MethodMetadata : MemberMetadata
    {
        private readonly MethodInfo _method;
        private readonly Type[] _parameterTypes;

        private readonly MethodInfoFieldMetadata _metadataField;

        public MethodMetadata(MethodInfo method)
            : base(method)
        {
            _method = method;

            _parameterTypes = InitializeParameterTypes(method);

            _metadataField = new MethodInfoFieldMetadata(method);
        }

        private static Type[] InitializeParameterTypes(MethodInfo method)
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

        public MethodInfo Method
        {
            get { return _method; }
        }

        public abstract MethodAttributes MethodAttributes { get; }

        public FieldMetadata MetadataField
        {
            get { return _metadataField; }
        }

        public Type[] ParameterTypes
        {
            get { return _parameterTypes; }
        }

        public Type ReturnType
        {
            get { return _method.ReturnType; }
        }

        public abstract MethodGenerator CreateGenerator(
            FieldMetadataFieldBuilderMap instanceFieldBuilders, 
            MethodMetadataFieldBuilderMap methodMetadataFieldBuilders,
            FieldBuilder dispatcherField);
    }
}
