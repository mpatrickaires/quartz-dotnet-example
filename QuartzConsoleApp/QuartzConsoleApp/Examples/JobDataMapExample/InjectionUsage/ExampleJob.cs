using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.JobDataMapExample.InjectionUsage
{
    public class ExampleJob : IJob
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int UnknownData { get; set; }
        public int WrongType { get; set; }
        public string SharedData { get; set; }

        public Task Execute(IJobExecutionContext context)
        {
            var logger = new CustomLogger("JobDataMapExample.InjectionUsage");
            logger.Log();
            try
            {
                Console.WriteLine($"Name: {Name} - Age: {Age}");

                // A property with a name that doesn't represent any key from the JobDataMap
                // will simply assume its type's default value
                Console.WriteLine($"Unknown Data: {UnknownData}");

                // The following can happen with a property that has the same name as a key from the 
                // JobDataMap but has a different type:
                //
                // - If conversion is possible, it will be converted.
                //
                // - If conversion is not possible, it will assume the type's default value.
                //
                // So, using JobDataMap injection has a better safety against wrong type conversion.
                Console.WriteLine($"Wrong Type: {WrongType}");

                // A property that represents a key that is defined at both IJobDetail and ITrigger
                // will assume the value that is defined at the ITrigger, which means that ITrigger
                // also has a higher priority here.
                Console.WriteLine($"Shared Data: {SharedData}");

                Console.WriteLine();

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Oops! An error ocurred during the execution of the job - " +
                    $"{e.GetType()}: {e.Message}");
                Console.WriteLine();
                return Task.CompletedTask;
            }
        }
    }
}
