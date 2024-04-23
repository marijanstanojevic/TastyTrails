using TastyTrails.Application.Common.Interfaces;

namespace TastyTrails.Infrastructure.Services
{
    public class DateTimeOffsetClock : IClock
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
