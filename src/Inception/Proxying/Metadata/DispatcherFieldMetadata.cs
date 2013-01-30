using System;

namespace Inception.Proxying.Metadata
{
    internal class DispatcherFieldMetadata : FieldMetadata
    {
        public DispatcherFieldMetadata() 
            : base("_dispatcher", typeof(IProxyDispatcher), PrivateReadonlyFieldAttributes)
        {
        }
    }
}
