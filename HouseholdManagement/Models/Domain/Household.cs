using System;
using System.Collections.Generic;

namespace HouseholdManagement.Models.Domain
{
    public class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ApplicationUser> Members { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public Household()
        {
            DateCreated = DateTime.Now;
        }
    }
}