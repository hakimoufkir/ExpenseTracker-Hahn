using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webAPI.enums;
using webAPI.Models;
using webAPI.Services;
using webAPI.Services.interfaces;

namespace webAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IPasswordHasher _passwordHasher;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPasswordHasher passwordHasher)
            : base(options)
        {
            _passwordHasher = passwordHasher;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Expense> Expenses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Expense relationships
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(e => e.UserId);

            // Configure Budget relationships
            modelBuilder.Entity<Budget>()
                .HasOne(b => b.User)
                .WithMany(u => u.Budget)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            // Store enum as string
            modelBuilder.Entity<Expense>()
                .Property(e => e.Category)
                .HasConversion<string>();

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "User1", Email = "user1@hahn.com", Password = _passwordHasher.HashPassword( "password123") },
                new User { Id = 2, Name = "User2", Email = "user2@hahn.com", Password = _passwordHasher.HashPassword( "password123") },
                new User { Id = 3, Name = "User3", Email = "user3@hahn.com", Password = _passwordHasher.HashPassword( "password123") },
                new User { Id = 4, Name = "User4", Email = "user4@hahn.com", Password = _passwordHasher.HashPassword( "password123") }
            );

            // Seed Budgets
            modelBuilder.Entity<Budget>().HasData(
                new Budget { Id = 1, UserId = 1, MonthlyLimit = 2000.00m, TotalSpent = 115.00m, Month = MonthEnum.December },
                new Budget { Id = 2, UserId = 2, MonthlyLimit = 1500.00m, TotalSpent = 300.00m, Month = MonthEnum.December },
                new Budget { Id = 3, UserId = 3, MonthlyLimit = 2500.00m, TotalSpent = 800.00m, Month = MonthEnum.December },
                new Budget { Id = 4, UserId = 4, MonthlyLimit = 1000.00m, TotalSpent = 450.00m, Month = MonthEnum.December }
            );

            // Seed Expenses
            modelBuilder.Entity<Expense>().HasData(
                // User 1 Expenses
                new Expense { Id = 1, UserId = 1, Description = "Groceries", Amount = 75.00m, Month = MonthEnum.December, Category = ExpenseCategory.Food },
                new Expense { Id = 2, UserId = 1, Description = "Transport", Amount = 40.00m, Month = MonthEnum.December, Category = ExpenseCategory.Transport },
                // User 2 Expenses
                new Expense { Id = 3, UserId = 2, Description = "Dining Out", Amount = 150.00m, Month = MonthEnum.December, Category = ExpenseCategory.Entertainment },
                new Expense { Id = 4, UserId = 2, Description = "Internet Bill", Amount = 50.00m, Month = MonthEnum.December, Category = ExpenseCategory.Utilities },
                new Expense { Id = 5, UserId = 2, Description = "Gas", Amount = 100.00m, Month = MonthEnum.December, Category = ExpenseCategory.Transport },
                // User 3 Expenses
                new Expense { Id = 6, UserId = 3, Description = "Car Maintenance", Amount = 500.00m, Month = MonthEnum.December, Category = ExpenseCategory.Transport },
                new Expense { Id = 7, UserId = 3, Description = "Groceries", Amount = 300.00m, Month = MonthEnum.December, Category = ExpenseCategory.Food },
                // User 4 Expenses
                new Expense { Id = 8, UserId = 4, Description = "Electricity Bill", Amount = 200.00m, Month = MonthEnum.December, Category = ExpenseCategory.Utilities },
                new Expense { Id = 9, UserId = 4, Description = "Dining Out", Amount = 250.00m, Month = MonthEnum.December, Category = ExpenseCategory.Entertainment }
            );

            // Seed Incomes
            modelBuilder.Entity<Income>().HasData(
                new Income { Id = 1, UserId = 1, Amount = 3000.00m, Month = MonthEnum.December, Description = "Monthly Salary" },
                new Income { Id = 2, UserId = 2, Amount = 2500.00m, Month = MonthEnum.December, Description = "Freelance Work" },
                new Income { Id = 3, UserId = 3, Amount = 4000.00m, Month = MonthEnum.December, Description = "Consulting Project" },
                new Income { Id = 4, UserId = 4, Amount = 1500.00m, Month = MonthEnum.December, Description = "Part-Time Job" }
            );
        }

    }
}
