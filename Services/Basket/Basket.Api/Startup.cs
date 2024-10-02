

using Basket.Api.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Api
{
   
        public class Startup
        {
            public IConfiguration Configuration;
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllers();

               // services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
               //services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

                services.AddDependencies(Configuration);
            }
            public void Configure(IApplicationBuilder app,
                IWebHostEnvironment env,
                IConfiguration configuration,
                IServiceProvider serviceProvider)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();

                }
                app.UseDependencies(configuration, serviceProvider);
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }

