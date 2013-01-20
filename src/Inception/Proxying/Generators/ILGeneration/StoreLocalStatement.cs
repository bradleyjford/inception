using System;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class StoreLocalStatement : IStatementEmitter
    {
        private readonly LocalBuilder _local;
        private readonly IExpressionEmitter _valueExpression;

        public StoreLocalStatement(LocalBuilder local, IExpressionEmitter valueExpression)
        {
            _local = local;
            _valueExpression = valueExpression;
        }

        public void Emit(ILGenerator il)
        {
            _valueExpression.Emit(il);
            il.Emit(OpCodes.Stloc, _local);
        }
    }
}
