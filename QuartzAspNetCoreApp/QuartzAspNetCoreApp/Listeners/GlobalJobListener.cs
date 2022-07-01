using Quartz;
using Quartz.Listener;
using QuartzAspNetCoreApp.Jobs.Logger;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzAspNetCoreApp.Listeners
{
    public class GlobalJobListener : JobListenerSupport
    {
        private CustomLogger<GlobalJobListener> _logger = new CustomLogger<GlobalJobListener>();
        public override string Name => nameof(GlobalJobListener);

        public override Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            var jobName = context.JobDetail.JobType.Name;

            _logger.Log($"{jobName} to be executed!");

            return Task.CompletedTask;
        }

        public override Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            var jobName = context.JobDetail.JobType.Name;

            if (context.CancellationToken.IsCancellationRequested)
                _logger.Log($"{jobName} was interrupted! Elapsed time: {context.JobRunTime}");
            else
                _logger.Log($"{jobName} was executed! Elapsed time: {context.JobRunTime}");

            return Task.CompletedTask;
        }
    }
}
