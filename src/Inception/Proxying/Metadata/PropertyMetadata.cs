using System;
using System.Diagnostics;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
	[DebuggerDisplay("{Name} ({PropertyInfo.PropertyType})")]
	internal class PropertyMetadata : MemberMetadata
	{
		private readonly PropertyInfo _propertyInfo;
		private readonly MethodMetadata _getMethod;
		private readonly MethodMetadata _setMethod;

		private readonly Type[] _indexerTypes;

		private readonly PropertyAttributes _propertyAttributes;

		public PropertyMetadata(
			PropertyInfo propertyInfo, 
			MethodMetadata getMethod, 
			MethodMetadata setMethod) 
			: base(propertyInfo)
		{
			_propertyInfo = propertyInfo;

			_getMethod = getMethod;
			_setMethod = setMethod;

			_indexerTypes = GetIndexerTypes(_propertyInfo);

			_propertyAttributes = propertyInfo.Attributes;
		}

		private static Type[] GetIndexerTypes(PropertyInfo property)
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

		private void UseExplicitImplementationForHandlerMethod(MethodMetadata handlerMethod)
		{
			if (handlerMethod != null)
			{
				handlerMethod.UseExplicitInterfaceImplementation();
			}
		}

		public PropertyAttributes PropertyAttributes
		{
			get { return _propertyAttributes; }
		}

		public Type PropertyType
		{
			get { return _propertyInfo.PropertyType; }
		}

		public Type[] IndexerTypes
		{
			get { return _indexerTypes; }
		}

		public PropertyInfo PropertyInfo
		{
			get { return _propertyInfo; }
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
