using System;
using System.Reflection;
using System.Reflection.Emit;
using Inception.Proxying.Metadata;

namespace Inception.Proxying.Generators
{
	internal class TargetedInvocationMethodGenerator : MethodGenerator
	{
		private readonly TargetedMethodMetadata _methodMetadata;
		private readonly FieldBuilder _targetField;

		public TargetedInvocationMethodGenerator(
			TargetedMethodMetadata methodMetadata, 
			FieldBuilder targetField)
		{
			_methodMetadata = methodMetadata;
			_targetField = targetField;
		}

		public override MethodBuilder Generate(TypeBuilder typeBuilder)
		{
			var method = _methodMetadata.Method;

			var methodBuilder = typeBuilder.DefineMethod("<Proxy>" + _methodMetadata.Name,
				MethodAttributes.Private | MethodAttributes.HideBySig,
				typeof(object),
				new[] { typeof(object[]) });

			if (method.IsGenericMethodDefinition)
			{
				CloneGenericMethodArguments(method, methodBuilder);
			}

			methodBuilder.DefineParameter(1, ParameterAttributes.None, "args");

			var il = methodBuilder.GetILGenerator();

			il.DeclareLocal(typeof(object));

			var parameters = method.GetParameters();

			var parameterCount = parameters.Length;

			for (var i = 0; i < parameterCount; i++)
			{
				var parameter = parameters[i];

				if (parameter.IsOut || parameter.ParameterType.IsByRef)
				{
					il.DeclareLocal(parameter.ParameterType.GetElementType());
				}
			}

			// Copy ref values into argument array
			var localIndex = 1;

			for (var i = 0; i < parameterCount; i++)
			{
				var parameter = parameters[i];

				if (parameter.ParameterType.IsByRef && !parameter.IsOut)
				{
					il.Emit(OpCodes.Ldarg_1);
					il.Emit(OpCodes.Ldc_I4_S, i);
					il.Emit(OpCodes.Ldelem_Ref);

					if (parameter.ParameterType.GetElementType().IsValueType)
					{
						il.Emit(OpCodes.Unbox_Any, parameter.ParameterType.GetElementType());
					}
					else
					{
						il.Emit(OpCodes.Castclass, parameter.ParameterType.GetElementType());
					}

					il.Emit(OpCodes.Stloc_S, localIndex++);
				}
			}

			// this
			il.Emit(OpCodes.Ldarg_0);

			il.Emit(OpCodes.Ldfld, _targetField);

			// Arguments
			localIndex = 1;

			for (var i = 0; i < parameterCount; i++)
			{
				var parameter = parameters[i];

				if (parameter.IsOut || parameter.ParameterType.IsByRef)
				{
					il.Emit(OpCodes.Ldloca_S, localIndex++);
				}
				else
				{
					il.Emit(OpCodes.Ldarg_1);
					il.Emit(OpCodes.Ldc_I4_S, i);
					il.Emit(OpCodes.Ldelem_Ref);

					if (parameter.ParameterType.IsValueType ||
						parameter.ParameterType.IsGenericParameter)
					{
						il.Emit(OpCodes.Unbox_Any, parameter.ParameterType);
					}
				}
			}

			il.Emit(OpCodes.Call, _methodMetadata.TargetMethod);

			if (method.ReturnType == typeof(void))
			{
				il.Emit(OpCodes.Nop);
				il.Emit(OpCodes.Ldnull);
			}
			else if (method.ReturnType.IsValueType)
			{
				il.Emit(OpCodes.Box, method.ReturnType);
			}

			il.Emit(OpCodes.Stloc_0);

			// Out/Ref values
			localIndex = 1;

			for (var i = 0; i < parameterCount; ++i)
			{
				var parameter = parameters[i];

				if (parameter.IsOut || parameter.ParameterType.IsByRef)
				{
					il.Emit(OpCodes.Ldarg_1);
					il.Emit(OpCodes.Ldc_I4_S, i);
					il.Emit(OpCodes.Ldloc_S, localIndex++);

					if (parameter.ParameterType.GetElementType().IsValueType)
					{
						il.Emit(OpCodes.Box, parameter.ParameterType.GetElementType());
					}

					il.Emit(OpCodes.Stelem_Ref);
				}
			}

			il.Emit(OpCodes.Ldloc_0);
			il.Emit(OpCodes.Ret);

			return methodBuilder;
		}
	}
}
