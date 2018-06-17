using System;
using System.Reflection;
using System.Reflection.Emit;
using InceptionCore.Proxying.Generators.ILGeneration;
using InceptionCore.Proxying.Metadata;

namespace InceptionCore.Proxying.Generators
{
    class ConstructorGenerator
    {
        public static void Generate(TypeBuilder typeBuilder,
            TypeMetadata metadata,
            FieldMetadataFieldBuilderMap fieldMetadataFieldBuilderMap)
        {
            var constructorCount = metadata.Constructors.Length;

            for (var i = 0; i < constructorCount; i++)
            {
                var constructor = metadata.Constructors[i];

                GenerateConstructor(typeBuilder, metadata, constructor, fieldMetadataFieldBuilderMap);
            }
        }

        static void GenerateConstructor(
            TypeBuilder typeBuilder,
            TypeMetadata typeMetadata,
            ConstructorMetadata constructorMetadata,
            FieldMetadataFieldBuilderMap fieldBuilders)
        {
            var constructor = typeBuilder.DefineConstructor(
                constructorMetadata.MethodAttributes,
                CallingConventions.Standard,
                constructorMetadata.ParameterTypes);

            DefineParameterNames(constructor, constructorMetadata);

            var il = constructor.GetILGenerator();

            InitializeInstanceFields(il, typeMetadata, constructorMetadata, fieldBuilders);

            if (constructorMetadata.CallBaseConstructor)
            {
                GenerateBaseConstructorCall(il, constructorMetadata);
            }

            il.Emit(OpCodes.Ret);
        }

        static void DefineParameterNames(
            ConstructorBuilder constructor,
            ConstructorMetadata constructorMetadata)
        {
            for (var i = 0; i < constructorMetadata.Parameters.Length; i++)
            {
                var parameter = constructorMetadata.Parameters[i];

                constructor.DefineParameter(parameter.Sequence, ParameterAttributes.None, parameter.Name);
            }
        }

        static void GenerateBaseConstructorCall(ILGenerator il, ConstructorMetadata constructorMetadata)
        {
            il.Emit(OpCodes.Ldarg_0);

            for (var i = 0; i < constructorMetadata.Parameters.Length; i++)
            {
                var parameter = constructorMetadata.Parameters[i] as ConstructorBaseParameterMetadata;

                if (parameter != null)
                {
                    new LoadArgumentExpression(parameter.Sequence).Emit(il);
                }
            }

            il.Emit(OpCodes.Call, constructorMetadata.ConstructorInfo);
        }

        static void InitializeInstanceFields(
            ILGenerator il,
            TypeMetadata typeMetadata,
            ConstructorMetadata constructorMetadata,
            FieldMetadataFieldBuilderMap fieldBuilders)
        {
            for (var i = 0; i < constructorMetadata.Parameters.Length; i++)
            {
                var parameter = constructorMetadata.Parameters[i];

                var dispatcherParameter = parameter as ConstructorDispatcherParameterMetadata;
                var targetParameter = parameter as ConstructorTargetParameterMetadata;

                if (dispatcherParameter != null)
                {
                    InitializeField(il, dispatcherParameter.InstanceField, dispatcherParameter, fieldBuilders);
                }
                else if (targetParameter != null)
                {
                    InitializeField(il, targetParameter.InstanceField, targetParameter, fieldBuilders);
                }
            }

            for (var i = 0; i < typeMetadata.Targets.Length; i++)
            {
                var target = typeMetadata.Targets[i];

                if (target.IsProxyInstantiated)
                {
                    InitializeTarget(il, target.InstanceField, target.TargetType, fieldBuilders);
                }
            }
        }

        static void InitializeField(
            ILGenerator il,
            FieldMetadata instanceField,
            ConstructorParameterMetadata dispatcherParameter,
            FieldMetadataFieldBuilderMap fieldBuilders)
        {
            var fieldBuilder = fieldBuilders[instanceField];

            new StoreFieldStatement(fieldBuilder,
                new LoadArgumentExpression(dispatcherParameter.Sequence)
            ).Emit(il);
        }

        static void InitializeTarget(
            ILGenerator il,
            FieldMetadata instanceField,
            Type targetType,
            FieldMetadataFieldBuilderMap fieldBuilders)
        {
            var fieldBuilder = fieldBuilders[instanceField];

            var defaultConstructor = targetType.GetConstructor(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                Type.EmptyTypes,
                null);

            new StoreFieldStatement(fieldBuilder,
                new NewObjectExpression(defaultConstructor)
            ).Emit(il);
        }
    }
}