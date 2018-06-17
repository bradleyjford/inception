using System;
using System.Runtime.Serialization;

namespace InceptionCore.Aspects
{
    [Serializable]
    public class AspectException : Exception
    {
        public AspectException()
        {
        }

        public AspectException(string message)
            : base(message)
        {
        }

        public AspectException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AspectException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }    
}
