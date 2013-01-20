using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
	internal sealed class StoreOutArgumentsStatement : IStatementEmitter
	{
		private readonly LocalBuilder _argumentArrayLocal;
		private readonly ParameterInfo[] _parameters;

		public StoreOutArgumentsStatement(
			LocalBuilder argumentArrayLocal, 
			ParameterInfo[] parameters)
		{
			_argumentArrayLocal = argumentArrayLocal;
			_parameters = parameters;
		}

		public void Emit(ILGenerator il)
		{
			for (var i = 0; i < _parameters.Length; ++i)
			{
				var parameter = _parameters[i];

				if (!IsOutOrRefParameter(parameter))
				{
					continue;
				}

				var parameterType = parameter.ParameterType.GetElementType();

				il.Emit(OpCodes.Ldarg_S, i + 1);
				il.Emit(OpCodes.Ldloc, _argumentArrayLocal);
				il.Emit(OpCodes.Ldc_I4_S, i);
				il.Emit(OpCodes.Ldelem_Ref);

				if (parameterType.IsValueType)
				{
					il.Emit(OpCodes.Unbox_Any, parameterType);
				}
				else
				{
					il.Emit(OpCodes.Castclass, parameterType);
				}

				if (parameterType == typeof(IntPtr))
				{
					il.Emit(OpCodes.Stind_I);
				}
				else if (parameterType == typeof(Byte))
				{
					il.Emit(OpCodes.Stind_I1);
				}
				else if (parameterType == typeof(Int16))
				{
					il.Emit(OpCodes.Stind_I2);
				}
				else if (parameterType == typeof(Int32))
				{
					il.Emit(OpCodes.Stind_I4);
				}
				else if (parameterType == typeof(Int64))
				{
					il.Emit(OpCodes.Stind_I8);
				}
				else if (parameterType == typeof(Single))
				{
					il.Emit(OpCodes.Stind_R4);
				}
				else if (parameterType == typeof(Double))
				{
					il.Emit(OpCodes.Stind_R8);
				}
				else if (parameterType.IsValueType)
				{
					il.Emit(OpCodes.Stobj, parameterType);
				}
				else
				{
					il.Emit(OpCodes.Stind_Ref);
				}
			}
		}

		private bool IsOutOrRefParameter(ParameterInfo parameter)
		{
			return parameter.IsOut || parameter.ParameterType.IsByRef;
		}
	}
}
