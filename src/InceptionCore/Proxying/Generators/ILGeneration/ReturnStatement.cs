using System;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    sealed class ReturnStatement : IStatementEmitter
    {
        readonly IExpressionEmitter _valueExpression;

        public ReturnStatement()
        {
        }

        public ReturnStatement(IExpressionEmitter valueExpression)
        {
            _valueExpression = valueExpression;
        }

        public void Emit(ILGenerator il)
        {
            _valueExpression?.Emit(il);

            il.Emit(OpCodes.Ret);
        }
    }
}