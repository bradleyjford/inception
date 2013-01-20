using System;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class LoadLocalExpression : IExpressionEmitter
    {
        private readonly LocalBuilder _local;

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
