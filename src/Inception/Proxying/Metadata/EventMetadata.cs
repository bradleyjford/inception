using System;
using System.Diagnostics;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
    [DebuggerDisplay("{EventInfo.Name}")]
    internal class EventMetadata : MemberMetadata
    {
        private readonly EventInfo _eventInfo;
        private readonly MethodMetadata _addHandlerMethod;
        private readonly MethodMetadata _removeHandlerMethod;
        private readonly MethodMetadata _raiseHandlerMethod;

        private readonly EventAttributes _eventAttributes;

        public EventMetadata(
             EventInfo eventInfo,
            MethodMetadata addHandlerMethod,
            MethodMetadata removeHandlerMethod,
            MethodMetadata raiseHandlerMethod)
            : base(eventInfo)
        {
            _eventInfo = eventInfo;
            _addHandlerMethod = addHandlerMethod;
            _removeHandlerMethod = removeHandlerMethod;
            _raiseHandlerMethod = raiseHandlerMethod;

            _eventAttributes = _eventInfo.Attributes;
        }

        public override void UseExplicitInterfaceImplementation()
        {
            base.UseExplicitInterfaceImplementation();
        
            UseExplicitImplementationForHandlerMethod(_addHandlerMethod);
            UseExplicitImplementationForHandlerMethod(_removeHandlerMethod);
            UseExplicitImplementationForHandlerMethod(_raiseHandlerMethod);
        }

        private void UseExplicitImplementationForHandlerMethod(MethodMetadata handlerMethod)
        {
            if (handlerMethod != null)
            {
                handlerMethod.UseExplicitInterfaceImplementation();
            }
        }

        public EventInfo EventInfo
        {
            get { return _eventInfo; }
        }

        public Type EventHandlerType
        {
            get { return _eventInfo.EventHandlerType; }
        }

        public EventAttributes EventAttributes
        {
            get { return _eventAttributes; }
        }

        public MethodMetadata GetAddMethod()
        {
            return _addHandlerMethod;
        }

        public MethodMetadata GetRemoveMethod()
        {
            return _removeHandlerMethod;
        }

        public MethodMetadata GetRaiseMethod()
        {
            return _raiseHandlerMethod;
        }
    }
}
