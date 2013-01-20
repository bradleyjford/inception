using System;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
	internal sealed class ConstructorMetadata : MemberMetadata
	{
		private static readonly MethodAttributes ConstructorMethodAttributes =
			MethodAttributes.Public | MethodAttributes.SpecialName;

		private readonly ConstructorInfo _constructorInfo;
		private readonly ConstructorParameterMetadata[] _parameters;

		private readonly Type[] _parameterTypes;

		public ConstructorMetadata(ConstructorParameterMetadata[] parameters) 
			: base(".ctor")
		{
			_parameters = parameters;

			_parameterTypes = GetParameterTypes(_parameters);
		}

		public ConstructorMetadata(
			ConstructorInfo constructorInfo,
			ConstructorParameterMetadata[] parameters)
			: base(constructorInfo)
		{
			_constructorInfo = constructorInfo;
			_parameters = parameters;

			_parameterTypes = GetParameterTypes(_parameters);
		}

		private static Type[] GetParameterTypes(ConstructorParameterMetadata[] parameters)
		{
			var parameterTypes = new Type[parameters.Length];

			for (var i = 0; i < parameters.Length; i++)
			{
				parameterTypes[i] = parameters[i].ParameterType;
			}

			return parameterTypes;
		}

		public MethodAttributes MethodAttributes
		{
			get { return ConstructorMethodAttributes; }
		}

		public ConstructorInfo ConstructorInfo
		{
			get { return _constructorInfo; }
		}

		public bool CallBaseConstructor
		{
			get { return _constructorInfo != null; }
		}

		public ConstructorParameterMetadata[] Parameters
		{
			get { return _parameters; }
		}

		public Type[] ParameterTypes
		{
			get { return _parameterTypes; }
		}
	}
}
