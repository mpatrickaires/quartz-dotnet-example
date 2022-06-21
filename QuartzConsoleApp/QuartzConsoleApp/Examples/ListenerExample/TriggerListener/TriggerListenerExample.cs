using Quartz;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.ListenerExample.TriggerListener
{
    public class TriggerListenerExample : ITriggerListener
    {
        public string Name => "TriggerListenerExample";

        public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            CustomLogger.LogAndBreak("Trigger completed!");

            return Task.CompletedTask;
        }

        public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            new CustomLogger("ListenerExample.TriggerListener").Log();

            CustomLogger.LogAndBreak("Trigger fired!");

            return Task.CompletedTask;
        }

        public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            CustomLogger.LogAndBreak("Trigger misfired!");

            return Task.CompletedTask;
        }

        public async Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            // The return here will dictate if the job will be executed or not:
            // - false: the job will not be vetoed and will execute normally.
            // - true: the job will be vetoed and will not be executed.
            var returnValue = true;

            CustomLogger.LogAndBreak($"Veto job execution! Return: {returnValue}");

            return await Task.FromResult(returnValue);
        }
    }
}
