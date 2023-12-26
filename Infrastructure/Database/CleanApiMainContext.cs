using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Models.User;

namespace Infrastructure.Database
{
    public class CleanApiMainContext : DbContext
    {
        public CleanApiMainContext() { }
        public CleanApiMainContext(DbContextOptions<CleanApiMainContext> options) : base(options) { }

        public virtual DbSet<Dog> Dogs { get; set; }
        public virtual DbSet<Bird> Birds { get; set; }
        public virtual DbSet<Cat> Cats { get; set; }
        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<Ownership> Ownerships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the composite primary key for Ownership entity
            modelBuilder.Entity<Ownership>().HasKey(o => new { o.UserId, o.AnimalId });

            // Configure the relationships with cascading delete
            modelBuilder.Entity<Ownership>()
                .HasOne(o => o.User)
                .WithMany(u => u.Ownerships)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ownership>()
                .HasOne(o => o.Animal)
                .WithMany(a => a.Ownerships)
                .HasForeignKey(o => o.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;Port=3306;Database=MyCleandb;User=root;Password=12345;";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)));
        }
    }
}


