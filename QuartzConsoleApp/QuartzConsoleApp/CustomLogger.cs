using System;

namespace QuartzConsoleApp
{
    public class CustomLogger
    {
        private readonly string _prefix;

        public CustomLogger(string prefix)
        {
            _prefix = prefix;
        }

        public void Log(object message = null)
        {
            Console.WriteLine($"({DateTime.Now.ToLongTimeString()}) [{_prefix}] {message}");
        }

        public static void LogAndBreak(object message = null)
        {
            Console.WriteLine($"{message}\n");
        }
    }
}
