using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register the MockDatabase (if needed)
            //services.AddSingleton<MockDatabase>();

            // Register the DatabaseConfiguration
            //services.AddScoped<IDatabaseConfiguration, DatabaseConfiguration>();

            // Register the DbContext
            services.AddDbContext<CleanApiMainContext>(options =>
            {
                string connectionString = "Server=localhost;Database=MyCleandb;User=root;Password=12345;";
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)));
            });

            return services;
        }
    }
}

