using System;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    sealed class StoreLocalStatement : IStatementEmitter
    {
        readonly LocalBuilder _local;
        readonly IExpressionEmitter _valueExpression;

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