using Infrastructure.Database;
using Infrastructure.Repositories.Birds;
using Infrastructure.Security;
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
            services.AddScoped<IDogRepository, DogRepository>();
            services.AddScoped<IBirdRepository, BirdRepository>();
            services.AddScoped<ICatRepository, CatRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserRepository, UserRepository>();

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