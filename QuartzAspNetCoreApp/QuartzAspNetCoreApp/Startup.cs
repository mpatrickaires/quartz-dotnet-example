using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Plugin.Interrupt;
using QuartzAspNetCoreApp.Extensions;
using QuartzAspNetCoreApp.Jobs;
using System;
using System.Globalization;

namespace QuartzAspNetCoreApp
{
    public class Startup
    {
        const string groupName = "MyGroup";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<QuartzOptions>(options =>
            {
                // Default value
                options.Scheduling.OverWriteExistingData = true;

                // Default value; only have effect if OverWriteExistingData is false, which may
                // cause an exception to be thrown.
                options.Scheduling.IgnoreDuplicates = false;
            });

            services.AddQuartz(q =>
            {
                q.SchedulerName = "QuartzAspNetCoreApp";
                q.SchedulerId = "AUTO";

                // This configuration is necessary if we want to inject services at our job instances
                // when they are created by the job factory.  
                // In the current example, if this configuration is not set and the
                // DependencyInjectionJob job is created, an error will occur because the job
                // factory will not be able to work around the job's constructor that asks for the
                // dependency injection.
                q.UseMicrosoftDependencyInjectionJobFactory();

                // When WaitForJobsToComplete is true, this will not work.
                // InterruptJobsOnShutdownWithWait must be used instead.
                //q.InterruptJobsOnShutdown = true;

                q.InterruptJobsOnShutdownWithWait = true;

                // This option is only present if the Quartz.Plugins package is installed.
                q.UseJobAutoInterrupt(options =>
                {
                    // The cancellation of the CancellationToken of job instances will automatically
                    // be requested if they execution goes past the defined at DefaultMaxRunTime.
                    // 5 minutes is the default value.
                    options.DefaultMaxRunTime = TimeSpan.FromMinutes(5);
                });

                q.UsePersistentStore(storeOptions =>
                {
                    storeOptions.UsePostgres(postgresOptions =>
                    {
                        postgresOptions.ConnectionString = "Host=localhost;Port=5432;Pooling=true;Database=QuartzAspNetCoreApp;Username=postgres;Password=123456;";
                    });

                    storeOptions.UseJsonSerializer();

                    // When Quartz is configured for clustering, any job interrupted while
                    // executing will be recovered by another node that may be running concurrently
                    // with the one that was executing the job.
                    storeOptions.UseClustering();
                });

                var greetingsJobKey = new JobKey(typeof(GreetingsJob).Name, groupName);
                q.ScheduleJob<GreetingsJob>(
                    t =>
                    {
                        t.WithIdentity($"{greetingsJobKey.Name}Trigger", groupName);
                        t.StartNow();
                        t.WithCronSchedule("0/5 * * * * ?");
                    },
                    j =>
                    {
                        j.WithIdentity(greetingsJobKey);
                    });

                // AddJobAndCronTrigger is an extension method made in this project that does the
                // same that was done to schedule the GreetingsJob. This was done just to have less
                // code verbosity.
                q.AddJobAndCronTrigger<DependencyInjectionJob>("0/8 * * * * ?", groupName);
                q.AddJobAndCronTrigger<SlowJob>("0/11 * * * * ?", groupName);

                var recoverableJobKey = new JobKey(typeof(RecoverableJob).Name, groupName);
                q.ScheduleJob<RecoverableJob>(
                    t =>
                    {
                        t.WithIdentity($"{recoverableJobKey.Name}Trigger", groupName);
                        t.StartNow();
                    },
                    j =>
                    {
                        j.WithIdentity(recoverableJobKey);
                        j.RequestRecovery();
                    });

                var interruptibleJobKey = new JobKey(typeof(InterruptibleJob).Name, groupName);
                q.ScheduleJob<InterruptibleJob>(
                    t =>
                    {
                        t.WithIdentity($"{interruptibleJobKey.Name}Trigger", groupName);
                        t.StartNow();
                    },
                    j =>
                    {
                        j.WithIdentity(interruptibleJobKey);

                        // For the job to be interruped by the UseJobAutoInterrupt after the given
                        // time configured, this key-value pair must be added to the UsingJobData.
                        j.UsingJobData(JobInterruptMonitorPlugin.JobDataMapKeyAutoInterruptable, true);

                        // Overrides the max time set at the UseJobAutoInterrupt options.
                        j.UsingJobData(JobInterruptMonitorPlugin.JobDataMapKeyMaxRunTime, TimeSpan.FromSeconds(15).TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
                    });
            });

            services.AddQuartzHostedService(options =>
            {
                // If this option is set to true, when the app is shutdown, it will await all the
                // jobs yet in execution to finish.
                options.WaitForJobsToComplete = true;
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
