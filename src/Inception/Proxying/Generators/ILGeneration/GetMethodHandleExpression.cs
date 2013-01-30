using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal sealed class GetMethodHandleExpression : IExpressionEmitter
    {
        private static readonly MethodInfo GenericGetMethodFromHandleMethodInfo =
            typeof(MethodBase).GetMethod("GetMethodFromHandle", new[]
            {                                                                                                                               
                typeof(RuntimeMethodHandle),
                typeof(RuntimeMethodHandle),
                typeof(RuntimeTypeHandle)
            });

        private static readonly MethodInfo GetMethodFromHandleMethodInfo =
            typeof(MethodBase).GetMethod("GetMethodFromHandle", new[]
            {
                typeof(RuntimeMethodHandle)
            });

        private readonly Type _type;
        private readonly MethodInfo _method;

        public GetMethodHandleExpression(Type type, MethodInfo method)
        {
            _type = type;
            _method = method;
        }

        public void Emit(ILGenerator il)
        {
            il.Emit(OpCodes.Ldtoken, _method);

            if (_type.IsGenericType)
            {
                il.Emit(OpCodes.Ldtoken, _type);
                il.Emit(OpCodes.Call, GenericGetMethodFromHandleMethodInfo);
            }
            else
            {
                il.Emit(OpCodes.Call, GetMethodFromHandleMethodInfo);
            }

            il.Emit(OpCodes.Castclass, typeof(MethodInfo));
        }
    }
}