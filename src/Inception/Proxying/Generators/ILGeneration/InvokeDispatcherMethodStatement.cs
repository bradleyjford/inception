using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
	public sealed class InvokeDispatcherMethodStatement : IStatementEmitter
	{
		private static readonly MethodInfo DispatcherInvocationMethod =
			typeof(IProxyDispatcher).GetMethod("MethodInvocation");

		private readonly MethodInfo _method;
		private readonly FieldBuilder _dispatcherField;
		private readonly FieldBuilder _methodInfoField;
		private readonly LocalBuilder _args;
		private readonly LocalBuilder _returnValue;
		private readonly MethodInfo _targetMethod;

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
			var loadTargetMethodExpression = _targetMethod == null ?
					(IExpressionEmitter)new LoadNullExpression() :
					(IExpressionEmitter)new LoadFunctionExpression(_targetMethod);

			if (_method.ReturnType == typeof(void))
			{
				new CallMethodStatement(DispatcherInvocationMethod,
					new IExpressionEmitter[]
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
							new IExpressionEmitter[]
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