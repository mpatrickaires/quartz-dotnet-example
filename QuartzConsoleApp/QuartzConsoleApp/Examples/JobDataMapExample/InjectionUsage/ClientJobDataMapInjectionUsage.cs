using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.JobDataMapExample.InjectionUsage
{
    public static class ClientJobDataMapInjectionUsage
    {
        const string SharedData = "sharedData";

        public async static Task Run()
        {
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<ExampleJob>()
                .WithIdentity("myJob", "myGroup")
                .UsingJobData("name", "Andrew")
                .UsingJobData("age", "26")
                .UsingJobData("wrongType", "Text")
                .UsingJobData(SharedData, "I'm from IJobDetail")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "myGroup")
                .UsingJobData(SharedData, "I'm from ITrigger")
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
