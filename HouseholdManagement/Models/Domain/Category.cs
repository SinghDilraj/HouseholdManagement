using System;

namespace HouseholdManagement.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public Category()
        {
            DateCreated = DateTime.Now;
        }
    }
}