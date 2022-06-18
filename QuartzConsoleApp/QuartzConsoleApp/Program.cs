using QuartzConsoleApp.Examples.JobDataMapExample.InjectionUsage;
using System.Threading.Tasks;

namespace QuartzConsoleApp
{
    internal class Program
    {
        public static object ClientSimpleUsage { get; private set; }

        static async Task Main(string[] args)
        {
            // Uncomment the following line to see metadata about the Quartz execution
            //Quartz.Logging.LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            //await ClientJobDataMapSimpleUsage.Run();
            await ClientJobDataMapInjectionUsage.Run();
        }
    }

}
