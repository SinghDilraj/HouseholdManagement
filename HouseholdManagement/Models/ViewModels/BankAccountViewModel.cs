using System;
using System.ComponentModel.DataAnnotations;

namespace HouseholdManagement.Models.ViewModels
{
    public class BankAccountViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public Decimal Balance { get; set; }
        [Required]
        public int HouseholdId { get; set; }
    }
}