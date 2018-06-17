using System;
using System.Reflection.Emit;
using InceptionCore.Proxying.Metadata;

namespace InceptionCore.Proxying.Generators
{
    static class EventGenerator
    {
        public static void Generate(
            TypeBuilder typeBuilder,
            EventMetadata eventMetadata,
            MethodBuilder addMethod,
            MethodBuilder removeMethod,
            MethodBuilder raiseMethod)
        {
            var @event = typeBuilder.DefineEvent(
                eventMetadata.Name,
                eventMetadata.EventAttributes,
                eventMetadata.EventHandlerType);

            if (addMethod != null)
            {
                @event.SetAddOnMethod(addMethod);
            }

            if (removeMethod != null)
            {
                @event.SetRemoveOnMethod(removeMethod);
            }

            if (raiseMethod != null)
            {
                @event.SetRaiseMethod(raiseMethod);
            }
        }
    }
}