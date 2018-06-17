using System;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    sealed class CastOrUnboxExpression : IExpressionEmitter
    {
        readonly Type _targetType;
        readonly IExpressionEmitter _valueExpression;

        public CastOrUnboxExpression(Type targetType, IExpressionEmitter valueExpression)
        {
            _targetType = targetType;
            _valueExpression = valueExpression;
        }

        public void Emit(ILGenerator il)
        {
            _valueExpression.Emit(il);

            if (_targetType.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, _targetType);
            }
            else
            {
                il.Emit(OpCodes.Castclass, _targetType);
            }
        }
    }
}