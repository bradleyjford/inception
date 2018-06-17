using System;
using System.Reflection;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    sealed class NewObjectExpression : IExpressionEmitter
    {
        readonly ConstructorInfo _constructor;

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