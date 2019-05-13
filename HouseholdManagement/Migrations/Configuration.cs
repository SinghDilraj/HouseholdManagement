namespace HouseholdManagement.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HouseholdManagement.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HouseholdManagement.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            RoleManager<IdentityRole> roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(p => p.Name == "Owner"))
            {
                IdentityRole ownerRole = new IdentityRole("Owner");
                roleManager.Create(ownerRole);
            }

            if (!context.Roles.Any(p => p.Name == "Member"))
            {
                IdentityRole memberRole = new IdentityRole("Member");
                roleManager.Create(memberRole);
            }
        }
    }
}
