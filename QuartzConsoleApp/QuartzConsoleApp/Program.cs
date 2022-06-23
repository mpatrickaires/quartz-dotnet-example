using QuartzConsoleApp.Examples.ConfigurationExample.ConfigurationInFile;
using QuartzConsoleApp.Examples.ConfigurationExample.ConfigurationInLine;
using QuartzConsoleApp.Examples.JobDataMapExample.InjectionUsage;
using QuartzConsoleApp.Examples.JobDataMapExample.SimpleUsage;
using QuartzConsoleApp.Examples.JobExample.JobConcurrency;
using QuartzConsoleApp.Examples.JobExample.JobState;
using QuartzConsoleApp.Examples.ListenerExample.JobListener;
using QuartzConsoleApp.Examples.ListenerExample.TriggerListener;
using QuartzConsoleApp.Examples.TriggerExample.CronTrigger;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp
{
    internal class Program
    {
        public static object ClientSimpleUsage { get; private set; }

        static async Task Main(string[] args)
        {
            // Uncomment the following line to see metadata about the Quartz's execution.
            //Quartz.Logging.LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            // Doing so to prevent Visual Studio automatic code cleanup from removing the using
            // of commented code.
            Func<Task> clientJobDataMapSimpleUsage = ClientJobDataMapSimpleUsage.Run;
            Func<Task> clientJobDataMapInjectionUsage = ClientJobDataMapInjectionUsage.Run;
            Func<Task> clientJobState = ClientJobState.Run;
            Func<Task> clientJobConcurrency = ClientJobConcurrency.Run;
            Func<Task> clientCronTrigger = ClientCronTrigger.Run;
            Func<Task> clientTriggerListener = ClientTriggerListener.Run;
            Func<Task> clientJobListener = ClientJobListener.Run;
            Func<Task> clientConfigurationInLine = ClientConfigurationInLine.Run;
            Func<Task> clientConfigurationInFile = ClientConfigurationInFile.Run;

            //await clientJobDataMapSimpleUsage();
            //await clientJobDataMapInjectionUsage();
            //await clientJobState();
            //await clientJobConcurrency();
            //await clientCronTrigger();
            //await clientTriggerListener();
            //await clientJobListener();
            //await clientConfigurationInLine();
            await clientConfigurationInFile();
        }
    }

}
