﻿using HouseholdManagement.Models.Domain;
using HouseholdManagement.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace HouseholdManagement.Controllers
{
    [Authorize]
    [RoutePrefix("Api/BankAccount")]
    public class BankAccountsController : BaseController
    {
        // GET: api/BankAccounts
        public IHttpActionResult GetAllBankAccounts()
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (user != null)
            {
                System.Collections.Generic.List<BankAccountViewModel> bankAccounts = DbContext.BankAccounts
                    .Where(p => p.Household.Owner == user || p.Household.Members.Contains(user))
                    .Select(p => new BankAccountViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Balance = p.Balance,
                        Created = p.Created,
                        HouseholdId = p.Household.Id,
                        Updated = p.Updated
                    }).ToList();

                return Ok(bankAccounts);
            }
            else
            {
                return Unauthorized();
            }
        }

        [Route("{BankAccountId:int}")]
        // GET: api/BankAccounts/5
        public IHttpActionResult GetBankAccountById(int BankAccountId)
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (user != null)
            {
                BankAccount bankAccount = DbContext.BankAccounts.FirstOrDefault(p => p.Id == BankAccountId &&
                (p.Household.Owner == user || p.Household.Members.Contains(user)));

                if (bankAccount != null)
                {
                    BankAccountViewModel viewModel = new BankAccountViewModel
                    {
                        Id = bankAccount.Id,
                        Name = bankAccount.Name,
                        Description = bankAccount.Description,
                        Balance = bankAccount.Balance,
                        Created = bankAccount.Created,
                        HouseholdId = bankAccount.Household.Id,
                        Updated = bankAccount.Updated
                    };

                    return Ok(viewModel);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: api/BankAccounts
        public IHttpActionResult PostNewBankAccount(BankAccountViewModel model)
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    Household household = DbContext.Households.FirstOrDefault(p => p.Id == model.HouseholdId);

                    if (household != null)
                    {
                        if (household.Owner == user)
                        {
                            BankAccount bankAccount = new BankAccount
                            {
                                Name = model.Name,
                                Description = model.Description,
                                Balance = 0,
                                Household = household
                            };

                            DbContext.BankAccounts.Add(bankAccount);

                            DbContext.SaveChanges();

                            BankAccountViewModel viewModel = new BankAccountViewModel
                            {
                                Id = bankAccount.Id,
                                Name = bankAccount.Name,
                                Description = bankAccount.Description,
                                Balance = bankAccount.Balance,
                                Created = bankAccount.Created,
                                HouseholdId = bankAccount.Household.Id,
                                Updated = bankAccount.Updated
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
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [Route("{BankAccountId:int}")]
        // PUT: api/BankAccounts/5
        public IHttpActionResult PutEditBankAccount(int BankAccountId, BankAccountEditModel model)
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            BankAccount bankAccount = DbContext.BankAccounts.FirstOrDefault(p => p.Id == BankAccountId);

            if (bankAccount != null && user != null)
            {
                if (bankAccount.Household.Owner == user)
                {
                    if (ModelState.IsValid)
                    {
                        bankAccount.Name = model.Name;
                        bankAccount.Description = model.Description;
                        bankAccount.Household = DbContext.Households.FirstOrDefault(p => p.Id == model.HouseholdId);

                        DbContext.SaveChanges();

                        BankAccountViewModel viewModel = new BankAccountViewModel
                        {
                            Id = bankAccount.Id,
                            Name = bankAccount.Name,
                            Description = bankAccount.Description,
                            Balance = bankAccount.Balance,
                            Created = bankAccount.Created,
                            HouseholdId = bankAccount.Household.Id,
                            Updated = bankAccount.Updated
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

        [Route("{BankAccountId:int}")]
        // DELETE: api/BankAccounts/5
        public IHttpActionResult DeleteBankAccount(int BankAccountId)
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            BankAccount bankAccount = DbContext.BankAccounts.FirstOrDefault(p => p.Id == BankAccountId);

            if (bankAccount != null && user != null)
            {
                if (bankAccount.Household.Owner == user)
                {
                    DbContext.BankAccounts.Remove(bankAccount);

                    DbContext.SaveChanges();

                    return Ok("Successfully Deleted.");
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