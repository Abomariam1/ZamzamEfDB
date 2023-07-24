using MediatR;

namespace Zamzam.Core.Entites.Interfaces
{
    public class BaseEvent : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
