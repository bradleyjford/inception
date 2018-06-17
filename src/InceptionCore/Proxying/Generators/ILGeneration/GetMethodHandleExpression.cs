using System;
using System.Reflection;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    sealed class GetMethodHandleExpression : IExpressionEmitter
    {
        static readonly MethodInfo GenericGetMethodFromHandleMethodInfo =
            typeof(MethodBase).GetMethod("GetMethodFromHandle", new[]
            {
                typeof(RuntimeMethodHandle),
                typeof(RuntimeMethodHandle),
                typeof(RuntimeTypeHandle)
            });

        static readonly MethodInfo GetMethodFromHandleMethodInfo =
            typeof(MethodBase).GetMethod("GetMethodFromHandle", new[]
            {
                typeof(RuntimeMethodHandle)
            });

        readonly MethodInfo _method;

        readonly Type _type;

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