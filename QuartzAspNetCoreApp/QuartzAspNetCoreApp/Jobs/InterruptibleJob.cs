using Quartz;
using QuartzAspNetCoreApp.Jobs.Logger;
using System;
using System.Threading.Tasks;

namespace QuartzAspNetCoreApp.Jobs
{
    [DisallowConcurrentExecution]
    public class InterruptibleJob : IJob
    {
        private CustomLogger<InterruptibleJob> _logger = new CustomLogger<InterruptibleJob>();

        public async Task Execute(IJobExecutionContext context)
        {
            var startTime = DateTime.Now;

            _logger.Log("Started!");

            for (int count = 0; count < 100; count++)
            {
                var secondsPassed = Math.Round((DateTime.Now - startTime).TotalSeconds);

                _logger.Log($"Seconds passed: {secondsPassed}");

                if (context.CancellationToken.IsCancellationRequested)
                {
                    _logger.Log("Cancellation requested! Job interrupted.");
                    return;
                }

                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }
    }
}
