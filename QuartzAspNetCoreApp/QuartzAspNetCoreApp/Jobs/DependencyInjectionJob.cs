using Microsoft.Extensions.Logging;
using Quartz;
using System;
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
            var message = _logger != null
                ? "DependencyInjectionJob executed and injected!"
                : "DependencyInjectionJob executed without being injected!";

            Console.WriteLine($"{message}\n");

            return Task.CompletedTask;
        }
    }
}
