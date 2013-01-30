using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class InitializeOutArgumentsStatement : IStatementEmitter
    {
        private readonly ParameterInfo[] _parameters;

        public InitializeOutArgumentsStatement(ParameterInfo[] parameters)
        {
            _parameters = parameters;
        }

        public void Emit(ILGenerator il)
        {
            for (var i = 0; i < _parameters.Length; i++)
            {
                var parameter = _parameters[i];

                if (!parameter.IsOut || parameter.IsIn)
                {
                    continue;
                }

                var position = i + 1;

                switch (position)
                {
                    case 1:
                        il.Emit(OpCodes.Ldarg_1);
                        break;

                    case 2:
                        il.Emit(OpCodes.Ldarg_2);
                        break;

                    case 3:
                        il.Emit(OpCodes.Ldarg_3);
                        break;

                    default:
                        il.Emit(OpCodes.Ldarg_S, position);
                        break;
                }

                var parameterBaseType = parameter.ParameterType.IsByRef || parameter.IsOut
                    ? parameter.ParameterType.GetElementType()
                    : parameter.ParameterType;

                il.Emit(OpCodes.Initobj, parameterBaseType);
            }
        }
    }
}
