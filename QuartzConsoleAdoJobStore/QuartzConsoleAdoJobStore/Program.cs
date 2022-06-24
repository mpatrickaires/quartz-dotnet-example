using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace QuartzConsoleAdoJobStore
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // IMPORTANT:
            // Access the following page to see all the Quartz tables creation scripts respective
            // for each database:
            // https://github.com/quartznet/quartznet/tree/main/database/tables
            //
            // To Quartz access the database, you must install it respective drive through the Nuget.
            // In this example, the Npgsql package was isntalled.

            var scheduler = await new StdSchedulerFactory().GetScheduler();

            System.Console.WriteLine($"Scheduler Name: {scheduler.SchedulerName}");
            System.Console.WriteLine($"Scheduler Id: {scheduler.SchedulerInstanceId}");

            var jobKey = JobKey.Create("myJob", "myGroup");

            var hasJob = (await scheduler.GetJobDetail(jobKey)) != null;

            if (!hasJob) await CreateJobsAndTriggers(scheduler);

            await scheduler.Start();

            await Task.Delay(-1);
        }

        static async Task CreateJobsAndTriggers(IScheduler scheduler)
        {
            var job = JobBuilder.Create<ExampleJob>()
                .WithIdentity("myJob", "myGroup")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "myGroup")
                .ForJob(job)
                .StartNow()
                .WithCronSchedule("0/5 * * * * ?")
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
