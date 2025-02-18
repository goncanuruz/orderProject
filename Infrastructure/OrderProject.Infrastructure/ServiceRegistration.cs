
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

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
        }
    }
}
