﻿
using Discount.Api.Extensions;

namespace Discount.Api
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

