using System;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class LoadFieldExpression : IExpressionEmitter
    {
        private readonly FieldBuilder _field;

        public LoadFieldExpression(FieldBuilder field)
        {
            _field = field;
        }

        public void Emit(ILGenerator il)
        {
            if (_field.IsStatic)
            {
                il.Emit(OpCodes.Ldsfld, _field);
            }
            else
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, _field);
            }
        }
    }
}