using HouseholdManagement.Models.Domain;
using HouseholdManagement.Models.Helpers;
using HouseholdManagement.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace HouseholdManagement.Controllers
{
    /// <summary>
    /// Household controller to create, edit and delete a household. Also invite other users to a household and view members of a household.
    /// </summary>
    //[Authorize]
    [RoutePrefix("Api/Household")]
    public class HouseholdController : BaseController
    {
        /// <summary>
        /// get method to return all the households the user is part of 
        /// </summary>
        /// <returns>
        /// ok with list of all the households user is part of
        /// </returns>
        //GET: api/Household
        public IHttpActionResult GetAllHouseholds()
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (user != null)
            {
                System.Collections.Generic.List<HouseholdViewModel> households = DbContext.Households
                    .Where(p => p.Owner == user || p.Members.Contains(user))
                    .Select(p => new HouseholdViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Created = p.Created,
                        Updated = p.Updated,
                        OwnerId = p.Owner.Id,
                        MembersId = p.Members.Select(q => q.Id).ToList()
                    }).ToList();

                return Ok(households);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("Users")]
        //GET: api/Household
        public IHttpActionResult GetUsersFromHouseholds()
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (user != null)
            {
                System.Collections.Generic.List<HouseholdViewModel> households = DbContext.Households
                    .Where(p => p.Owner == user || p.Members.Contains(user))
                    .Select(p => new HouseholdViewModel
                    {
                        Id = p.Id,
                        OwnerId = p.Owner.Id,
                        MembersId = p.Members.Select(q => q.Id).ToList()
                    }).ToList();

                return Ok(households);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// get method to return a household if user is a part of it
        /// </summary>
        /// <param name="householdId">
        /// householdId int value
        /// </param>
        /// <returns>
        /// ok with household 
        /// </returns>
        [Route("{householdId:int}")]
        // GET: api/Household/5
        public IHttpActionResult GetHouseholdById(int? householdId)
        {
            if (householdId.HasValue)
            {
                Household household = DbContext.Households.FirstOrDefault(p => p.Id == householdId);

                Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

                if (household != null && user != null)
                {
                    if (household.Owner == user || household.Members.Contains(user))
                    {
                        HouseholdViewModel viewModel = new HouseholdViewModel
                        {
                            Id = household.Id,
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
                return BadRequest("Id not valid");
            }
        }

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
                    Id = household.Id,
                    Name = household.Name,
                    Description = household.Description,
                    Created = household.Created,
                    Updated = household.Updated,
                    OwnerId = household.Owner?.Id,
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
        [Route("{householdId:int}")]
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
                            Id = household.Id,
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
        [Route("{householdId:int}")]
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

        /// <summary>
        /// post method to invite users to a household (only owner of the household can invite)
        /// </summary>
        /// <param name="userId">
        /// id of user to invite
        /// </param>
        /// <param name="householdId">
        /// id of household to join through invitation
        /// </param>
        /// <returns>
        /// ok with a link to join household
        /// </returns>
        [Route("{userEmail:minlength(1)}/{householdId:int}")]
        public IHttpActionResult PostInviteUsers(string userEmail, int? householdId)
        {
            if (!string.IsNullOrEmpty(userEmail) && householdId.HasValue)
            {
                Models.ApplicationUser user = DefaultUserManager.FindByEmail(userEmail);

                Household household = DbContext.Households.FirstOrDefault(p => p.Id == householdId);

                Models.ApplicationUser owner = DefaultUserManager.FindById(User.Identity.GetUserId());

                if (household != null && user != null && owner != null)
                {
                    if (household.Owner == owner)
                    {
                        MyEmailService mailService = new MyEmailService();

                        if (!household.Invitees.Contains(user))
                        {
                            household.Invitees.Add(user);

                            DbContext.SaveChanges();

                            string link = Url.Link("DefaultApi", new { Controller = "Household/Join", Id = $"{householdId}" });

                            string body = $"<p>you recieved an invitation from {User.Identity.Name} to join the household {household.Name}. click or go to this link <a href='{ link}'>Join Household</a></p>";

                            mailService.Send(user.Email, "Invitation to Household", body);

                            return Ok(MvcHtmlString.Create(body));
                        }
                        else
                        {
                            return BadRequest("User already invited");
                        }
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
                return BadRequest("id not valid");
            }
        }

        /// <summary>
        /// method to join a household
        /// </summary>
        /// <param name="householdId">
        /// int household to join id
        /// </param>
        /// <returns>
        /// ok success message
        /// </returns>
        [Route("Join/{householdId:int}")]
        public IHttpActionResult JoinHousehold(int? householdId)
        {
            if (householdId.HasValue)
            {
                Household household = DbContext.Households.FirstOrDefault(p => p.Id == householdId);

                if (household != null)
                {
                    Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

                    if (User != null && household.Owner != user && !household.Members.Contains(user) && household.Invitees.Contains(user))
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

        /// <summary>
        /// method to leave a household
        /// </summary>
        /// <param name="householdId">
        /// int household to leave id
        /// </param>
        /// <returns>
        /// ok success message
        /// </returns>
        [Route("Leave/{householdId:int}")]
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
