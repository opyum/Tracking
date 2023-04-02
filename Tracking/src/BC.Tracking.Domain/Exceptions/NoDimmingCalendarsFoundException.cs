using System;
using System.Runtime.Serialization;

namespace Connexity.BC.Tracking.Domain.Exceptions
{
    [Serializable]
    public sealed class NoTrackingsFoundException : Exception
    {
        public NoTrackingsFoundException(string message) : base(message)
        {
        }

        private NoTrackingsFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}