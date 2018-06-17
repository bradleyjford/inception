using System;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    sealed class LoadInt32Expression : IExpressionEmitter
    {
        readonly int _value;

        public LoadInt32Expression(int value)
        {
            _value = value;
        }

        public void Emit(ILGenerator il)
        {
            il.Emit(OpCodes.Ldc_I4, _value);
        }
    }
}