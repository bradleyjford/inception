using System;
using System.Reflection;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    sealed class CallMethodStatement : IStatementEmitter
    {
        readonly IExpressionEmitter[] _arguments;
        readonly MethodInfo _method;

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