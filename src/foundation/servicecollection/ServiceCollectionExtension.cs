using AutoMapper;
using EasyNetQ;
using foundation._3party;
using foundation.config;
using foundation.ef5;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddDbContext<DefaultDbContext>(options => options.UseMySQL(AppConfig.ConnectionString));
            services.AddScoped<DefaultDbTransaction>();
            services.Scan(scan => scan.FromAssemblies(Assembly.Load("respository")).AddClasses(t => t.Where(type => type.IsClass))
                .AsImplementedInterfaces().WithScopedLifetime());
            services.Scan(scan => scan.FromAssemblies(Assembly.Load("domain")).AddClasses(t => t.Where(type => type.IsClass))
                .AsSelfWithInterfaces().WithScopedLifetime());
            services.Scan(scan => scan.FromAssemblies(Assembly.Load("service")).AddClasses(t => t.Where(type => type.IsClass))
                .AsImplementedInterfaces().WithScopedLifetime());

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.Load("domain"));
            });
            services.AddSingleton(config.CreateMapper());
            services.AddSingleton(RabbitHutch.CreateBus("host=localhost"));
        }
    }
}
