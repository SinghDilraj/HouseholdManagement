using System;

namespace HouseholdManagement.Models.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Initiated { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public Decimal Amount { get; set; }
        public CategoryViewModel Category { get; set; }
        public int BankAccountId { get; set; }
    }
}