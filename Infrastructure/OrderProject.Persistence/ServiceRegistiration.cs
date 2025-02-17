using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderProject.Application.Repositories;
using OrderProject.Persistence.Contexts;
using OrderProject.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext ekleme
            services.AddDbContext<OrderProjectDbContext>(options =>
            {
                options.EnableSensitiveDataLogging(); // Geliştirme ortamında hassas veri günlüğü için
                options.UseMySql(
                    configuration.GetConnectionString("Default"),
                    new MySqlServerVersion(new Version(8, 0, 41)) // MySQL sürümünüzü burada belirtin
                );
            }, ServiceLifetime.Scoped);


            // Repository'ler için bağımlılık ekleme
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Generic Repository
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>)); // Sadece okuma için Repository
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>)); // Sadece yazma için Repository


        }
    }
}
