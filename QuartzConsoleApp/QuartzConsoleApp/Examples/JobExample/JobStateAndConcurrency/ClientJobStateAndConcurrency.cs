using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.JobExample.JobStateAndConcurrency
{
    public static class ClientJobStateAndConcurrency
    {
        public async static Task Run()
        {
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<ExampleJob>()
                .WithIdentity("myJob", "myGroup")
                .UsingJobData("name", "Andrew")
                .UsingJobData("age", 26)
                .UsingJobData("changedData", Guid.NewGuid())
                .UsingJobData("injectedData", Guid.NewGuid())
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "myGroup")
                .UsingJobData("triggerData", Guid.NewGuid())
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            await Task.Delay(TimeSpan.FromSeconds(30));
        }
    }
}
