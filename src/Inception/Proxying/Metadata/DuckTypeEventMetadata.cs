using System;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
    internal class DuckTypeEventMetadata : EventMetadata
    {
        private readonly EventInfo _targetEvent;

        public DuckTypeEventMetadata(
            EventInfo @event, 
            EventInfo targetEvent,
            MethodMetadata addHandlerMethod, 
            MethodMetadata removeHandlerMethod, 
            MethodMetadata raiseHandlerMethod) 
            : base(@event, addHandlerMethod, removeHandlerMethod, raiseHandlerMethod)
        {
            _targetEvent = targetEvent;
        }

        public EventInfo TargetEvent
        {
            get { return _targetEvent; }
        }
    }
}
