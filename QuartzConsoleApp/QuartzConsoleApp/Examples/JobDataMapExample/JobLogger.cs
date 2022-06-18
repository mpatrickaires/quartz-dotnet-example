using System;

namespace QuartzConsoleApp.Examples.JobDataMapExample
{
    public class JobLogger
    {
        private readonly string _prefix;

        public JobLogger(string prefix)
        {
            _prefix = prefix;
        }

        public void Log(object message = null)
        {
            Console.WriteLine($"({DateTime.Now.ToLongTimeString()}) [{_prefix}] {message}");
        }
    }
}
