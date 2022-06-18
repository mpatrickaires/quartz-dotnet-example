using Quartz;
using QuartzConsoleApp.Examples.JobDataMapExample;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.JobExample.JobConcurrency
{
    // The DisallowConcurrentExecution prevents job instances from a specific job
    // definition (JobDetail) from being executed concurrently.
    // This, however, still allows the concurrent execution of differents job definition. So, in our
    // example, jobJohn and jobAndrew will run together, but multiple instances of each
    // cannot be run concurrently.
    // This attribute is recommended to be used together with the PersistJobDataAfterExecution to
    // prevent different threads from modifying the same data, which could generate race conditions.
    //
    // If the following line is commented, there will be multiple jobJohn and jobAndrew running
    // concurrently.
    [DisallowConcurrentExecution]
    public class ExampleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var logger = new JobLogger("JobExample.JobConcurrency");
            logger.Log();
            try
            {
                var jobName = context.JobDetail.Key.Name;
                Console.WriteLine($"Job {jobName} started!");

                Console.WriteLine();

                // Notice that the delay here (10 seconds) is greater than the interval from the
                // trigger (2 seconds). So, if the DisallowConcurrentExecution attribute is
                // defined, the other job instances that are fired by the trigger will need to
                // wait for the execution of the current job instance to finish.
                await Task.Delay(TimeSpan.FromSeconds(10));

                logger.Log();
                Console.WriteLine($"Job {jobName} finished!");

                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Oops! An error ocurred during the execution of the job - " +
                    $"{e.GetType()}: {e.Message}");
                Console.WriteLine();
            }
        }
    }
}
