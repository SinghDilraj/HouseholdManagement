using System;

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

        public BankAccount()
        {
            Created = DateTime.Now;
        }
    }
}