using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.ListenerExample.JobListener
{
    public static class ClientJobListener
    {
        public static async Task Run()
        {
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<ExampleJob>()
                .WithIdentity("myJob", "myGroup")
                .StoreDurably()
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "myGroup")
                .ForJob(job)
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromSeconds(5))
                    .RepeatForever())
                .Build();

            scheduler.ListenerManager.AddJobListener(new JobListenerExample(),
                KeyMatcher<JobKey>.KeyEquals(new JobKey("myJob", "myGroup")));

            // Uncomment the following AddTriggerListener to veto the job execution and to the
            // JobExecutionVetoed at the JobListener be executed.
            //scheduler.ListenerManager.AddTriggerListener(new VetoJob(),
            //    KeyMatcher<TriggerKey>.KeyEquals(new TriggerKey("myTrigger", "myGroup")));

            await scheduler.AddJob(job, false);
            await scheduler.ScheduleJob(trigger);

            await Task.Delay(TimeSpan.FromSeconds(60));
        }
    }
}
