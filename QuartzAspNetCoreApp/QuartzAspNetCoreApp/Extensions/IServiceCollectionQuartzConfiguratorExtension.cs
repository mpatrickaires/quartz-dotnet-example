using Quartz;
using System;

namespace QuartzAspNetCoreApp.Extensions
{
    public static class IServiceCollectionQuartzConfiguratorExtension
    {
        public static IServiceCollectionQuartzConfigurator AddJobAndTrigger<TJob>(this IServiceCollectionQuartzConfigurator quartz, string cronExpression, string groupName = "defaultGroup") where TJob : IJob
        {
            if (!CronExpression.IsValidExpression(cronExpression)) throw new ArgumentException("Invalid cron expression.");

            var jobName = typeof(TJob).Name;

            var jobKey = new JobKey(jobName, groupName);

            quartz.AddJob<TJob>(j => j.WithIdentity(jobKey));

            quartz.AddTrigger(t =>
            {
                t.WithIdentity($"{jobName}Trigger", groupName);
                t.ForJob(jobKey);
                t.StartNow();
                t.WithCronSchedule(cronExpression);
            });

            return quartz;
        }
    }
}
