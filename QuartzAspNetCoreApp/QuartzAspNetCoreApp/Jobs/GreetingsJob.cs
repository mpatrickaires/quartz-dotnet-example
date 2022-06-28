using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzAspNetCoreApp.Jobs
{
    public class GreetingsJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello! GreetingsJob executed.\n");
            return Task.CompletedTask;
        }
    }
}
