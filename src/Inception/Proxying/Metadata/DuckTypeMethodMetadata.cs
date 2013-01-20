using System;
using System.Reflection;
using System.Reflection.Emit;
using Inception.Proxying.Generators;

namespace Inception.Proxying.Metadata
{
	internal class DuckTypeMethodMetadata : MethodMetadata
	{
		private readonly MethodInfo _targetMethod;
		private readonly MethodAttributes _methodAttributes;

		public DuckTypeMethodMetadata(MethodInfo method, MethodInfo targetMethod) 
			: base(method)
		{
			_targetMethod = targetMethod;
			_methodAttributes = method.Attributes;

			_methodAttributes &= ~MethodAttributes.Abstract;
			_methodAttributes &= ~MethodAttributes.NewSlot;

			_methodAttributes |= MethodAttributes.ReuseSlot | MethodAttributes.Final;
		}

		public override MethodAttributes MethodAttributes
		{
			get { return _methodAttributes; }
		}

		public MethodInfo TargetMethod
		{
			get { return _targetMethod; }
		}

		public override MethodGenerator CreateGenerator(
			FieldMetadataFieldBuilderMap instanceFieldBuilders, 
			MethodMetadataFieldBuilderMap methodMetadataFieldBuilders, 
			FieldBuilder dispatcherField)
		{
			return new DuckTypeMethodGenerator(this);
		}
	}
}
