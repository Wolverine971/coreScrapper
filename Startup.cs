using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.MemoryStorage;
using HangfireCore.Mvc.Data;
using HangfireCore.Mvc.Infrastructure;
using HangfireCore.Mvc.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;




namespace coreScrape
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
           services.AddDbContext<AppDbContext>(opts => opts.UseSqlite("DataSource=HangfireCore.db"));
            services.AddHangfire( opts => opts.UseMemoryStorage());
            services.AddHangfire(opt => opt.UseSqlServerStorage("Your Hangfire Connection string"));
            // Add framework services.
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Simple example with dependency injection for a data provider.
            services.AddSingleton<Providers.IWeatherProvider, Providers.WeatherProviderFake>();
            services.AddSingleton<Providers.IGrabbyProvider, Providers.GrabbyProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory, 
            IServiceProvider serviceProvider,
            AppDbContext ctx)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Webpack initialization with hot-reload.
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            // Configure hangfire to use the new JobActivator we defined.
            GlobalConfiguration.Configuration
                .UseActivator(new HangfireActivator(serviceProvider));

            // The rest of the hangfire config as usual.
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                name: "spa-fallback",
                defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
