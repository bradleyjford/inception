using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
	internal sealed class InitializeInvokerArgumentArrayStatement : IStatementEmitter
	{
		private readonly LocalBuilder _argumentArrayLocal;
		private readonly ParameterInfo[] _parameters;

		public InitializeInvokerArgumentArrayStatement(
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
				
				var parameterType = parameter.IsOut || parameter.ParameterType.IsByRef ? 
					parameter.ParameterType.GetElementType() : 
					parameter.ParameterType;

				il.Emit(OpCodes.Ldloc, _argumentArrayLocal);
				il.Emit(OpCodes.Ldc_I4_S, i);
				il.Emit(OpCodes.Ldarg_S, i + 1);

				if (parameter.IsOut || parameter.ParameterType.IsByRef)
				{
					il.Emit(OpCodes.Ldobj, parameterType);
				}	

				if (parameterType.IsGenericParameter || parameterType.IsValueType)
				{
					il.Emit(OpCodes.Box, parameterType);
				}

				il.Emit(OpCodes.Stelem_Ref);
			}
		}
	}
}
