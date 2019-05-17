using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdManagement.Models.ViewModels
{
    public class CategoryEditModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int HouseholdId { get; set; }
    }
}