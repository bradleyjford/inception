using System;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class LoadArgumentExpression : IExpressionEmitter
    {
        private readonly int _index;

        public LoadArgumentExpression(ParameterBuilder parameter)
        {
            _index = parameter.Position;
        }

        public LoadArgumentExpression(int index)
        {
            _index = index;
        }

        public void Emit(ILGenerator il)
        {
            switch (_index)
            {
                case 0:
                    il.Emit(OpCodes.Ldarg_0);
                    break;

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
                    il.Emit(OpCodes.Ldarg_S, _index);
                    break;
            }
        }
    }
}