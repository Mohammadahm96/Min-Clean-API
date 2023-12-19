using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Models.User;
using Domain.Models.Animal;



namespace Infrastructure.Database
{
    public class CleanApiMainContext : DbContext
    {
        public CleanApiMainContext() { }
        public CleanApiMainContext(DbContextOptions<CleanApiMainContext> options) : base(options) { }


        public virtual DbSet<Dog> Dogs { get; set; } // DbSet for the User table
        public virtual DbSet<Bird> Birds { get; set; }
        public virtual DbSet<Cat> Cats { get; set; }
        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<UserAnimal> UsersAnimals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAnimal>()
                .HasKey(ua => new { ua.UserId, ua.AnimalId });

            modelBuilder.Entity<UserAnimal>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAnimals)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<UserAnimal>()
                .HasOne(ua => ua.Animal)
                .WithMany(a => a.UserAnimals)
                .HasForeignKey(ua => ua.AnimalId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;Port=3306;Database=Cleandb;User=root;Password=12345;";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)));
        }
    }
}

