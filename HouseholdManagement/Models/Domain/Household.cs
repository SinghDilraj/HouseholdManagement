﻿using System;
using System.Collections.Generic;

namespace HouseholdManagement.Models.Domain
{
    /// <summary>
    /// Household class that contains properties; id, name, description, members, date created and updated.
    /// </summary>
    public class Household
    {
        /// <summary>
        /// id of household
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// name of household
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// description of household
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// owner of the household
        /// </summary>
        public virtual ApplicationUser Owner { get; set; }

        /// <summary>
        /// list of members in the household
        /// </summary>
        public virtual List<ApplicationUser> Members { get; set; }

        /// <summary>
        /// list of invited users in the household
        /// </summary>
        public virtual List<ApplicationUser> Invitees { get; set; }

        /// <summary>
        /// list of categories in the household
        /// </summary>
        public virtual List<Category> Categories { get; set; }

        /// <summary>
        /// list of bank accounts in the household
        /// </summary>
        public virtual List<BankAccount> BankAccounts { get; set; }

        /// <summary>
        /// household creation date
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// household update date
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// constructor for houshold to instantiate created to current datetime
        /// and other lists.
        /// </summary>
        public Household()
        {
            Created = DateTime.Now;
            Members = new List<ApplicationUser>();
            Invitees = new List<ApplicationUser>();
            Categories = new List<Category>();
            BankAccounts = new List<BankAccount>();
        }
    }
}