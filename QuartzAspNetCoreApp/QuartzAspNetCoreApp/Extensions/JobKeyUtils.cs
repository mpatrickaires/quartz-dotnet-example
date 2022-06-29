using Quartz;

namespace QuartzAspNetCoreApp.Extensions
{
    public class JobKeyUtils
    {
        public static JobKey GetJobKeyFromType<TJob>(string groupName) where TJob : IJob
        {
            var jobName = typeof(TJob).Name;

            return new JobKey(jobName, groupName);
        }
    }
}
