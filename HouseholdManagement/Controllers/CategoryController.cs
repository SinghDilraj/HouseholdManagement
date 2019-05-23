using HouseholdManagement.Models.Domain;
using HouseholdManagement.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace HouseholdManagement.Controllers
{
    /// <summary>
    /// Category controller to get, create, edit and delete categories
    /// </summary>
    [Authorize]
    [RoutePrefix("Api/Category")]
    public class CategoryController : BaseController
    {
        /// <summary>
        /// Get method to return all categories
        /// </summary>
        /// <returns>
        /// list of all categories
        /// </returns>
        // GET: api/Category
        public IHttpActionResult GetAllCategories()
        {
            string userId = User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userId))
            {
                System.Collections.Generic.List<CategoryViewModel> categories = DbContext.Categories
                    .Where(p => p.Household.Owner.Id == userId || p.Household.Members.Any(q => q.Id == userId))
                    .Select(p => new CategoryViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        HouseholdId = p.Household.Id,
                        Created = p.Created,
                        Updated = p.Updated
                    }).ToList();

                return Ok(categories);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// get method to return a category from an id
        /// </summary>
        /// <param name="categoryId">
        /// category id required
        /// </param>
        /// <returns>
        /// category
        /// </returns>
        [Route("{categoryId:int}")]
        // GET: api/Category/5
        public IHttpActionResult Get(int categoryId)
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            Category category = DbContext.Categories
                    .FirstOrDefault(p => p.Id == categoryId);

            if (user != null && category != null)
            {
                if (category.Household.Owner == user || category.Household.Members.Any(q => q == user))
                {
                    CategoryViewModel viewModel = new CategoryViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
                        HouseholdId = category.Household.Id,
                        Created = category.Created,
                        Updated = category.Updated
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

        /// <summary>
        /// Post method to create a new category
        /// </summary>
        /// <param name="model">
        /// category model with category name and description with household id int required
        /// </param>
        /// <returns>
        /// newly created category
        /// </returns>
        // POST: api/Category
        public IHttpActionResult Post(CategoryViewModel model)
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (User != null)
            {
                if (ModelState.IsValid)
                {
                    Category category = new Category
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Household = DbContext.Households.FirstOrDefault(p => p.Id == model.HouseholdId)
                    };

                    DbContext.Categories.Add(category);

                    DbContext.SaveChanges();

                    CategoryViewModel viewModel = new CategoryViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
                        Created = category.Created,
                        Updated = category.Updated,
                        HouseholdId = category.Household.Id
                    };

                    return Ok(viewModel);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// put method to edit existing category
        /// </summary>
        /// <param name="categoryId">
        /// int category id required
        /// </param>
        /// <param name="model">
        /// model with categiry name and category description required
        /// </param>
        /// <returns>
        /// edited category
        /// </returns>
        // PUT: api/Category/5
        [Route("{categoryId:int}")]
        public IHttpActionResult Put(int categoryId, CategoryViewModel model)
        {
            Category category = DbContext.Categories.FirstOrDefault(p => p.Id == categoryId);

            if (category != null)
            {
                Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

                if (User != null && category.Household.Owner == user)
                {
                    if (ModelState.IsValid)
                    {
                        category.Name = model.Name;
                        category.Description = model.Description;
                        category.Household = DbContext.Households.FirstOrDefault(p => p.Id == model.HouseholdId);
                        category.Updated = DateTime.Now;

                        DbContext.SaveChanges();

                        CategoryViewModel viewModel = new CategoryViewModel
                        {
                            Id = category.Id,
                            Name = category.Name,
                            Description = category.Description,
                            Created = category.Created,
                            Updated = category.Updated,
                            HouseholdId = category.Household.Id
                        };

                        return Ok(viewModel);
                    }
                    else
                    {
                        return BadRequest(ModelState);
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

        /// <summary>
        /// Delete method to delete a category
        /// </summary>
        /// <param name="categoryId">
        /// category id int required
        /// </param>
        /// <returns>
        /// ok with success message
        /// </returns>
        // DELETE: api/Category/5
        [Route("{categoryId:int}")]
        public IHttpActionResult Delete(int categoryId)
        {
            Category category = DbContext.Categories.FirstOrDefault(p => p.Id == categoryId);

            if (category != null)
            {
                Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

                if (User != null && category.Household.Owner == user)
                {
                    DbContext.Categories.Remove(category);

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
    }
}
