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

        public void Log(object param = null)
        {
            var message = param?.ToString();
            Console.WriteLine($"({DateTime.Now.ToLongTimeString()}) [{_prefix}] {message}");
        }
    }
}
