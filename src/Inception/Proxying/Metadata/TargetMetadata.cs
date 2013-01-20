using System;

namespace Inception.Proxying.Metadata
{
	internal sealed class TargetMetadata : MemberMetadata
	{
		private readonly string _parameterName;
		private readonly Type _type;
		private readonly Type _targetType;
		private readonly bool _isProxyInstantiated;

		private TargetFieldMetadata _instanceField;

		public TargetMetadata(
			string parameterName, 
			Type type, 
			Type targetType, 
			bool isProxyInstantiated) 
			: base(parameterName)
		{
			_parameterName = parameterName;
			_type = type;
			_targetType = targetType;
			_isProxyInstantiated = isProxyInstantiated;

			_instanceField = new TargetFieldMetadata("_" + parameterName, type);
		}

		public string ParameterName
		{
			get { return _parameterName; }
		}

		public Type Type
		{
			get { return _type; }
		}

		public Type TargetType
		{
			get { return _targetType; }
		}

		public bool IsProxyInstantiated
		{
			get { return _isProxyInstantiated; }
		}

		public FieldMetadata InstanceField
		{
			get { return _instanceField; }
		}
	}
}