using System;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class LoadInt32Expression : IExpressionEmitter
    {
        private readonly int _value;

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
