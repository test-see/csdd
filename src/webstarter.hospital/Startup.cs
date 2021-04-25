using csdd.Middlewares;
using foundation.config;
using foundation.servicecollection;
using irespository.user.enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using webstarter.hospital.handlers;

namespace webstarter.hospital
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
            services.AddCors(options =>
            {
                options.AddPolicy("csdd", builder =>
                {
                    builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
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

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireDefaultRole", policy =>
                {
                    policy.RequireRole(((int)Portal.Admin).ToString(),
                       ((int)Portal.Client).ToString(),
                       ((int)Portal.Hospital).ToString());
                });
                options.AddPolicy("RequireHospitalRole", policy =>
                {
                    policy.RequireRole(((int)Portal.Hospital).ToString());
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() { {  new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                    },
                    new List<string>() }
                });
            });

            services.AddRabbitMqProducer(Configuration.GetSection("AppConfig:RabbitMq"))
                .AddProductionExchange("exchange.name", Configuration.GetSection("AppConfig:RabbitMqExchange"));

            services.AddRabbitMqServices(Configuration.GetSection("AppConfig:RabbitMq"))
                .AddProductionExchange("exchange.name", Configuration.GetSection("AppConfig:RabbitMqExchange"))
                .AddAsyncMessageHandlerTransient<PurchaseGenerateAsyncMessageHandler>("purchase.generate");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHealthChecks("/health");
            app.UseWhen(p => p.Request.Path.Value.StartsWith("/api"), action => action.UseMiddleware<ApiResponseMiddleware>());

            app.UseRouting();
            app.UseCors("csdd");

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"My API {Version}");
            });
        }
    }
}
