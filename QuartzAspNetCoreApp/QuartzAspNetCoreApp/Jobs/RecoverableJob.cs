using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzAspNetCoreApp.Jobs
{
    [DisallowConcurrentExecution]
    public class RecoverableJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Recoverable job started!");

            for (int counter = 0; counter < 10; counter++)
            {
                Console.WriteLine($"Recoverable job counter: {counter}");
                await Task.Delay(TimeSpan.FromSeconds(5));
            }

            Console.WriteLine("Recoverable job finished!");
        }
    }
}
