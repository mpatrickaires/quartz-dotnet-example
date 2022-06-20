using Quartz;
using QuartzConsoleApp.Examples.JobDataMapExample;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.TriggerExample.CronTrigger
{
    public class ExampleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var logger = new CustomLogger("TriggerExample.CronTrigger");
            logger.Log();

            Console.WriteLine("Job executed!");
            Console.WriteLine();

            return Task.CompletedTask;
        }
    }
}
