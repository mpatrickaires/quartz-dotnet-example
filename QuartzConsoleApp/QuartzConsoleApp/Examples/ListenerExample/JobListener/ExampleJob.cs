using Quartz;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.ListenerExample.JobListener
{
    public class ExampleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            CustomLogger.LogAndBreak("Job executed!");

            // Even if the PersistJobDataAfterExecution attribute is not present for the job type,
            // when the job instance add data for the JobDataMap it will be visible for the listener.
            // So, the 'jobData' data can be accessed by the JobWasExecuted method of the job
            // listener and it will have the value set here.
            context.MergedJobDataMap.Put("jobData", "A job data for the listener!");

            return Task.CompletedTask;
        }
    }
}
