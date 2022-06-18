using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.JobExample.JobState
{
    public static class ClientJobState
    {
        public async static Task Run()
        {
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();

            await scheduler.Start();

            // Data are persisted separated for each job definition (each IJobDetail). So,
            // the changes made from jobAndrew will not affect jobJohn and vice versa.
            IJobDetail jobAndrew = JobBuilder.Create<ExampleJob>()
                .WithIdentity("jobAndrew", "myGroup")
                .UsingJobData("name", "Andrew")
                .UsingJobData("age", 26)
                .UsingJobData("changedData", Guid.NewGuid())
                .UsingJobData("injectedData", Guid.NewGuid())
                .UsingJobData("jobIdentifier", null)
                .Build();

            ITrigger triggerAndrew = TriggerBuilder.Create()
                .WithIdentity("triggerAndrew", "myGroup")
                .UsingJobData("triggerData", Guid.NewGuid())
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            IJobDetail jobJohn = JobBuilder.Create<ExampleJob>()
                .WithIdentity("jobJohn", "myGroup")
                .UsingJobData("name", "John")
                .UsingJobData("age", 31)
                .UsingJobData("changedData", Guid.NewGuid())
                .UsingJobData("injectedData", Guid.NewGuid())
                .UsingJobData("jobIdentifier", null)
                .Build();

            ITrigger triggerJohn = TriggerBuilder.Create()
                .WithIdentity("triggerJohn", "myGroup")
                .UsingJobData("triggerData", Guid.NewGuid())
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobAndrew, triggerAndrew);
            await Task.Delay(TimeSpan.FromSeconds(1));
            await scheduler.ScheduleJob(jobJohn, triggerJohn);

            await Task.Delay(TimeSpan.FromSeconds(30));
        }
    }
}
