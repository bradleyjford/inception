using System;
using System.Reflection;
using System.Reflection.Emit;
using Inception.Proxying.Generators.ILGeneration;
using Inception.Proxying.Metadata;

namespace Inception.Proxying.Generators
{
    internal class DuckTypeMethodGenerator : MethodGenerator
    {
        private readonly DuckTypeMethodMetadata _methodMetadata;

        public DuckTypeMethodGenerator(DuckTypeMethodMetadata methodMetadata)
        {
            _methodMetadata = methodMetadata;
        }

        public override MethodBuilder Generate(TypeBuilder typeBuilder)
        {
            var methodBuilder = GenerateMethod(typeBuilder);

            typeBuilder.DefineMethodOverride(methodBuilder, _methodMetadata.Method);

            return methodBuilder;
        }

        private MethodBuilder GenerateMethod(TypeBuilder typeBuilder)
        {
            var methodBuilder = typeBuilder.DefineMethod(
                _methodMetadata.Name,
                _methodMetadata.MethodAttributes,
                CallingConventions.Standard,
                _methodMetadata.ReturnType,
                _methodMetadata.ParameterTypes);

            var parameters = _methodMetadata.Method.GetParameters();
            var parameterCount = parameters.Length;

            var parameterBuilders = new ParameterBuilder[parameterCount];

            for (var i = 0; i < parameterCount; i++)
            {
                parameterBuilders[i] = methodBuilder.DefineParameter(i + 1, ParameterAttributes.None, parameters[i].Name);
            }

            var il = methodBuilder.GetILGenerator();

            var args = new IExpressionEmitter[parameterCount];

            for (var i = 0; i < parameterCount; i++)
            {
                args[i] = new LoadArgumentExpression(parameterBuilders[i]);
            }

            il.Emit(OpCodes.Ldarg_0);

            if (_methodMetadata.ReturnType == typeof(void))
            {
                new CallMethodStatement(_methodMetadata.TargetMethod, args).Emit(il);

                il.Emit(OpCodes.Ldnull);

                new ReturnStatement().Emit(il);
            }
            else
            {
                new ReturnStatement(
                    new CallMethodExpression(_methodMetadata.TargetMethod, args)
                ).Emit(il);
            }

            return methodBuilder;
        }
    }
}
