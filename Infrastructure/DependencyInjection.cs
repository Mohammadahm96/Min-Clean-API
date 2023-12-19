using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Repositories;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register the MockDatabase (if needed)
            services.AddSingleton<MockDatabase>();

            // Register the DatabaseConfiguration
            //services.AddScoped<IDatabaseConfiguration, DatabaseConfiguration>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Register the DbContext
            services.AddDbContext<CleanApiMainContext>(options =>
            {
                string connectionString = "Server=localhost;Database=Cleandb;User=root;Password=12345;";
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)));
            });

            return services;
        }
    }
}

