using System;
using System.Diagnostics;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
	[DebuggerDisplay("{Name}")]
	internal class FieldMetadata : MemberMetadata
	{
		protected const FieldAttributes PrivateReadonlyFieldAttributes =
			FieldAttributes.Private | FieldAttributes.InitOnly;

		protected const FieldAttributes PrivateStaticReadonlyFieldAttributes =
			FieldAttributes.Private | FieldAttributes.Static | FieldAttributes.InitOnly;

		private readonly Type _fieldType;
		private readonly FieldAttributes _fieldAttributes;

		public FieldMetadata(
			string name, 
			Type fieldType, 
			FieldAttributes fieldAttributes) 
			: base(name)
		{
			_fieldType = fieldType;
			_fieldAttributes = fieldAttributes;
		}

		public Type FieldType
		{
			get { return _fieldType; }
		}

		public FieldAttributes FieldAttributes
		{
			get { return _fieldAttributes; }
		}
	}
}
