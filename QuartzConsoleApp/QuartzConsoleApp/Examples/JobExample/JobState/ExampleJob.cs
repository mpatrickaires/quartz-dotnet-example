using Quartz;
using QuartzConsoleApp.Examples.JobDataMapExample;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.JobExample.JobState
{
    // Since the class now have the PersistJobDataAfterExecution attribute, data modified or added
    // by the job instance will be kept in the JobDataMap, which means that it will be persisted
    // between executions.
    [PersistJobDataAfterExecution]
    public class ExampleJob : IJob
    {
        public Guid InjectedData { get; set; }

        public Task Execute(IJobExecutionContext context)
        {
            var logger = new JobLogger("JobExample.JobState");
            logger.Log();
            try
            {
                // If the changes are made at the JobDataMap obtained from the MergedJobDataMap,
                // the changes will not be persisted.
                //JobDataMap dataMap = context.MergedJobDataMap;
                JobDataMap dataMap = context.JobDetail.JobDataMap;

                JobDataMap dataMapTrigger = context.Trigger.JobDataMap;

                var name = dataMap.GetString("name");
                var age = dataMap.GetInt("age");
                var changedData = dataMap.GetGuid("changedData");
                var triggerData = dataMapTrigger.GetGuid("triggerData");
                var jobIdentifier = (string)dataMap.Get("jobIdentifier") ?? "No identifier yet.";

                Console.WriteLine($"Name: {name} - Age: {age}");
                Console.WriteLine($"Changed Data: {changedData}");
                Console.WriteLine($"Injected Data: {InjectedData}");
                Console.WriteLine($"Trigger Data: {triggerData}");
                Console.WriteLine($"Job Identifier: {jobIdentifier}");

                dataMap.Put("changedData", Guid.NewGuid());

                var newData = (string)dataMap.Get("newData") ?? "There is no new data :(";
                Console.WriteLine($"New Data: {newData}");

                dataMap.Put("newData", "It's me, the new data :)");

                // Doing changes directly to the property that was injected with the data will not
                // have affect at the persistance; it is necessary to do the changes directly at the
                // JobDataMap.
                InjectedData = Guid.NewGuid();

                // Changes directly at the JobDataMap obtained from the ITrigger will also not be
                // persisted.
                // In conclusion, to save the additions and modifications, the job instance can only
                // make changes to the JobDataMap obtained from the IJobDetail
                dataMapTrigger.Put("triggerData", Guid.NewGuid());

                // Data are persisted separated for each job definition (each IJobDetail). So,
                // the changes made from jobAndrew will not affect jobJohn and vice versa.
                var jobName = context.JobDetail.Key.Name;
                dataMap.Put("jobIdentifier", $"Hey, I'm {jobName}!");

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
