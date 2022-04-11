using System;

namespace XeynergyCodeChallenge.WebUI.Common
{
    public class Envelope<T>
    {
        public T Result { get; }
        public string ErrorMessage { get; }
        public DateTime TimeGenerated { get; }
        public bool Success { get; }

        protected internal Envelope(T result, string errorMessage, bool success = false)
        {
            Result = result;
            ErrorMessage = errorMessage;
            TimeGenerated = DateTime.Now;
            Success = success;
        }
    }

    public class Envelope : Envelope<string>
    {
        protected Envelope(string errorMessage, bool success = false)
            : base(string.Empty, errorMessage, success) { }

        public static Envelope<T> Ok<T>(T result) =>
            new(result, string.Empty, success: true);

        public static Envelope<T> Error<T>(string errorMessage) =>
            new(default, errorMessage);
    }
}
