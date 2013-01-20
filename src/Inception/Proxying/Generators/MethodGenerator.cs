using System;
using System.Reflection;
using System.Reflection.Emit;
using Inception.Proxying.Generators.ILGeneration;
using Inception.Proxying.Metadata;

namespace Inception.Proxying.Generators
{
	internal abstract class MethodGenerator
	{
		public abstract MethodBuilder Generate(TypeBuilder typeBuilder);

		protected static MethodBuilder GenerateMethod(
			TypeBuilder typeBuilder,
			MethodMetadata methodMetadata,
			FieldBuilder dispatcherField,
			FieldBuilder methodInfoField,
			MethodInfo targetMethod)
		{
			var method = methodMetadata.Method;

			var parameters = method.GetParameters();
			var parameterCount = parameters.Length;

			var methodBuilder = typeBuilder.DefineMethod(
				methodMetadata.Name,
				methodMetadata.MethodAttributes,
				methodMetadata.ReturnType,
				methodMetadata.ParameterTypes);

			if (method.IsGenericMethodDefinition)
			{
				CloneGenericMethodArguments(method, methodBuilder);
			}

			for (var i = 0; i < parameterCount; i++)
			{
				methodBuilder.DefineParameter(i + 1, parameters[i].Attributes, parameters[i].Name);
			}

			var il = methodBuilder.GetILGenerator();

			var args = il.DeclareLocal(typeof(object[]));

			LocalBuilder returnValue = null;

			if (method.ReturnType != typeof(void))
			{
				returnValue = il.DeclareLocal(method.ReturnType);
			}

			new InitializeOutArgumentsStatement(parameters).Emit(il);

			new StoreLocalStatement(
				args,
				new CreateArrayExpression(typeof(object), parameterCount)
			).Emit(il);

			new InitializeInvokerArgumentArrayStatement(args, parameters).Emit(il);

			new InvokeDispatcherMethodStatement(
				dispatcherField,
				methodInfoField,
				method,
				args,
				returnValue,
				targetMethod
			).Emit(il);

			new StoreOutArgumentsStatement(args, parameters).Emit(il);

			if (method.ReturnType == typeof(void))
			{
				new ReturnStatement().Emit(il);
			}
			else
			{
				new ReturnStatement(new LoadLocalExpression(returnValue)).Emit(il);
			}

			return methodBuilder;
		}

		protected static GenericTypeParameterBuilder[] CloneGenericMethodArguments(MethodInfo source, MethodBuilder destination)
		{
			var genericArgs = source.GetGenericArguments();

			if (genericArgs.Length == 0)
			{
				return new GenericTypeParameterBuilder[0];
			}

			var names = new string[genericArgs.Length];

			for (var i = 0; i < genericArgs.Length; i++)
			{
				names[i] = genericArgs[i].Name;
			}

			var genericTypeParameterBuilders = destination.DefineGenericParameters(names);

			for (var i = 0; i < genericTypeParameterBuilders.Length; i++)
			{
				var genericArgConstraints = genericArgs[i].GetGenericParameterConstraints();

				for (var j = 0; j < genericArgConstraints.Length; j++)
				{
					genericTypeParameterBuilders[i].SetBaseTypeConstraint(genericArgConstraints[j]);
				}
			}

			return genericTypeParameterBuilders;
		}
	}
}
