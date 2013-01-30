using System;
using System.Reflection.Emit;
using Inception.Proxying.Metadata;

namespace Inception.Proxying.Generators
{
    internal class TypeGenerator
    {
        private readonly ModuleBuilder _moduleBuilder;
        private readonly string _generationNamespace;
        private readonly TypeMetadata _metadata;

        private MethodMetadataMethodBuilderMap _methodBuilders;
        private MethodMetadataFieldBuilderMap _methodMetadataFieldBuilders;
        private FieldMetadataFieldBuilderMap _instanceFieldBuilders;
        private TypeBuilder _typeBuilder;

        public TypeGenerator(
            ModuleBuilder moduleBuilder,
            string generationNamespace, 
            TypeMetadata metadata)
        {
            _moduleBuilder = moduleBuilder;
            _generationNamespace = generationNamespace;
            _metadata = metadata;
        }

        public Type Generate()
        {
            var typeName = _generationNamespace + "." + _metadata.Name;

            _typeBuilder = _moduleBuilder.DefineType(
                typeName,
                TypeMetadata.TypeAttributes,
                _metadata.BaseType,
                _metadata.Interfaces);

            _methodMetadataFieldBuilders = MethodMetadataFieldGenerator.Generate(_typeBuilder, _metadata);

            StaticConstructorGenerator.Generate(_typeBuilder, _metadata, _methodMetadataFieldBuilders);

            _instanceFieldBuilders = InstanceFieldGenerator.Generate(_typeBuilder, _metadata);

            ConstructorGenerator.Generate(_typeBuilder, _metadata, _instanceFieldBuilders);

            _methodBuilders = GenerateMethods(_methodMetadataFieldBuilders, _instanceFieldBuilders);

            GenerateProperties(_methodBuilders);
            GenerateEvents(_methodBuilders);

            return _typeBuilder.CreateType();
        }

        private MethodMetadataMethodBuilderMap GenerateMethods(
            MethodMetadataFieldBuilderMap methodMetadataFieldBuilders,
            FieldMetadataFieldBuilderMap fieldMetadataFieldBuilders)
        {
            var dispatcherField = fieldMetadataFieldBuilders[_metadata.DispatcherField];

            var methods = _metadata.Methods;
            var methodCount = methods.Length;

            var result = new MethodMetadataMethodBuilderMap(methodCount);

            for (var i = 0; i < methodCount; i++)
            {
                var methodMetadata = methods[i];

                var methodGenerator = methodMetadata.CreateGenerator(
                    fieldMetadataFieldBuilders, 
                    methodMetadataFieldBuilders, 
                    dispatcherField);

                var methodBuilder = methodGenerator.Generate(_typeBuilder);

                result.Add(methodMetadata, methodBuilder);
            }

            return result;
        }

        private void GenerateProperties(MethodMetadataMethodBuilderMap methodBuilders)
        {
            var properties = _metadata.Properties;
            var propertyCount = properties.Length;

            for (var i = 0; i < propertyCount; i++)
            {
                var propertyMetadata = properties[i];

                GenerateProperty(propertyMetadata, methodBuilders);
            }
        }

        private void GenerateProperty(
            PropertyMetadata propertyMetadata,
            MethodMetadataMethodBuilderMap methodBuilders)
        {
            var getMethod = GetMethodBuilder(methodBuilders, propertyMetadata.GetGetMethod());
            var setMethod = GetMethodBuilder(methodBuilders, propertyMetadata.GetSetMethod());

            PropertyGenerator.Generate(_typeBuilder, propertyMetadata, getMethod, setMethod);
        }

        private static MethodBuilder GetMethodBuilder(
            MethodMetadataMethodBuilderMap methodBuilders, 
            MethodMetadata methodMetadata)
        {
            if (methodMetadata == null)
            {
                return null;
            }

            return methodBuilders[methodMetadata];
        }

        private void GenerateEvents(MethodMetadataMethodBuilderMap methodBuilders)
        {
            var events = _metadata.Events;
            var eventCount = events.Length;

            for (var i = 0; i < eventCount; i++)
            {
                var eventMetadata = events[i];

                GenerateEvent(eventMetadata, methodBuilders);
            }
        }

        private void GenerateEvent(
            EventMetadata eventMetadata,
            MethodMetadataMethodBuilderMap methodBuilders)
        {
            var addMethod = GetMethodBuilder(methodBuilders, eventMetadata.GetAddMethod());
            var removeMethod = GetMethodBuilder(methodBuilders, eventMetadata.GetRemoveMethod());
            var raiseMethod = GetMethodBuilder(methodBuilders, eventMetadata.GetRaiseMethod());

            EventGenerator.Generate(_typeBuilder, eventMetadata, addMethod, removeMethod, raiseMethod);
        }
    }
}
