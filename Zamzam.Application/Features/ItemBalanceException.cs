using System.Runtime.Serialization;

namespace Zamzam.Application.Features
{
    [Serializable]
    internal class ItemBalanceException : Exception
    {
        public ItemBalanceException()
        {
        }

        public ItemBalanceException(string? message) : base(message)
        {
        }

        public ItemBalanceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ItemBalanceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}