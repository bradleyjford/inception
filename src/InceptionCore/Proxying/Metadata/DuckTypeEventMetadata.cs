using System;
using System.Reflection;

namespace InceptionCore.Proxying.Metadata
{
    class DuckTypeEventMetadata : EventMetadata
    {
        public DuckTypeEventMetadata(
            EventInfo @event,
            EventInfo targetEvent,
            MethodMetadata addHandlerMethod,
            MethodMetadata removeHandlerMethod,
            MethodMetadata raiseHandlerMethod)
            : base(@event, addHandlerMethod, removeHandlerMethod, raiseHandlerMethod)
        {
            TargetEvent = targetEvent;
        }

        public EventInfo TargetEvent { get; }
    }
}