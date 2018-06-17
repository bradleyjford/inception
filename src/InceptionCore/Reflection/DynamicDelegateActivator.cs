using System;
using System.Reflection;
using System.Reflection.Emit;

namespace InceptionCore.Reflection
{
    public sealed class DynamicDelegateActivator
    {
        readonly ConstructorInfo _constructorInfo;

        readonly Lazy<Func<object[], object>> _delegate;
        readonly Type _type;

        public DynamicDelegateActivator(Type type, ConstructorInfo constructorInfo)
        {
            _type = type;
            _constructorInfo = constructorInfo;

            _delegate = new Lazy<Func<object[], object>>(CreateDelegate);
        }

        Func<object[], object> CreateDelegate()
        {
            var method = new DynamicMethod(
                _type.Name + "__DynamicDelegateActivator",
                _type,
                new[] {typeof(object[])});

            var il = method.GetILGenerator();

            var parameters = _constructorInfo.GetParameters();

            for (var i = 0; i < parameters.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_0);

                switch (i)
                {
                    case 0:
                        il.Emit(OpCodes.Ldc_I4_0);
                        break;
                    case 1:
                        il.Emit(OpCodes.Ldc_I4_1);
                        break;
                    case 2:
                        il.Emit(OpCodes.Ldc_I4_2);
                        break;
                    case 3:
                        il.Emit(OpCodes.Ldc_I4_3);
                        break;
                    case 4:
                        il.Emit(OpCodes.Ldc_I4_4);
                        break;
                    case 5:
                        il.Emit(OpCodes.Ldc_I4_5);
                        break;
                    case 6:
                        il.Emit(OpCodes.Ldc_I4_6);
                        break;
                    case 7:
                        il.Emit(OpCodes.Ldc_I4_7);
                        break;
                    case 8:
                        il.Emit(OpCodes.Ldc_I4_8);
                        break;
                    default:
                        il.Emit(OpCodes.Ldc_I4, i);
                        break;
                }

                il.Emit(OpCodes.Ldelem_Ref);

                var parameterType = parameters[i].ParameterType;

                if (parameterType.IsValueType)
                {
                    il.Emit(OpCodes.Unbox_Any, parameterType);
                }
                else
                {
                    il.Emit(OpCodes.Castclass, parameterType);
                }
            }

            il.Emit(OpCodes.Newobj, _constructorInfo);

            il.Emit(OpCodes.Ret);

            return (Func<object[], object>)method.CreateDelegate(typeof(Func<object[], object>));
        }

        public object CreateInstance(object[] constructorArguments)
        {
            return _delegate.Value(constructorArguments);
        }
    }
}