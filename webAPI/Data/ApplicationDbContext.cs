using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webAPI.Models;

namespace webAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Precomputed hashed passwords (replace these with actual hashed values)
            var passwordHasher = new PasswordHasher<User>();
            string hashedPassword1 = passwordHasher.HashPassword(null, "Test@123");
            string hashedPassword2 = passwordHasher.HashPassword(null, "Test@123");

            // Seed initial users with precomputed passwords
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "user1@hahn.com",
                    Name = "hahn 1",
                    Password = hashedPassword1
                },
                new User
                {
                    Id = 2,
                    Email = "user2@hahn.com",
                    Name = "hahn 2",
                    Password = hashedPassword2
                }
            );
        }

    }
}
