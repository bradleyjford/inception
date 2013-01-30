using System;

namespace Inception.Proxying
{
    public class InterfaceProxyDefinition : ProxyDefinition
    {
        public InterfaceProxyDefinition(Type primaryInterfaceType, InterfaceDefinition[] interfaces)
            : base(primaryInterfaceType, interfaces)
        {
            
        }
    }
}
