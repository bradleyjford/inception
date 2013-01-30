using System;
using System.Reflection;
using System.Reflection.Emit;
using Inception.Proxying.Generators;

namespace Inception.Proxying.Metadata
{
    internal class TargetedMethodMetadata : MethodMetadata
    {
        private readonly FieldMetadata _targetField;
        private readonly MethodInfo _targetMethod;
        private readonly MethodAttributes _methodAttributes;

        public TargetedMethodMetadata(MethodInfo method, FieldMetadata targetField, MethodInfo targetMethod) 
            : base(method)
        {
            _targetField = targetField;
            _targetMethod = targetMethod;

            _methodAttributes = method.Attributes;

            _methodAttributes &= ~MethodAttributes.NewSlot;
            _methodAttributes &= ~MethodAttributes.Abstract;
            _methodAttributes |= MethodAttributes.ReuseSlot | MethodAttributes.Final;
        }

        public MethodInfo TargetMethod
        {
            get { return _targetMethod; }
        }

        public override MethodAttributes MethodAttributes
        {
            get { return _methodAttributes; }
        }

        public override MethodGenerator CreateGenerator(
            FieldMetadataFieldBuilderMap instanceFieldBuilders,
            MethodMetadataFieldBuilderMap methodMetadataFieldBuilders,
            FieldBuilder dispatcherField)
        {
            var methodInfoField = methodMetadataFieldBuilders[this];
            var targetField = instanceFieldBuilders[_targetField];

            return new TargetedMethodGenerator(this, dispatcherField, methodInfoField, targetField);
        }
    }
}
