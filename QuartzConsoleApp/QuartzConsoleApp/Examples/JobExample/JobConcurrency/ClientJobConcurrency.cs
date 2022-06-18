using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.JobExample.JobConcurrency
{
    public static class ClientJobConcurrency
    {
        public async static Task Run()
        {
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();

            await scheduler.Start();

            IJobDetail jobAndrew = JobBuilder.Create<ExampleJob>()
                .WithIdentity("jobAndrew", "myGroup")
                .Build();

            ITrigger triggerAndrew = TriggerBuilder.Create()
                .WithIdentity("triggerAndrew", "myGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(2)
                    .RepeatForever())
                .Build();

            IJobDetail jobJohn = JobBuilder.Create<ExampleJob>()
                .WithIdentity("jobJohn", "myGroup")
                .Build();

            ITrigger triggerJohn = TriggerBuilder.Create()
                .WithIdentity("triggerJohn", "myGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(2)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobAndrew, triggerAndrew);
            await Task.Delay(TimeSpan.FromSeconds(1));
            await scheduler.ScheduleJob(jobJohn, triggerJohn);

            await Task.Delay(TimeSpan.FromSeconds(30));
        }
    }
}
