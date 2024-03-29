﻿using foundation._3party;
using foundation.config;
using foundation.ef5;
using Mediator.Net;
using Mediator.Net.MicrosoftDependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using TencentCloud.Common;

namespace foundation.servicecollection
{
    public static class ServiceCollectionExtension
    {
        public static void AddExtensionCollections(this IServiceCollection services, AppConfig AppConfig)
        {
            services.AddSingleton(AppConfig);
            services.AddSingleton(new SmsSendRequest(new Credential { SecretId = AppConfig.TencentCloudSMS?.SecretId, SecretKey = AppConfig.TencentCloudSMS?.SecretKey, }));
            services.AddDbContext<DefaultDbContext>(options => options.UseMySql(AppConfig.ConnectionString, new MySqlServerVersion(new Version(5, 7, 18))));

            services.AddScoped<DefaultDbTransaction>();
            services.Scan(scan => scan.FromAssemblies(Assembly.Load("respository")).AddClasses(t => t.Where(type => type.IsClass))
                .AsImplementedInterfaces().WithScopedLifetime());
            services.Scan(scan => scan.FromAssemblies(Assembly.Load("client.storage.v2")).AddClasses(t => t.Where(type => type.IsClass))
                .AsImplementedInterfaces().WithScopedLifetime());
            services.Scan(scan => scan.FromAssemblies(Assembly.Load("domain")).AddClasses(t => t.Where(type => type.IsClass))
                .AsSelfWithInterfaces().WithScopedLifetime());
            services.Scan(scan => scan.FromAssemblies(Assembly.Load("client.domain.v2")).AddClasses(t => t.Where(type => type.IsClass))
                .AsSelf().WithScopedLifetime());
            services.Scan(scan => scan.FromAssemblies(Assembly.Load("service")).AddClasses(t => t.Where(type => type.IsClass))
                .AsImplementedInterfaces().WithScopedLifetime());
            services.Scan(scan => scan.FromAssemblies(Assembly.Load("client.application.v2")).AddClasses(t => t.Where(type => type.IsClass))
                .AsSelf().WithScopedLifetime());

            var mediaBuilder = new MediatorBuilder();
            mediaBuilder.RegisterHandlers(Assembly.Load("domain.bff"), Assembly.Load("storage"));
            services.RegisterMediator(mediaBuilder);

            services.AddCap(x =>
            {
                x.UseRabbitMQ(t => {
                    t.HostName = AppConfig.RabbitMq.HostName;
                    t.UserName = AppConfig.RabbitMq.UserName;
                    t.Password = AppConfig.RabbitMq.Password;
                });
                x.UseEntityFramework<DefaultDbContext>();
            });
        }
    }
}
