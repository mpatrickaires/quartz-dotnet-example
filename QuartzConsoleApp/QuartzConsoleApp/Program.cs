using QuartzConsoleApp.Examples.JobDataMapExample.InjectionUsage;
using QuartzConsoleApp.Examples.JobDataMapExample.SimpleUsage;
using QuartzConsoleApp.Examples.JobExample.JobState;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp
{
    internal class Program
    {
        public static object ClientSimpleUsage { get; private set; }

        static async Task Main(string[] args)
        {
            // Uncomment the following line to see metadata about the Quartz execution.
            //Quartz.Logging.LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            // Doing so to prevent Visual Studio automatic code cleanup from removing the using
            // of commented code.
            Func<Task> clientJobDataMapSimpleUsage = ClientJobDataMapSimpleUsage.Run;
            Func<Task> clientJobDataMapInjectionUsage = ClientJobDataMapInjectionUsage.Run;
            Func<Task> clientJobState = ClientJobState.Run;

            //await clientJobDataMapSimpleUsage();
            //await clientJobDataMapInjectionUsage();
            await clientJobState();
        }
    }

}
