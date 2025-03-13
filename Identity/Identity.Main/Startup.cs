using EShopping.Core.Extensions;
using EShopping.Core.Middleware;
using Identity.Main.Extensions.Dependencies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Identity.Main
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
            services.AddAppDependencies(Configuration);
            services.AddCorsServices(Configuration);     
            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            //.AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();

            app.UseStaticFiles();

            app.UseCorsServices(Configuration);

            var fordwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            fordwardedHeaderOptions.KnownNetworks.Clear();
            fordwardedHeaderOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(fordwardedHeaderOptions);

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.None,
                MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None,
                Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always
            });

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });

            app.Use(async (httpContext, next) =>
            {
                httpContext.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate, max-age=0";
                httpContext.Response.Headers[HeaderNames.Expires] = "-1";
                await next();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
