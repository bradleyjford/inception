using System;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    sealed class LoadLocalExpression : IExpressionEmitter
    {
        readonly LocalBuilder _local;

        public LoadLocalExpression(LocalBuilder local)
        {
            _local = local;
        }

        public void Emit(ILGenerator il)
        {
            il.Emit(OpCodes.Ldloc, _local);
        }
    }
}