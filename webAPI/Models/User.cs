using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Navigation property to Budgets (ignored in serialization)
        [JsonIgnore]
        public ICollection<Budget> Budget { get; set; }

        // Navigation property to Expenses (ignored in serialization)
        [JsonIgnore]
        public ICollection<Expense> Expenses { get; set; }  
    }

}
