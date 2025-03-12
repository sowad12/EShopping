using EShopping.Core.Middleware;
using EShopping.Utilities;
using Newtonsoft.Json;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway.Global
{
    public class Startup
    {
        public IConfiguration configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = EnvironmentHelper.GetAppSettings(env).Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthenticationServices(configuration);

            services.AddAuthorization();


            string[] origins = configuration.GetSection("CorsOptions").GetSection("Origins")
                .AsEnumerable().ToArray().Where(c => c.Value != null).Select(c => c.Value).ToArray();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod().AllowCredentials()
                );
            });
            
            services.AddOcelot();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /*// Exception Handler
            app.UseExceptionHandler("/Error");*/

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowOrigin");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("API gateway running!");
                });
            });
            //app.UseIpRateLimiting();

            app.UseOcelot();
           
        }
    }
}
