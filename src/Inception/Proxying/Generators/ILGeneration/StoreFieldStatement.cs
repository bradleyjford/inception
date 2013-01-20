using System;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class StoreFieldStatement : IStatementEmitter
    {
        private readonly FieldBuilder _field;
		private readonly IExpressionEmitter _valueEmitter;

        public StoreFieldStatement(FieldBuilder field, IExpressionEmitter valueEmitter)
        {
            _field = field;
            _valueEmitter = valueEmitter;
        }

        public void Emit(ILGenerator il)
        {
            if (_field.IsStatic)
            {
                _valueEmitter.Emit(il);
                il.Emit(OpCodes.Stsfld, _field);
            }
            else
            {
                il.Emit(OpCodes.Ldarg_0);
                _valueEmitter.Emit(il);
                il.Emit(OpCodes.Stfld, _field);
            }
        }
    }
}