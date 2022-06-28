using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzAspNetCoreApp.Jobs
{
    [DisallowConcurrentExecution]
    public class SlowJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Action<string> log = (message => Console.WriteLine($"{message}\n"));

            log("SlowJob started!");

            await Task.Delay(TimeSpan.FromSeconds(5));

            log("SlowJob finished!");
        }
    }
}
