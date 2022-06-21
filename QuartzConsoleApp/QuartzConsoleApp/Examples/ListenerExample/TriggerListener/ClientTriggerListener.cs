using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.ListenerExample.TriggerListener
{
    public class ClientTriggerListener
    {
        public async static Task Run()
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
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromSeconds(5))
                    .RepeatForever())
                .Build();

            scheduler.ListenerManager.AddTriggerListener(new TriggerListenerExample(),
                KeyMatcher<TriggerKey>.KeyEquals(new TriggerKey("myTrigger", "myGroup")));

            await scheduler.AddJob(job, false);
            await scheduler.ScheduleJob(trigger);

            await Task.Delay(TimeSpan.FromSeconds(60));
        }
    }
}
