using System;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class CreateArrayExpression : IExpressionEmitter
    {
        private readonly Type _elementType;
        private readonly int _length;

        public CreateArrayExpression(Type elementType, int length)
        {
            _elementType = elementType;
            _length = length;
        }

        public void Emit(ILGenerator il)
        {
            il.Emit(OpCodes.Ldc_I4, _length);
            il.Emit(OpCodes.Newarr, _elementType);
        }
    }
}
