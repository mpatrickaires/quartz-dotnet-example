using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzConsoleApp.Examples.JobDataMapExample.SimpleUsage
{
    public class ExampleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            const string SharedData = "sharedData";

            var logger = new CustomLogger("JobDataMapExample.SimpleUsage");
            logger.Log();

            try
            {
                JobDataMap dataMap = context.JobDetail.JobDataMap;

                var name = dataMap.GetString("name");
                var age = dataMap.GetInt("age");
                Console.WriteLine($"Name: {name} - Age: {age}");

                // The following can happen when using the get with the wrong type:
                //
                // - If conversion is possible, it will be converted.
                //
                // - If conversion is not possible, an InvalidCastException exception will be
                // throwed.
                //
                // The following line represents a conversion that is not possible, so the 
                // InvalidCastException will be throwed.
                //var wrongType = dataMap.GetInt("name");

                // Using the get with an unknown key will give the default value of the return type
                var unknownData = dataMap.GetInt("unknownData");
                Console.WriteLine($"Unknown Data: {unknownData}");

                // By default, data stored or modified through the Put is volatile, so it will only
                // last for the duration of the current job execution. Once the execution is finished,
                // the data will disappear or will return to its original state.
                //
                // In the next execution, the age will not be the one defined here.
                dataMap.Put("age", 30);

                // There will never be a value in 'newData', as it is not persisted across executions.
                var newData = (string)dataMap.Get("newData") ?? "There is no new data :(";
                Console.WriteLine($"New Data: {newData}");
                dataMap.Put("newData", "It's me, the new data :)");

                // If we try to add a key that already exists, an ArgumentException will be throwed.
                //dataMap.Add("age", 80);

                // A key shared by the IJobDetail and the ITrigger will have a different value based
                // on the following:
                //
                // - If accessed through the JobDetail.JobDataMap, it will have the value that
                // was defined at the IJobDetail.
                //
                // - If accessed through the Trigger.JobDataMap or MergedJobDataMap, it will have
                // the value that was defined at the ITrigger (the ITrigger has priority over the
                // IJobDetail in data sharing).
                var sharedDataJobDetail = context.JobDetail.JobDataMap.GetString(SharedData);
                var sharedDataTrigger = context.Trigger.JobDataMap.GetString(SharedData);
                var sharedDataMerged = context.MergedJobDataMap.GetString(SharedData);
                Console.WriteLine("Shared Data:");
                Console.WriteLine($"\t- [JobDetail.JobDataMap]: {sharedDataJobDetail}");
                Console.WriteLine($"\t- [Trigger.JobDataMap]: {sharedDataTrigger}");
                Console.WriteLine($"\t- [MergedJobDataMap]: {sharedDataMerged}");

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
