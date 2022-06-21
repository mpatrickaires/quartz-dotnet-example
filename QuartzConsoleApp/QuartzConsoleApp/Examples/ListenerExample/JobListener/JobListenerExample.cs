using Quartz;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.ListenerExample.JobListener
{
    public class JobListenerExample : IJobListener
    {
        public string Name => "JobListenerExample";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            new CustomLogger("ListenerExample.JobListener").Log();

            CustomLogger.LogAndBreak("Job execution vetoed!");

            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            new CustomLogger("ListenerExample.JobListener").Log();

            CustomLogger.LogAndBreak("Job to be executed!");

            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            CustomLogger.LogAndBreak("Job was executed!");

            return Task.CompletedTask;
        }
    }
}
