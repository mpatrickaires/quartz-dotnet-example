using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.ConfigurationExample.ConfigurationInFile
{
    public static class ClientConfigurationInFile
    {
        public static async Task Run()
        {
            // You can set the scheduler properties through a configuration file named
            // 'quartz.config' which must be located at the root of the project.
            // The StdSchedulerFactory will automatically seek for this file and set its properties
            // values to the IScheduler instance.
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();
            System.Console.WriteLine($"Scheduler Name: {scheduler.SchedulerName}");
            System.Console.WriteLine($"Scheduler Id: {scheduler.SchedulerInstanceId}");
            // IMPORTANT:
            // For this to work in Visual Studio, you must access the 'quartz.config' file
            // Properties and set the 'Copy To Output Directory' option to 'Copy always'.
            // Otherwise the StdSchedulerFactory will not be able to see this file and will set the
            // default properties.
        }
    }
}
