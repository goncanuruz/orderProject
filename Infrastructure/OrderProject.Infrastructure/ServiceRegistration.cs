
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderProject.Application.Abstractions.Services;
using OrderProject.Application.DTOs;
using OrderProject.Infrastructure.Services;
using RabbitMQ.Client;
using StackExchange.Redis;

namespace OrderProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var factory = new ConnectionFactory() { HostName = configuration["RabbitMQ:Url"] };
            factory.UserName = configuration["RabbitMQ:UserName"];
            factory.Password = configuration["RabbitMQ:Password"];

            var connection = factory.CreateConnection();
            serviceCollection.AddSingleton<IConnection>(connection);


            ApplicationSettings settings = new ApplicationSettings();
            configuration.Bind(settings);
            serviceCollection.AddSingleton(settings);

            serviceCollection.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                // Redis yapılandırma ayarlarını alıp parse ediyoruz
                var redisConfigOptions = ConfigurationOptions.Parse(configuration["Redis:Configuration"], true);
                return ConnectionMultiplexer.Connect(redisConfigOptions);
            });

            serviceCollection.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Configuration"] ;
                options.InstanceName = "OrderProject";
            });

            serviceCollection.AddScoped<IEmailService, EmailService>();
            serviceCollection.AddScoped<IRabbitMqService, RabbitMqService>();

            serviceCollection.AddSingleton<IRedisService, RedisService>();

        }
    }
}
