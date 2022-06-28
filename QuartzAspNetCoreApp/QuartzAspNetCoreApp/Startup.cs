using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using QuartzAspNetCoreApp.Extensions;
using QuartzAspNetCoreApp.Jobs;

namespace QuartzAspNetCoreApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<QuartzOptions>(Configuration.GetSection("Quartz"));

            services.AddQuartz(q =>
            {
                const string groupName = "myGroup";
                const string defaultCronExpression = "0/5 * * * * ?";

                // This configuration is necessary if we want to inject services at our job instances
                // when they are created by the job factory.  
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.AddJobAndTrigger<GreetingsJob>(defaultCronExpression, groupName);
                q.AddJobAndTrigger<DependencyInjectionJob>(defaultCronExpression, groupName);
                q.AddJobAndTrigger<SlowJob>(defaultCronExpression, groupName);
            });

            services.AddQuartzHostedService(options =>
            {
                // If this option is set to true, then when the app is shutdown it will await all the
                // current jobs in execution to finish.
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
