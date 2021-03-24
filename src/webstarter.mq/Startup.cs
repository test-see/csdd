using EasyNetQ;
using foundation.config;
using foundation.servicecollection;
using iservice.purchase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace webstarter.mq
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppConfig = Configuration.GetSection("AppConfig").Get<AppConfig>();
            Version = Configuration.GetSection("Version").Get<string>();
        }

        public IConfiguration Configuration { get; }
        public AppConfig AppConfig { get; }
        public string Version { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddControllers();
            services.AddExtensionCollections(AppConfig);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "webstarter.mq", Version = "v1" });
            });

            Task.Run(() => SubscribeAsync(services));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "webstarter.mq v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var logpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nlog");
            if (!Directory.Exists(logpath)) Directory.CreateDirectory(logpath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(logpath),
                RequestPath = new PathString("/nlog"),
                ServeUnknownFileTypes = true,
                DefaultContentType = "text/plain; charset=utf-8"
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(logpath),
                RequestPath = new PathString("/nlog"),
            });

        }

        private async Task SubscribeAsync(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var bus = sp.GetService<IBus>();
            var log = sp.GetService<ILoggerFactory>().CreateLogger<Startup>();
            await bus.PubSub.SubscribeAsync<RabbitMqMessage<int>>("my_subscription_id",
                msg =>
                {
                    log.LogInformation("begin...");
                    sp.GetService<IPurchaseService>().Generate(msg.Payload);
                    log.LogInformation("end...");
                },
                x => x.WithTopic("Purchase.Generate"));
        }
    }
}
