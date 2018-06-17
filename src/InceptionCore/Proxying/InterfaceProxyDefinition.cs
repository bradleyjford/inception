using System;

namespace InceptionCore.Proxying
{
    public class InterfaceProxyDefinition : ProxyDefinition
    {
        public InterfaceProxyDefinition(Type primaryInterfaceType, InterfaceDefinition[] interfaces)
            : base(primaryInterfaceType, interfaces)
        {
        }
    }
}