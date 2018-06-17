using System;
using System.Reflection;

namespace InceptionCore.Proxying.Metadata
{
    class MethodInfoFieldMetadata : FieldMetadata
    {
        const string FieldNameFormat = @"_<Method{0}>{1}";

        public MethodInfoFieldMetadata(MethodInfo method)
            : base(GenerateFieldName(method), typeof(MethodInfo), PrivateStaticReadonlyFieldAttributes)
        {
        }

        static string GenerateFieldName(MethodInfo method)
        {
            return string.Format(FieldNameFormat, method.GetHashCode(), method.Name);
        }
    }
}