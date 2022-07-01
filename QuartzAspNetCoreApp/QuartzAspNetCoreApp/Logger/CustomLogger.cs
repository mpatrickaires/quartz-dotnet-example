using System;

namespace QuartzAspNetCoreApp.Jobs.Logger
{
    public class CustomLogger<T>
    {
        public void Log(object message = null)
        {
            var time = DateTime.Now.ToLongTimeString();

            var className = typeof(T).Name;

            var log = $"({time}) [{className}] {message}";

            Console.WriteLine(log);
        }
    }
}
