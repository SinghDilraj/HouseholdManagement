using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdManagement.Models.ViewModels
{
    public class HouseholdUsersViewModel
    {
        public int HouseholdId { get; set; }
        public UserViewModel Owner { get; set; }
        public List<UserViewModel> Members { get; set; }

        public HouseholdUsersViewModel()
        {
            Members = new List<UserViewModel>();
        }
    }
}