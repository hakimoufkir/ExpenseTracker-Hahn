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

            modelBuilder.Entity<User>().HasData(
     new User
     {
         Id = 1,
         Name = "John Doe",
         Email = "johndoe@example.com",
         Password = _passwordHasher.HashPassword("password123")
     }
 );

            // Seed Budget
            modelBuilder.Entity<Budget>().HasData(
                new Budget
                {
                    Id = 1,
                    UserId = 1,
                    MonthlyLimit = 5000.00m,
                    Month = MonthEnum.December
                },
                new Budget
                {
                    Id = 2,
                    UserId = 1,
                    MonthlyLimit = 4500.00m,
                    Month = MonthEnum.November
                }
            );

            // Seed Expenses for December
            modelBuilder.Entity<Expense>().HasData(
                new Expense
                {
                    Id = 1,
                    UserId = 1,
                    Description = "Groceries",
                    Amount = 300.00m,
                    Month = MonthEnum.December,
                    Category = ExpenseCategory.Food
                },
                new Expense
                {
                    Id = 2,
                    UserId = 1,
                    Description = "Dining Out",
                    Amount = 150.00m,
                    Month = MonthEnum.December,
                    Category = ExpenseCategory.Entertainment
                },
                new Expense
                {
                    Id = 3,
                    UserId = 1,
                    Description = "Electricity Bill",
                    Amount = 120.00m,
                    Month = MonthEnum.December,
                    Category = ExpenseCategory.Utilities
                },
                new Expense
                {
                    Id = 4,
                    UserId = 1,
                    Description = "Internet Bill",
                    Amount = 60.00m,
                    Month = MonthEnum.December,
                    Category = ExpenseCategory.Utilities
                },
                new Expense
                {
                    Id = 5,
                    UserId = 1,
                    Description = "Fuel",
                    Amount = 200.00m,
                    Month = MonthEnum.December,
                    Category = ExpenseCategory.Transport
                },
                new Expense
                {
                    Id = 6,
                    UserId = 1,
                    Description = "Gym Membership",
                    Amount = 50.00m,
                    Month = MonthEnum.December,
                    Category = ExpenseCategory.Health
                },
                new Expense
                {
                    Id = 7,
                    UserId = 1,
                    Description = "Books",
                    Amount = 90.00m,
                    Month = MonthEnum.December,
                    Category = ExpenseCategory.Education
                },
                new Expense
                {
                    Id = 8,
                    UserId = 1,
                    Description = "Gifts",
                    Amount = 250.00m,
                    Month = MonthEnum.December,
                    Category = ExpenseCategory.Miscellaneous
                },
                new Expense
                {
                    Id = 9,
                    UserId = 1,
                    Description = "Holiday Shopping",
                    Amount = 600.00m,
                    Month = MonthEnum.December,
                    Category = ExpenseCategory.Miscellaneous
                }
            );

            // Seed Incomes for December
            modelBuilder.Entity<Income>().HasData(
                new Income
                {
                    Id = 1,
                    UserId = 1,
                    Amount = 4000.00m,
                    Month = MonthEnum.December,
                    Description = "Monthly Salary"
                },
                new Income
                {
                    Id = 2,
                    UserId = 1,
                    Amount = 500.00m,
                    Month = MonthEnum.December,
                    Description = "Freelance Project"
                },
                new Income
                {
                    Id = 3,
                    UserId = 1,
                    Amount = 800.00m,
                    Month = MonthEnum.December,
                    Description = "Investment Return"
                },
                new Income
                {
                    Id = 4,
                    UserId = 1,
                    Amount = 300.00m,
                    Month = MonthEnum.December,
                    Description = "Rental Income"
                }
            );
        }

    }
}
