using System.Diagnostics;
using CustomerManagement.Common.Abstract;

namespace CustomerManagement.Common.Logging
{
    public static class LogFmt
    {
        private const string MethodTag = "Method";
        private const string MessageTag = "Message";
        private const string ElapsedTag = "Elapsed";
        private const string ResultCountTag = "ResultCount";
        private const string CorrelationIdTag = "CorrelationId";

        public static string Method(string methodName)
        {
            return $"{MethodTag}={methodName}";
        }

        public static string Message(string message)
        {
            return $"{MessageTag}={message}";
        }

        public static string ResultCount(int resultCount)
        {
            return $"{ResultCountTag}={resultCount}";
        }

        public static string CorrelationId(string correlationId)
        {
            return $"{CorrelationIdTag}={correlationId}";
        }

        public static string CorrelationId(Guid correlationId)
        {
            return $"{CorrelationIdTag}={correlationId.ToString()}";
        }

        public static string CorrelationId(ITrackableRequest request)
        {
            return $"{CorrelationIdTag}={request.CorrelationId}";
        }

        public static string Elapsed(Stopwatch stopwatch)
        {
            return $"{ElapsedTag}={stopwatch.Elapsed}";
        }
    }
}