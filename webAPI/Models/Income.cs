using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webAPI.enums;

namespace webAPI.Models
{
    public class Income
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public MonthEnum Month { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } // Navigation property
    }
}
