using foundation.config;
using foundation.ef5;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using VueCliMiddleware;

namespace csdd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppConfig = Configuration.GetSection("AppConfig").Get<AppConfig>();
        }

        public IConfiguration Configuration { get; }
        public AppConfig AppConfig { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddControllers();

            services.AddSingleton(AppConfig);
            services.AddDbContext<DefaultDbContext>(options => options.UseMySQL(AppConfig.ConnectionString));
            services.Scan(scan => scan.FromAssemblies(Assembly.Load("respository")).AddClasses(t => t.Where(type => type.IsClass))
                .AsImplementedInterfaces().WithScopedLifetime());
            services.Scan(scan => scan.FromAssemblies(Assembly.Load("domain")).AddClasses(t => t.Where(type => type.IsClass))
                .AsSelfWithInterfaces().WithScopedLifetime());
            services.Scan(scan => scan.FromAssemblies(Assembly.Load("service")).AddClasses(t => t.Where(type => type.IsClass))
                .AsImplementedInterfaces().WithScopedLifetime());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidIssuer = AppConfig.Authentication.ValidIssuer,
                      ValidAudience = AppConfig.Authentication.ValidAudience,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.Authentication.IssuerSigningKey))
                  };
              });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHealthChecks("/health");
            app.UseRouting();
            app.UseSpaStaticFiles();

            app.UseAuthentication();
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                    spa.Options.SourcePath = "ClientApp/";
                else
                    spa.Options.SourcePath = "dist";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve");
                }

            });
        }
    }
}
