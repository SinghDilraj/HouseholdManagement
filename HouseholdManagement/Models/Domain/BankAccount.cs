using System;
using System.Collections.Generic;

namespace HouseholdManagement.Models.Domain
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public Decimal Balance { get; set; }
        public virtual Household Household { get; set; }
        public virtual List<Transaction> Transactions { get; set; }

        public BankAccount()
        {
            Created = DateTime.Now;
            Transactions = new List<Transaction>();
        }
    }
}