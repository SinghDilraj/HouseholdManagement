using HouseholdManagement.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Http;

namespace HouseholdManagement.Controllers
{
    public class BaseController : ApiController
    {
        public readonly ApplicationDbContext DbContext;
        public readonly UserManager<ApplicationUser> DefaultUserManager;

        public BaseController()
        {
            DbContext = new ApplicationDbContext();
            DefaultUserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DbContext));
        }
    }
}
