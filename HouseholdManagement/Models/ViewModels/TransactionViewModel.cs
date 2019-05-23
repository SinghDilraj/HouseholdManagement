using System;
using System.ComponentModel.DataAnnotations;

namespace HouseholdManagement.Models.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Initiated { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public CategoryViewModel Category { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int BankAccountId { get; set; }
        public bool IsVoid { get; set; }
    }
}