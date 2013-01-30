using System;
using System.Reflection;
using System.Reflection.Emit;
using Inception.Proxying.Generators;

namespace Inception.Proxying.Metadata
{
    internal class NonTargetedMethodMetadata : MethodMetadata
    {
        private readonly MethodAttributes _methodAttributes;

        public NonTargetedMethodMetadata(MethodInfo method) 
            : base(method)
        {
            _methodAttributes = method.Attributes;
            _methodAttributes &= ~MethodAttributes.Abstract;
            _methodAttributes &= ~MethodAttributes.NewSlot;
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

            return new NonTargetedMethodGenerator(this, dispatcherField, methodInfoField);
        }
    }
}
