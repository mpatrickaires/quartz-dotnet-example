using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace QuartzAspNetCoreApp.Jobs
{
    public class DependencyInjectionJob : IJob
    {
        private ILogger<DependencyInjectionJob> _logger;

        public DependencyInjectionJob(ILogger<DependencyInjectionJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            if (_logger != null) _logger.LogInformation("DependencyInjectionJob executed!\n");
            else System.Console.WriteLine("DependencyInjectionJob executed without logger!\n");

            return Task.CompletedTask;
        }
    }
}
