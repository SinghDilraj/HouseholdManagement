using System;

namespace HouseholdManagement.Models.ViewModels
{
    public class TransactionEditModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public int BankAccountId { get; set; }
        public DateTime Initiated { get; set; }
    }
}