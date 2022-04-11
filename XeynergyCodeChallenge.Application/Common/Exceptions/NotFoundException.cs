using System;

namespace XeynergyCodeChallenge.Application.Common.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
