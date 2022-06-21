using Quartz;
using Quartz.Listener;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.ListenerExample.JobListener
{
    public class VetoJob : TriggerListenerSupport
    {
        public override string Name => "VetoJob";

        public override Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }
    }
}
