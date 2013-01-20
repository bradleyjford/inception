using System;
using Inception.Reflection;

namespace Inception.Proxying
{
	public interface IProxyActivator
	{
		object CreateInstance(Type type, ArgumentCollection constructorArguments);
	}
}
