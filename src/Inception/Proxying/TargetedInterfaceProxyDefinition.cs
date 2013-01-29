using System;

namespace Inception.Proxying
{
	public class TargetedInterfaceProxyDefinition : TargetedProxyDefinition
	{
		public TargetedInterfaceProxyDefinition(
			Type type, 
			Type targetType, 
			InterfaceDefinition[] interfaces) 
			: base(type, targetType, interfaces)
		{
			if (!type.IsInterface)
			{
				throw new ArgumentException("Specified type must be an interface type.", "type");
			}

			if (targetType.IsAssignableFrom(type))
			{
				throw new ArgumentException("Specified targetType must implement type interface.", "targetType");
			}
		}
	}
}
