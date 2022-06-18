using System.Threading.Tasks;

namespace QuartzConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Uncomment the following line to see metadata about the Quartz execution
            //Quartz.Logging.LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            //await Examples.JobDataMapExample.SimpleUsage.Client.Run();
            await Examples.JobDataMapExample.InjectionUsage.Client.Run();
        }
    }

}
