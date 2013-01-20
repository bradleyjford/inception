using System;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class LoadNullExpression : IExpressionEmitter 
    {
        public void Emit(ILGenerator il)
        {
            il.Emit(OpCodes.Ldnull);
        }
    }
}
