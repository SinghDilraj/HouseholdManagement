using System.Collections.Generic;

namespace HouseholdManagement.Models.ViewModels
{
    public class HouseholdUsersViewModel
    {
        public int HouseholdId { get; set; }
        public UserViewModel Owner { get; set; }
        public List<UserViewModel> Members { get; set; }
        public List<UserViewModel> Invitees { get; set; }

        public HouseholdUsersViewModel()
        {
            Members = new List<UserViewModel>();
            Invitees = new List<UserViewModel>();
        }
    }
}