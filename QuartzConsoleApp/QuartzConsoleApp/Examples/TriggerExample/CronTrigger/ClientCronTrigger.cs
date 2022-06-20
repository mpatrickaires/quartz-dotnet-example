using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.TriggerExample.CronTrigger
{
    public static class ClientCronTrigger
    {
        public static async Task Run()
        {
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<ExampleJob>()
                .WithIdentity("myJob", "myGroup")
                .Build();

            // Testing the InTimeZone attribute.
            // https://howtomanagedevices.com/windows-10/1774/list-of-windows-10-time-zone-codes-tzutil/
            // BE CAREFUL! This timezone list may change, as Windows could update it in the future.
            //
            // UTC:
            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("myTriggerUtc", "myJob")
            //    .WithCronSchedule("* * 23 * * ?", x => x
            //        .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("UTC")))
            //    .Build();
            //
            // Brazil (UTC-03:00):
            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("myTriggerBrazil", "myJob")
            //    .WithCronSchedule("* * 20 * * ?", x => x
            //        .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")))
            //    .Build();
            //
            // It will, however, be able to successfully execute with the system's defined timezone.
            // So, the following will run at the defined hour even without specifying the timezone.
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTriggerWithouTimezone", "myJob")
                .WithCronSchedule("* * 20 * * ?")
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            await Task.Delay(-1);
        }
    }
}
