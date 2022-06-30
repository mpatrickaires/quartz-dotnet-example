using Quartz;
using QuartzAspNetCoreApp.Jobs.Logger;
using System;
using System.Threading.Tasks;

namespace QuartzAspNetCoreApp.Jobs
{
    [DisallowConcurrentExecution]
    public class RecoverableJob : IJob
    {
        private JobLogger<RecoverableJob> _logger = new JobLogger<RecoverableJob>();

        public async Task Execute(IJobExecutionContext context)
        {
            if (context.Recovering) _logger.Log("Recovered! I'm back.");
            else _logger.Log("Started!");

            for (int counter = 0; counter < 10; counter++)
            {
                _logger.Log($"Counter: {counter}");
                await Task.Delay(TimeSpan.FromSeconds(5));
            }

            _logger.Log("Finished!");
        }
    }
}
