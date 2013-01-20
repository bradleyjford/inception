using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
	internal sealed class NewObjectExpression : IExpressionEmitter
	{
		private readonly ConstructorInfo _constructor;

		public NewObjectExpression(ConstructorInfo constructor)
		{
			_constructor = constructor;
		}

		public void Emit(ILGenerator il)
		{
			il.Emit(OpCodes.Newobj, _constructor);
		}
	}
}
