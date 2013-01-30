using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class CallMethodStatement : IStatementEmitter
    {
        private readonly MethodInfo _method;
        private readonly IExpressionEmitter[] _arguments;

        public CallMethodStatement(MethodInfo method, IExpressionEmitter[] arguments)
        {
            _method = method;
            _arguments = arguments;
        }

        public void Emit(ILGenerator il)
        {
            for (var i = 0; i < _arguments.Length; i++)
            {
                _arguments[i].Emit(il);
            }

            if (_method.IsStatic)
            {
                il.Emit(OpCodes.Call, _method);
            }
            else if (_method.IsVirtual)
            {
                il.Emit(OpCodes.Callvirt, _method);
            }
            else
            {
                il.Emit(OpCodes.Call, _method);
            }

            il.Emit(OpCodes.Pop);
        }
    }
}
