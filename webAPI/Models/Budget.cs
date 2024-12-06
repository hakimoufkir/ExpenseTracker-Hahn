using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using webAPI.enums;

namespace webAPI.Models
{
    public class Budget
    {
        public int Id { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal MonthlyLimit { get; set; }       

        [Required]
        public MonthEnum Month { get; set; } // Enum for Month

        [Required]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; } // Navigation property
    }
}
