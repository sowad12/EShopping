
using Catalog.Api.Extensions;
using EShopping.Core.Filters;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;

namespace Catalog.Api
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

                services.AddDependencies(Configuration);
                services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(InputValidationFilter));             //options.Filters.Add(type of(ExceptionFilter));
                });
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
