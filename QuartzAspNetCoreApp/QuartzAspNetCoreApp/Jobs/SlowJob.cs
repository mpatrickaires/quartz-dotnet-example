using Quartz;
using QuartzAspNetCoreApp.Jobs.Logger;
using System;
using System.Threading.Tasks;

namespace QuartzAspNetCoreApp.Jobs
{
    [DisallowConcurrentExecution]
    public class SlowJob : IJob
    {
        private JobLogger<SlowJob> _logger = new JobLogger<SlowJob>();

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.Log("Started!");

            await Task.Delay(TimeSpan.FromSeconds(5));

            _logger.Log("Finished!");
        }
    }
}
