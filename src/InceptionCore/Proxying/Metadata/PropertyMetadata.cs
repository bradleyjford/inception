using System;
using System.Diagnostics;
using System.Reflection;

namespace InceptionCore.Proxying.Metadata
{
    [DebuggerDisplay("{Name} ({PropertyInfo.PropertyType})")]
    class PropertyMetadata : MemberMetadata
    {
        readonly MethodMetadata _getMethod;

        readonly MethodMetadata _setMethod;

        public PropertyMetadata(
            PropertyInfo propertyInfo,
            MethodMetadata getMethod,
            MethodMetadata setMethod)
            : base(propertyInfo)
        {
            PropertyInfo = propertyInfo;

            _getMethod = getMethod;
            _setMethod = setMethod;

            IndexerTypes = GetIndexerTypes(PropertyInfo);

            PropertyAttributes = propertyInfo.Attributes;
        }

        public PropertyAttributes PropertyAttributes { get; }

        public Type PropertyType => PropertyInfo.PropertyType;

        public Type[] IndexerTypes { get; }

        public PropertyInfo PropertyInfo { get; }

        static Type[] GetIndexerTypes(PropertyInfo property)
        {
            var indexerParameters = property.GetIndexParameters();

            var result = new Type[indexerParameters.Length];

            for (var i = 0; i < indexerParameters.Length; i++)
            {
                result[i] = indexerParameters[i].ParameterType;
            }

            return result;
        }

        public override void UseExplicitInterfaceImplementation()
        {
            base.UseExplicitInterfaceImplementation();

            UseExplicitImplementationForHandlerMethod(_getMethod);
            UseExplicitImplementationForHandlerMethod(_setMethod);
        }

        void UseExplicitImplementationForHandlerMethod(MethodMetadata handlerMethod)
        {
            if (handlerMethod != null)
            {
                handlerMethod.UseExplicitInterfaceImplementation();
            }
        }

        public MethodMetadata GetGetMethod()
        {
            return _getMethod;
        }

        public MethodMetadata GetSetMethod()
        {
            return _setMethod;
        }
    }
}