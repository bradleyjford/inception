using System;

namespace Inception.Proxying
{
	internal class InterfaceProxyDefinition : ProxyDefinition
	{
		public InterfaceProxyDefinition(Type primaryInterfaceType, InterfaceDefinition[] interfaces)
			: base(primaryInterfaceType, interfaces)
		{
			
		}
	}
}
