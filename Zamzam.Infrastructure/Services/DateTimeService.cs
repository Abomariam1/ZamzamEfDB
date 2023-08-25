using Zamzam.Application.Interfaces;

namespace Zamzam.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}