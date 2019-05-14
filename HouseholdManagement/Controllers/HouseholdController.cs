using HouseholdManagement.Models.Domain;
using HouseholdManagement.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace HouseholdManagement.Controllers
{
    /// <summary>
    /// Household controller to create, edit and delete a household. Also invite other users to a household and view members of a household.
    /// </summary>
    //[Authorize]
    [RoutePrefix("Api/Household")]
    public class HouseholdController : BaseController
    {
        // GET: api/Household
        //public IHttpActionResult Get()
        //{
        //    return Ok();
        //}

        //[Route("householdId:int")]
        //// GET: api/Household/5
        //public IHttpActionResult Get(int? householdId)
        //{
        //    return Ok();
        //}

        /// <summary>
        /// post method to create a household (the owner of a household is the creater of the household)
        /// </summary>
        /// <param name="model">
        /// model required with household name and description
        /// </param>
        /// <returns>
        /// ok with the created household
        /// </returns>
        // POST: api/Household
        public IHttpActionResult PostCreateHousehold(HouseholdViewModel model)
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (User != null)
            {
                Household household = new Household
                {
                    Name = model.Name,
                    Description = model.Description,
                    Owner = DbContext.Users.FirstOrDefault(p => p.Id == model.OwnerId)
                };

                DbContext.Households.Add(household);

                DbContext.SaveChanges();

                HouseholdViewModel viewModel = new HouseholdViewModel
                {
                    Name = household.Name,
                    Description = household.Description,
                    Created = household.Created,
                    Updated = household.Updated,
                    OwnerId = household.Owner.Id,
                    MembersId = household.Members.Select(p => p.Id).ToList()
                };

                return Ok(viewModel);
            }
            else
            {
                return BadRequest("User is not authenticated");
            }
        }

        /// <summary>
        /// method to edit a household (only household owner can edit a household)
        /// </summary>
        /// <param name="householdId">
        /// id int value required to find a specific household
        /// </param>
        /// <param name="model">
        /// model with new household name and description required
        /// </param>
        /// <returns>
        /// ok with the edited household
        /// </returns>
        [Route("householdId:int")]
        // PUT: api/Household/5
        public IHttpActionResult PutEditHousehold(int? householdId, HouseholdViewModel model)
        {
            if (householdId.HasValue)
            {
                Household household = DbContext.Households.FirstOrDefault(p => p.Id == householdId);

                if (household != null)
                {
                    Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

                    if (User != null && household.Owner == user)
                    {
                        household.Name = model.Name;
                        household.Description = model.Description;
                        household.Updated = DateTime.Now;

                        DbContext.SaveChanges();

                        HouseholdViewModel viewModel = new HouseholdViewModel
                        {
                            Name = household.Name,
                            Description = household.Description,
                            Created = household.Created,
                            Updated = household.Updated,
                            OwnerId = household.Owner.Id,
                            MembersId = household.Members.Select(p => p.Id).ToList()
                        };

                        return Ok(viewModel);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Household id not valid");
            }
        }

        /// <summary>
        /// method to delete a household
        /// </summary>
        /// <param name="householdId">
        /// household id int value required to find a specific household
        /// </param>
        /// <returns>
        /// ok status with success message
        /// </returns>
        [Route("householdId:int")]
        // DELETE: api/Household/5
        public IHttpActionResult DeleteHousehold(int? householdId)
        {
            if (householdId.HasValue)
            {
                Household household = DbContext.Households.FirstOrDefault(p => p.Id == householdId);

                if (household != null)
                {
                    Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

                    if (User != null && household.Owner == user)
                    {
                        DbContext.Households.Remove(household);

                        DbContext.SaveChanges();

                        return Ok("Successfully deleted.");
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Household id not valid");
            }
        }

        public void InviteUsers(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {

            }
        }

        public IHttpActionResult JoinHousehold(int? householdId)
        {
            if (householdId.HasValue)
            {
                Household household = DbContext.Households.FirstOrDefault(p => p.Id == householdId);

                if (household != null)
                {
                    Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

                    if (User != null && household.Owner != user && !household.Members.Contains(user))
                    {
                        household.Members.Add(user);

                        DbContext.SaveChanges();

                        return Ok("Successfully joined the household.");
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Household id not valid");
            }
        }

        public IHttpActionResult LeaveHousehold(int? householdId)
        {
            if (householdId.HasValue)
            {
                Household household = DbContext.Households.FirstOrDefault(p => p.Id == householdId);

                if (household != null)
                {
                    Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

                    if (User != null && household.Owner != user && household.Members.Contains(user))
                    {
                        household.Members.Remove(user);

                        DbContext.SaveChanges();

                        return Ok("Successfully left the household.");
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Household id not valid");
            }
        }
    }
}
