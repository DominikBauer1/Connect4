using System;
using System.Runtime.Serialization;

namespace Connect4.Exceptions
{
    [Serializable]
    internal class InvalidGameMoveException : Exception
    {
        public InvalidGameMoveException()
        {
        }

        public InvalidGameMoveException(string message) : base(message)
        {
        }

        public InvalidGameMoveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidGameMoveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}