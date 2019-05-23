using System;

namespace HouseholdManagement.Models.Domain
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVoid { get; set; }
        public DateTime Initiated { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public decimal Amount { get; set; }
        public virtual Category Category { get; set; }
        public virtual BankAccount BankAccount { get; set; }

        public Transaction()
        {
            Created = DateTime.Now;
        }
    }
}