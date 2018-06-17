using System;

namespace InceptionCore.Proxying.Metadata
{
    class DispatcherFieldMetadata : FieldMetadata
    {
        public DispatcherFieldMetadata()
            : base("_dispatcher", typeof(IProxyDispatcher), PrivateReadonlyFieldAttributes)
        {
        }
    }
}