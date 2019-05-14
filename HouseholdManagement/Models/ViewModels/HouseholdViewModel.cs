using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HouseholdManagement.Models.ViewModels
{
    public class HouseholdViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public List<string> MembersId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public HouseholdViewModel()
        {
            MembersId = new List<string>();
        }
    }
}