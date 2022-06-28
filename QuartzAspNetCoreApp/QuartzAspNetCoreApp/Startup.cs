using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<QuartzOptions>(Configuration.GetSection("Quartz"));

            services.AddQuartz(q =>
            {
                const string groupName = "myGroup";
                const string defaultCronExpression = "0/5 * * * * ?";

                q.UseMicrosoftDependencyInjectionJobFactory();

                var greetingsJobKey = new JobKey("greetingsJob", groupName);

                q.AddJob<GreetingsJob>(j =>
                {
                    j.WithIdentity(greetingsJobKey);
                });

                q.AddTrigger(t =>
                {
                    t.ForJob(greetingsJobKey);
                    t.StartNow();
                    t.WithCronSchedule(defaultCronExpression);
                });

                var dependencyInjectionJobKey = new JobKey("dependencyInjectionJob", groupName);

                q.AddJob<DependencyInjectionJob>(j =>
                {
                    j.WithIdentity(dependencyInjectionJobKey);
                });

                q.AddTrigger(t =>
                {
                    t.ForJob(dependencyInjectionJobKey);
                    t.StartNow();
                    t.WithCronSchedule(defaultCronExpression);
                });
            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
