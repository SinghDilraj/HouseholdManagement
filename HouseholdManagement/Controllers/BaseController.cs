using HouseholdManagement.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Http;

namespace HouseholdManagement.Controllers
{
    /// <summary>
    /// This is a base controller which is a parent controller for all other custom, and contains properties that are commonly used in all controllers.
    /// </summary>
    public class BaseController : ApiController
    {
        /// <summary>
        /// database context property
        /// </summary>
        public readonly ApplicationDbContext DbContext;
        /// <summary>
        /// usermanage property
        /// </summary>
        public readonly UserManager<ApplicationUser> DefaultUserManager;
        /// <summary>
        /// contructor method to create an instance of other properties 
        /// </summary>
        public BaseController()
        {
            DbContext = new ApplicationDbContext();
            DefaultUserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DbContext));
        }
    }
}
