using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OrderProject.Persistence.Contexts
{
    public class OrderProjectDbContextFactory : IDesignTimeDbContextFactory<OrderProjectDbContext>
    { 
        public OrderProjectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<OrderProjectDbContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("Default");
            builder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 41)));
            return new OrderProjectDbContext(builder.Options);
        }

        public static OrderProjectDbContext GetContext()
        {
            var builder = new DbContextOptionsBuilder<OrderProjectDbContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString =
                configuration
                    .GetConnectionString("Default");
            builder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 41)));
            return new OrderProjectDbContext(builder.Options);
        }
    }
}
