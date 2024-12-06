using webAPI.enums;

namespace webAPI.DTOs
{

    public class IncomeDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public MonthEnum Month { get; set; } // Track by month
    }
    public class UserDTO
    {
        public int Id { get; set; } // Unique ID for the user
        public string Name { get; set; } // User's name
        public string Email { get; set; } // User's email
    }
    public class BudgetDTO
    {
        public int Id { get; set; }
        public decimal MonthlyLimit { get; set; }
        public decimal TotalSpent { get; set; }
        public MonthEnum Month { get; set; }
    }

    public class BudgetStatusDTO
    {
        public decimal RemainingBudget { get; set; }
        public bool IsExceeded { get; set; }
    }

    public class ExpenseSummaryDTO
    {
        public string Category { get; set; } // e.g., "Food", "Transport"
        public decimal TotalAmount { get; set; } // Total expense in that category
    }


    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; } // Stored as a string for user-facing data
        public MonthEnum Month { get; set; } // Track by Month
    }

}
