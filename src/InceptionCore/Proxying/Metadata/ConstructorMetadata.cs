using System;
using System.Reflection;

namespace InceptionCore.Proxying.Metadata
{
    sealed class ConstructorMetadata : MemberMetadata
    {
        static readonly MethodAttributes ConstructorMethodAttributes =
            MethodAttributes.Public | MethodAttributes.SpecialName;

        public ConstructorMetadata(ConstructorParameterMetadata[] parameters)
            : base(".ctor")
        {
            Parameters = parameters;

            ParameterTypes = GetParameterTypes(Parameters);
        }

        public ConstructorMetadata(
            ConstructorInfo constructorInfo,
            ConstructorParameterMetadata[] parameters)
            : base(constructorInfo)
        {
            ConstructorInfo = constructorInfo;
            Parameters = parameters;

            ParameterTypes = GetParameterTypes(Parameters);
        }

        public MethodAttributes MethodAttributes => ConstructorMethodAttributes;

        public ConstructorInfo ConstructorInfo { get; }

        public bool CallBaseConstructor => ConstructorInfo != null;

        public ConstructorParameterMetadata[] Parameters { get; }

        public Type[] ParameterTypes { get; }

        static Type[] GetParameterTypes(ConstructorParameterMetadata[] parameters)
        {
            var parameterTypes = new Type[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                parameterTypes[i] = parameters[i].ParameterType;
            }

            return parameterTypes;
        }
    }
}