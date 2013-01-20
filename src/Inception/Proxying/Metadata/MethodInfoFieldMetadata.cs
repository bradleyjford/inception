using System;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
	internal class MethodInfoFieldMetadata : FieldMetadata
	{
		private const string FieldNameFormat = @"_<Method{0}>{1}";

		public MethodInfoFieldMetadata(MethodInfo method) 
			: base(GenerateFieldName(method), typeof(MethodInfo), PrivateStaticReadonlyFieldAttributes)
		{
		}

		private static string GenerateFieldName(MethodInfo method)
		{
			return String.Format(FieldNameFormat, method.GetHashCode(), method.Name);
		}
	}
}
