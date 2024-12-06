﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webAPI.Data;

#nullable disable

namespace webAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("webAPI.Models.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<decimal>("MonthlyLimit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalSpent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Budgets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Month = 12,
                            MonthlyLimit = 5000.00m,
                            TotalSpent = 0.00m,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Month = 11,
                            MonthlyLimit = 4500.00m,
                            TotalSpent = 0.00m,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("webAPI.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Expenses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 200.00m,
                            Category = "Food",
                            Description = "Groceries",
                            Month = 12,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 150.00m,
                            Category = "Entertainment",
                            Description = "Dining Out",
                            Month = 12,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            Amount = 120.00m,
                            Category = "Utilities",
                            Description = "Electricity Bill",
                            Month = 12,
                            UserId = 1
                        },
                        new
                        {
                            Id = 4,
                            Amount = 60.00m,
                            Category = "Utilities",
                            Description = "Internet Bill",
                            Month = 12,
                            UserId = 1
                        },
                        new
                        {
                            Id = 5,
                            Amount = 180.00m,
                            Category = "Transport",
                            Description = "Fuel",
                            Month = 12,
                            UserId = 1
                        },
                        new
                        {
                            Id = 6,
                            Amount = 50.00m,
                            Category = "Health",
                            Description = "Gym Membership",
                            Month = 12,
                            UserId = 1
                        },
                        new
                        {
                            Id = 7,
                            Amount = 90.00m,
                            Category = "Education",
                            Description = "Books",
                            Month = 12,
                            UserId = 1
                        },
                        new
                        {
                            Id = 8,
                            Amount = 250.00m,
                            Category = "Miscellaneous",
                            Description = "Gifts",
                            Month = 12,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("webAPI.Models.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Incomes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 4000.00m,
                            Description = "Monthly Salary",
                            Month = 12,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 500.00m,
                            Description = "Freelance Project",
                            Month = 12,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            Amount = 300.00m,
                            Description = "Investment Return",
                            Month = 12,
                            UserId = 1
                        },
                        new
                        {
                            Id = 4,
                            Amount = 200.00m,
                            Description = "Rental Income",
                            Month = 12,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("webAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "user1@hahn.com",
                            Name = "User1",
                            Password = "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f"
                        });
                });

            modelBuilder.Entity("webAPI.Models.Budget", b =>
                {
                    b.HasOne("webAPI.Models.User", "User")
                        .WithMany("Budget")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("webAPI.Models.Expense", b =>
                {
                    b.HasOne("webAPI.Models.User", "User")
                        .WithMany("Expenses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("webAPI.Models.Income", b =>
                {
                    b.HasOne("webAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("webAPI.Models.User", b =>
                {
                    b.Navigation("Budget");

                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}
