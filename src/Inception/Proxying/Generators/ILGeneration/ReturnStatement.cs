using System;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
	internal sealed class ReturnStatement : IStatementEmitter
	{
		private readonly IExpressionEmitter _valueExpression;

		public ReturnStatement()
		{
			
		}

		public ReturnStatement(IExpressionEmitter valueExpression)
		{
			_valueExpression = valueExpression;
		}

		public void Emit(ILGenerator il)
		{
			if (_valueExpression != null)
			{
				_valueExpression.Emit(il);
			}

			il.Emit(OpCodes.Ret);
		}
	}
}
