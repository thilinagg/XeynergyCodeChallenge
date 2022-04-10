using System;
using XeynergyCodeChallenge.Application.Common.Interfaces;

namespace XeynergyCodeChallenge.Infrastructure.Services
{
    internal class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
