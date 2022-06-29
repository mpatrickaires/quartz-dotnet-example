using Quartz;
using System;

namespace QuartzAspNetCoreApp.Extensions
{
    public static class IServiceCollectionQuartzConfiguratorExtension
    {
        const string defaultGroup = "defaultGroup";

        public static IServiceCollectionQuartzConfigurator AddJobAndCronTrigger<TJob>(
        this IServiceCollectionQuartzConfigurator quartz, string cronExpression,
        bool isJobRecoverable = false, string groupName = defaultGroup) where TJob : IJob
        {
            if (!CronExpression.IsValidExpression(cronExpression))
                throw new ArgumentException("Invalid cron expression.");

            var jobKey = JobKeyUtils.GetJobKeyFromType<TJob>(groupName);

            quartz.AddJob<TJob>(j =>
            {
                j.WithIdentity(jobKey);
                j.RequestRecovery(isJobRecoverable);
            });

            quartz.AddTrigger(t =>
            {
                t.WithIdentity($"{jobKey.Name}Trigger", groupName);
                t.ForJob(jobKey);
                t.StartNow();
                t.WithCronSchedule(cronExpression);
            });

            return quartz;
        }

        public static IServiceCollectionQuartzConfigurator AddJobAndSimpleTrigger<TJob>(
            this IServiceCollectionQuartzConfigurator quartz, int repeatCount, TimeSpan interval,
            bool isJobRecoverable = false, string groupName = defaultGroup) where TJob : IJob
        {
            if (repeatCount < -1)
                throw new ArgumentException($"{nameof(repeatCount)} must be zero, a positive integer or -1 (repeats indefinitely");

            var jobKey = JobKeyUtils.GetJobKeyFromType<TJob>(groupName);

            quartz.AddJob<TJob>(j =>
            {
                j.WithIdentity(jobKey);
                j.RequestRecovery(isJobRecoverable);
                j.StoreDurably();
            });

            quartz.AddTrigger(t =>
            {
                t.WithIdentity($"{jobKey.Name}Trigger", groupName);
                t.ForJob(jobKey);
                t.StartNow();
                t.WithSimpleSchedule(s =>
                {
                    s.WithInterval(interval);
                    s.WithRepeatCount(repeatCount);
                });
            });

            return quartz;
        }
    }
}
