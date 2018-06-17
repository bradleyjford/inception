using System;
using System.Reflection;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    sealed class LoadFunctionExpression : IExpressionEmitter
    {
        static readonly ConstructorInfo DelegateConstructor =
            typeof(ProxyTargetInvocation).GetConstructors()[0];

        readonly MethodInfo _method;

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