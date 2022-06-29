using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using QuartzAspNetCoreApp.Extensions;
using QuartzAspNetCoreApp.Jobs;
using System;

namespace QuartzAspNetCoreApp
{
    public class Startup
    {
        const string groupName = "myGroup";
        const string defaultCronExpression = "0/5 * * * * ?";

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

                q.UsePersistentStore(storeOptions =>
                {
                    storeOptions.UsePostgres(postgresOptions =>
                    {
                        postgresOptions.ConnectionString = "Host=localhost;Port=5432;Pooling=true;Database=QuartzAspNetCoreApp;Username=postgres;Password=123456;";
                    });

                    storeOptions.UseJsonSerializer();
                    storeOptions.UseClustering();
                });

                string dailyCronExpression = $"0 0 {DateTime.Now.Hour} * * ?";


                //q.AddJobAndCronTrigger<GreetingsJob>(defaultCronExpression, false, groupName);
                //q.AddJobAndCronTrigger<DependencyInjectionJob>(defaultCronExpression, false,
                //    groupName);
                //q.AddJobAndCronTrigger<SlowJob>(defaultCronExpression, false, groupName);
                //q.AddJobAndCronTrigger<RecoverableJob>(dailyCronExpression, false, groupName);
                q.AddJobAndSimpleTrigger<RecoverableJob>(0, TimeSpan.MaxValue, true, groupName);
            });

            services.AddQuartzHostedService(options =>
            {
                // If this option is set to true, then when the app is shutdown it will await all the
                // current jobs in execution to finish.
                //options.WaitForJobsToComplete = true;
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
