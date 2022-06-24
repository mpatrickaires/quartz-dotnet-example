using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzConsoleAdoJobStore
{
    public class ExampleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"[{DateTime.Now}]  Job executed!");

            Thread.Sleep(TimeSpan.FromSeconds(3));

            Console.WriteLine($"[{DateTime.Now}]  Job finished!");

            return Task.CompletedTask;
        }
    }
}
