﻿using System;

namespace HouseholdManagement.Models.Domain
{
    /// <summary>
    /// category class contains property; id, name, description, created and updated
    /// </summary>
    public class Category
    {
        /// <summary>
        /// id of category
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// name of category
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// description of category
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// creation date of category
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// update date of category
        /// </summary>
        public DateTime? Updated { get; set; }
        /// <summary>
        /// constructor for category to create instance of date created to current date
        /// </summary>
        public Category()
        {
            Created = DateTime.Now;
        }
    }
}