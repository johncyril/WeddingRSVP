using System;
using System.Runtime.Serialization;

namespace WeddingRsvp.Api.Exceptions
{
    [Serializable]
    internal class MultipleInvitesFoundException : Exception
    {
        public MultipleInvitesFoundException()
        {
        }

        public MultipleInvitesFoundException(string message) : base(message)
        {
        }

        public MultipleInvitesFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MultipleInvitesFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}