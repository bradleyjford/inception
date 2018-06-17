using System;
using System.Diagnostics;
using System.Reflection;

namespace InceptionCore.Proxying.Metadata
{
    [DebuggerDisplay("{EventInfo.Name}")]
    class EventMetadata : MemberMetadata
    {
        readonly MethodMetadata _addHandlerMethod;

        readonly MethodMetadata _raiseHandlerMethod;
        readonly MethodMetadata _removeHandlerMethod;

        public EventMetadata(
            EventInfo eventInfo,
            MethodMetadata addHandlerMethod,
            MethodMetadata removeHandlerMethod,
            MethodMetadata raiseHandlerMethod)
            : base(eventInfo)
        {
            EventInfo = eventInfo;
            _addHandlerMethod = addHandlerMethod;
            _removeHandlerMethod = removeHandlerMethod;
            _raiseHandlerMethod = raiseHandlerMethod;

            EventAttributes = EventInfo.Attributes;
        }

        public EventInfo EventInfo { get; }

        public Type EventHandlerType => EventInfo.EventHandlerType;

        public EventAttributes EventAttributes { get; }

        public override void UseExplicitInterfaceImplementation()
        {
            base.UseExplicitInterfaceImplementation();

            UseExplicitImplementationForHandlerMethod(_addHandlerMethod);
            UseExplicitImplementationForHandlerMethod(_removeHandlerMethod);
            UseExplicitImplementationForHandlerMethod(_raiseHandlerMethod);
        }

        void UseExplicitImplementationForHandlerMethod(MethodMetadata handlerMethod)
        {
            if (handlerMethod != null)
            {
                handlerMethod.UseExplicitInterfaceImplementation();
            }
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