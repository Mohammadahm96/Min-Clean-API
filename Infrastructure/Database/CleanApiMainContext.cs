using Microsoft.EntityFrameworkCore;
using Domain.Models;



namespace Infrastructure.Database
{
    public class CleanApiMainContext : DbContext
    {
        public CleanApiMainContext() { }
        public CleanApiMainContext(DbContextOptions<CleanApiMainContext> options) : base(options) { }


        public virtual DbSet<Dog> Dogs { get; set; } // DbSet for the User table
        public virtual DbSet<Bird> Birds { get; set; }
        public virtual DbSet<Cat>  Cats { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;Port=3306;Database=Cleandb;User=root;Password=12345;";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)));
        }
    }
}

