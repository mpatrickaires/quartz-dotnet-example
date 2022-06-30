using System;
using System.Diagnostics;

namespace QuartzAspNetCoreApp.Jobs.Logger
{
    public class JobLogger<T>
    {
        private static string GetCallingClassName(int skipFrames = 2)
        {
            var callingMethod = new StackFrame(skipFrames).GetMethod();

            var callingClass = callingMethod.DeclaringType;

            while (callingClass?.Name == "<Execute>d__0") callingClass = callingClass?.DeclaringType;

            return callingClass?.Name;
        }

        public void Log(object message = null)
        {
            var time = DateTime.Now.ToLongTimeString();

            var className = typeof(T).Name;

            var log = $"({time}) [{className}] {message}";

            Console.WriteLine(log);
        }
    }
}
