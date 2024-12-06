using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBudgetWithMonthEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthlyLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSpent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "user1@hahn.com", "User1", "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f" },
                    { 2, "user2@hahn.com", "User2", "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f" },
                    { 3, "user3@hahn.com", "User3", "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f" },
                    { 4, "user4@hahn.com", "User4", "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f" }
                });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "Id", "Month", "MonthlyLimit", "TotalSpent", "UserId", "Year" },
                values: new object[,]
                {
                    { 1, 12, 2000.00m, 115.00m, 1, 2024 },
                    { 2, 12, 1500.00m, 300.00m, 2, 2024 },
                    { 3, 12, 2500.00m, 800.00m, 3, 2024 },
                    { 4, 12, 1000.00m, 450.00m, 4, 2024 }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "Category", "Date", "Description", "UserId" },
                values: new object[,]
                {
                    { 1, 75.00m, "Food", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Groceries", 1 },
                    { 2, 40.00m, "Transport", new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Transport", 1 },
                    { 3, 150.00m, "Entertainment", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dining Out", 2 },
                    { 4, 50.00m, "Utilities", new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Internet Bill", 2 },
                    { 5, 100.00m, "Transport", new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gas", 2 },
                    { 6, 500.00m, "Transport", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Car Maintenance", 3 },
                    { 7, 300.00m, "Food", new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Groceries", 3 },
                    { 8, 200.00m, "Utilities", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electricity Bill", 4 },
                    { 9, 250.00m, "Entertainment", new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dining Out", 4 }
                });

            migrationBuilder.InsertData(
                table: "Incomes",
                columns: new[] { "Id", "Amount", "Date", "Description", "UserId" },
                values: new object[,]
                {
                    { 1, 3000.00m, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monthly Salary", 1 },
                    { 2, 2500.00m, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Freelance Work", 2 },
                    { 3, 4000.00m, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consulting Project", 3 },
                    { 4, 1500.00m, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Part-Time Job", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserId",
                table: "Incomes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
