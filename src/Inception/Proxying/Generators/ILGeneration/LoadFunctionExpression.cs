using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class LoadFunctionExpression : IExpressionEmitter
    {
        private static readonly ConstructorInfo DelegateConstructor =
            typeof(ProxyTargetInvocation).GetConstructors()[0];

        private readonly MethodInfo _method;

        public LoadFunctionExpression(MethodInfo method)
        {
            _method = method;
        }

        public void Emit(ILGenerator il)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldftn, _method);
            il.Emit(OpCodes.Newobj, DelegateConstructor);
        }
    }
}
