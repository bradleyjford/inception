using System;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
    internal static class MethodMetadataFactory
    {
        public static MethodMetadata Create(
            IProxyDefinitionElement definitionElement, 
            FieldMetadata targetField, 
            MethodInfo method)
        {
            var classProxyDefinition = definitionElement as ClassProxyDefinition;

            if (classProxyDefinition != null)
            {
                return new ClassMethodMetadata(method);
            }

            var interfaceProxyDefinition = definitionElement as InterfaceProxyDefinition;

            if (interfaceProxyDefinition != null)
            {
                return new NonTargetedMethodMetadata(method);
            }
            
            var targetedClassProxyDefinition = definitionElement as TargetedClassProxyDefinition;

            if (targetedClassProxyDefinition != null)
            {
                var targetMethod = MemberLocator.LocateMatchingMethod(method, targetedClassProxyDefinition.TargetType);

                return new TargetedMethodMetadata(method, targetField, targetMethod);
            }

            var targetedInterfaceProxyDefinition = definitionElement as TargetedInterfaceProxyDefinition;

            if (targetedInterfaceProxyDefinition != null)
            {
                var targetMethod = MemberLocator.LocateMatchingMethod(method, targetedInterfaceProxyDefinition.TargetType);

                return new TargetedMethodMetadata(method, targetField, targetMethod);
            }

            var mixinInterfaveDefinition = definitionElement as MixinInterfaceDefinition;

            if (mixinInterfaveDefinition != null)
            {
                var targetMethod = MemberLocator.LocateMatchingMethod(method, mixinInterfaveDefinition.MixinType);

                return new TargetedMethodMetadata(method, targetField, targetMethod);
            }

            var nonTargetefInterfaceDefinition = definitionElement as NonTargetedInterfaceDefinition;

            if (nonTargetefInterfaceDefinition != null)
            {
                return new NonTargetedMethodMetadata(method);
            }

            throw new NotSupportedException();
        }
    }
}
