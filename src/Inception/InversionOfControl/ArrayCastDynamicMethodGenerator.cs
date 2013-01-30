using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Inception.InversionOfControl
{
    public static class ArrayCastDynamicMethodGenerator
    {
        public static Func<object[], object> Generate(Type elementType)
        {
            var returnType = elementType.MakeArrayType();

            var method = new DynamicMethod(
                "CastArray",
                MethodAttributes.Static | MethodAttributes.Public,
                CallingConventions.Standard,
                returnType,
                new[] { typeof(object[]) },
                typeof(ArrayCastDynamicMethodGenerator),
                false);

            var ilGen = method.GetILGenerator();

            ilGen.Emit(OpCodes.Nop);
            ilGen.Emit(OpCodes.Ldarg_0);

            var castMethod = typeof(ArrayCastDynamicMethodGenerator)
                .GetMethod("ToStronglyTypedArray", BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(elementType);

            ilGen.EmitCall(OpCodes.Call, castMethod, null);

            ilGen.Emit(OpCodes.Ret);

            return (Func<object[], object>)
                method.CreateDelegate(typeof(Func<object[], object>));
        }

        private static T[] ToStronglyTypedArray<T>(object[] items)
        {
            var result = new T[items.Length];

            for (var i = 0; i < items.Length; i++)
            {
                result[i] = (T)items[i];
            }

            return result;
        }
    }
}
