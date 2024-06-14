using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Order.Domain.Repositories;
using Order.Infrastructure.CacheStorage;
using Order.Infrastructure.MessageBus;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Persistence.Repositories;
using Order.Infrastructure.ServiceDiscovery;
using RabbitMQ.Client;

namespace Order.Infrastructure
{
    public static class InfrastructureInjection
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(_ => new MongoDbOptions(configuration["Mongo:Database"]!,
                configuration["Mongo:Host"]!));

            services.AddSingleton<IMongoClient>(sp => new MongoClient(sp.GetRequiredService<MongoDbOptions>().ConnectionString));
            services.AddTransient(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(sp.GetRequiredService<MongoDbOptions>().Database));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }

        public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(configuration["RabbitMq:Host"]!)
            };

            var connection = connectionFactory.CreateConnection(configuration["RabbitMq:Queue"]!);

            services.AddSingleton(new ProducerConnection(connection));
            services.AddSingleton<IMessageBusClient, RabbitMqClient>();

            return services;
        }

        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(sp => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(configuration["Consul:Host"]!);
            }));

            services.AddTransient<IServiceDiscoveryService, ConsulService>();
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            var registration = new AgentServiceRegistration
            {
                ID = $"order-service-{Guid.NewGuid()}",
                Name = "OrderServices",
                Address = "localhost",
                Port = 5003
            };

            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            Console.WriteLine("Service registered in Consul");

            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
                Console.WriteLine("Service deregistered from Consul");
            });

            return app;
        }

        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = "OrdersCache";
                options.Configuration = configuration["Redis:Host"];
            });

            services.AddTransient<ICacheService, CacheService>();
            return services;
        }
    }
}
