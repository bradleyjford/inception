using System;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    sealed class CreateArrayExpression : IExpressionEmitter
    {
        readonly Type _elementType;
        readonly int _length;

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