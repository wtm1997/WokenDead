using System;
using System.Diagnostics;

namespace CommonLogic
{
    public static class Log
    {
        public static event Action<string> onDebug;
        public static event Action<string> onInfo;
        public static event Action<string> onError;

        [Conditional("DEBUG")]
        public static void Debug(string message)
        {
            onDebug?.Invoke(message);
        }

        public static void Info(string message)
        {
            onInfo?.Invoke(message);
        }

        public static void Error(string message)
        {
            onError?.Invoke(message);
        }
    }
}