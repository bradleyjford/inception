using System;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    sealed class LoadNullExpression : IExpressionEmitter
    {
        public void Emit(ILGenerator il)
        {
            il.Emit(OpCodes.Ldnull);
        }
    }
}