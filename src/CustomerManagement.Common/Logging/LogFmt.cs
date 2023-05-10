using System.Diagnostics;

namespace CustomerManagement.Common.Logging
{
    public static class LogFmt
    {
        private const string MethodTag = "Method";
        private const string MessageTag = "Message";
        private const string ElapsedTag = "Elapsed";

        public static string Method(string methodName)
        {
            return $"{MethodTag}={methodName}";
        }

        public static string Message(string message)
        {
            return $"{MessageTag}={message}";
        }

        public static string Elapsed(Stopwatch stopwatch)
        {
            return $"{ElapsedTag}={stopwatch.Elapsed}";
        }
    }
}