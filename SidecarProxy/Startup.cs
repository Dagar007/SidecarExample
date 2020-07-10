using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SidecarProxy.HttpHandlers;

namespace SidecarProxy
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
            services.AddHttpContextAccessor();
            services.AddTransient<AdditionalHeaderHandler>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.RunProxy(new ProxyOptions
                {
                    Scheme = "http",
                    Host = "localhost",
                    Port = Configuration["DEMOAPI_PORT"],
                    BackChannelMessageHandler = new AdditionalHeaderHandler()
                });
            }
            else
            {
                app.RunProxy(new ProxyOptions
                {
                    Scheme = "http",
                    Host = "localhost",
                    Port = Environment.GetEnvironmentVariable("DEMOAPI_PORT"),
                    BackChannelMessageHandler = new AdditionalHeaderHandler()
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
