using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class CallMethodExpression : IExpressionEmitter
    {
        private readonly MethodInfo _method;
        private readonly IExpressionEmitter[] _arguments;

        public CallMethodExpression(MethodInfo method, IExpressionEmitter[] arguments)
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

            if (_method.ReturnType.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, _method.ReturnType);
            }
            else if (_method.ReturnType != typeof(object))
            {
                il.Emit(OpCodes.Castclass, _method.ReturnType);
            }
        }
    }
}
