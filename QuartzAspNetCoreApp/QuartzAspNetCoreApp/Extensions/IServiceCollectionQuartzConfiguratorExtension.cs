using Quartz;
using System;

namespace QuartzAspNetCoreApp.Extensions
{
    public static class IServiceCollectionQuartzConfiguratorExtension
    {
        public static IServiceCollectionQuartzConfigurator AddJobAndCronTrigger<TJob>(
        this IServiceCollectionQuartzConfigurator quartz, string cronExpression, string groupName)
            where TJob : IJob
        {
            if (!CronExpression.IsValidExpression(cronExpression))
                throw new ArgumentException("Invalid cron expression.");

            var jobName = typeof(TJob).Name;

            var jobKey = new JobKey(jobName, groupName);

            quartz.ScheduleJob<TJob>(
                trigger =>
                {
                    trigger.WithIdentity($"{jobKey.Name}Trigger", groupName);
                    trigger.StartNow();
                    trigger.WithCronSchedule(cronExpression);
                },
                job =>
                {
                    job.WithIdentity(jobKey);
                }
            );

            return quartz;
        }
    }
}
