using Quartz;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.ConfigurationExample.ConfigurationInLine
{
    public static class ClientConfigurationInLine
    {
        public static async Task Run()
        {
            // You can set the scheduler properties through a NameValueCollection
            NameValueCollection properties = new NameValueCollection();
            properties.Add("quartz.scheduler.instanceName", "JustARandomNameScheduler");
            properties.Add("quartz.scheduler.instanceId", "CONFIGURATION_IN_LINE_ID");

            IScheduler scheduler = await SchedulerBuilder.Create(properties)
            // You can set the scheduler properties through the SchedulerBuilder (this one will
            // override the NameValueCollection.
                .WithName("ConfigInLineScheduler")
                .BuildScheduler();

            Console.WriteLine($"Scheduler Name: {scheduler.SchedulerName}");
            Console.WriteLine($"Scheduler Id: {scheduler.SchedulerInstanceId}");
        }
    }
}
