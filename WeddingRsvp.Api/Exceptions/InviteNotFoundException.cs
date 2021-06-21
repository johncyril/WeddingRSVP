using System;
using System.Runtime.Serialization;

namespace WeddingRsvp.Api.Exceptions
{
    [Serializable]
    internal class InviteNotFoundException : Exception
    {
        public InviteNotFoundException()
        {
        }

        public InviteNotFoundException(string message) : base(message)
        {
        }

        public InviteNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InviteNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}