using Quartz;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.ListenerExample.JobListener
{
    public class ExampleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            CustomLogger.LogAndBreak("Job executed!");

            return Task.CompletedTask;
        }
    }
}
