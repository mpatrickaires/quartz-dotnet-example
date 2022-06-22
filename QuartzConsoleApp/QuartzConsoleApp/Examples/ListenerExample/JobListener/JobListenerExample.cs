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
            var jobData = context.MergedJobDataMap.GetString("jobData");

            CustomLogger.LogAndBreak($"Job was executed! - jobData: {jobData}");

            return Task.CompletedTask;
        }
    }
}
