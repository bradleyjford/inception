using System;
using System.Reflection;

namespace Inception.Proxying
{
	public interface IProxyDispatcher
	{
		object MethodInvocation(
			object target,
			MethodInfo method, 
			object[] arguments, 
			ProxyTargetInvocation proxyTargetInvocation);
	}
}