using Microsoft.Extensions.Logging;
using Quartz;
using QuartzAspNetCoreApp.Jobs.Logger;
using System.Threading.Tasks;

namespace QuartzAspNetCoreApp.Jobs
{
    public class DependencyInjectionJob : IJob
    {
        private ILogger<DependencyInjectionJob> _injectedObject;
        private CustomLogger<DependencyInjectionJob> _logger = new CustomLogger<DependencyInjectionJob>();

        public DependencyInjectionJob(ILogger<DependencyInjectionJob> injectedObject)
        {
            _injectedObject = injectedObject;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var message = _injectedObject != null
                ? "Executed and injected!"
                : "Executed without being injected!";

            _logger.Log($"{message}");

            return Task.CompletedTask;
        }
    }
}
