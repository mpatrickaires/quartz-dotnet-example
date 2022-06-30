using Quartz;
using QuartzAspNetCoreApp.Jobs.Logger;
using System.Threading.Tasks;

namespace QuartzAspNetCoreApp.Jobs
{
    public class GreetingsJob : IJob
    {
        private JobLogger<GreetingsJob> _logger = new JobLogger<GreetingsJob>();

        public Task Execute(IJobExecutionContext context)
        {
            _logger.Log("Hello!");
            return Task.CompletedTask;
        }
    }
}
