using System;
using System.Reflection;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    public sealed class InvokeDispatcherMethodStatement : IStatementEmitter
    {
        static readonly MethodInfo DispatcherInvocationMethod =
            typeof(IProxyDispatcher).GetMethod("MethodInvocation");

        readonly LocalBuilder _args;
        readonly FieldBuilder _dispatcherField;

        readonly MethodInfo _method;
        readonly FieldBuilder _methodInfoField;
        readonly LocalBuilder _returnValue;
        readonly MethodInfo _targetMethod;

        public InvokeDispatcherMethodStatement(
            FieldBuilder dispatcherField,
            FieldBuilder methodInfoField,
            MethodInfo method,
            LocalBuilder args,
            LocalBuilder returnValue,
            MethodInfo targetMethod)
        {
            _method = method;
            _dispatcherField = dispatcherField;
            _methodInfoField = methodInfoField;
            _args = args;
            _returnValue = returnValue;
            _targetMethod = targetMethod;
        }

        public void Emit(ILGenerator il)
        {
            var loadTargetMethodExpression = _targetMethod == null
                ? new LoadNullExpression()
                : (IExpressionEmitter)new LoadFunctionExpression(_targetMethod);

            if (_method.ReturnType == typeof(void))
            {
                new CallMethodStatement(DispatcherInvocationMethod,
                    new[]
                    {
                        new LoadFieldExpression(_dispatcherField),
                        new LoadArgumentExpression(0),
                        new LoadFieldExpression(_methodInfoField),
                        new LoadLocalExpression(_args),
                        loadTargetMethodExpression
                    }
                ).Emit(il);
            }
            else
            {
                new StoreLocalStatement(_returnValue,
                    new CastOrUnboxExpression(_method.ReturnType,
                        new CallMethodExpression(DispatcherInvocationMethod,
                            new[]
                            {
                                new LoadFieldExpression(_dispatcherField),
                                new LoadArgumentExpression(0),
                                new LoadFieldExpression(_methodInfoField),
                                new LoadLocalExpression(_args),
                                loadTargetMethodExpression
                            }
                        )
                    )
                ).Emit(il);
            }
        }
    }
}