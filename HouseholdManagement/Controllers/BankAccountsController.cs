using HouseholdManagement.Models.Domain;
using HouseholdManagement.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace HouseholdManagement.Controllers
{
    [Authorize]
    [RoutePrefix("Api/BankAccount")]
    public class BankAccountsController : BaseController
    {
        // GET: api/BankAccounts
        [Route("")]
        public IHttpActionResult GetAllBankAccounts()
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (user != null)
            {
                System.Collections.Generic.List<BankAccountViewModel> bankAccounts = DbContext.BankAccounts
                    .Where(p => p.Household.Owner.Id == user.Id || p.Household.Members.Any(q => q.Id == user.Id))
                    .Select(p => new BankAccountViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Balance = p.Balance,
                        Created = p.Created,
                        HouseholdId = p.Household.Id,
                        Updated = p.Updated,
                        Transactions = p.Transactions
                        .Select(q => new TransactionViewModel
                        {
                            Id = q.Id,
                            Amount = q.Amount,
                            Description = q.Description,
                            Category = new CategoryViewModel
                            {
                                Id = q.Category.Id,
                                Created = q.Category.Created,
                                Description = q.Category.Description,
                                HouseholdId = q.Category.Household.Id,
                                Name = q.Category.Name,
                                Updated = q.Category.Updated
                            },
                            BankAccountId = q.BankAccount.Id,
                            Created = q.Created,
                            Initiated = q.Initiated,
                            Title = q.Title,
                            Updated = q.Updated
                        }).ToList()
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
                (p.Household.Owner == user || p.Household.Members.Any(q => q.Id == user.Id)));

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
                        Updated = bankAccount.Updated,
                        Transactions = bankAccount.Transactions
                        .Select(q => new TransactionViewModel
                        {
                            Id = q.Id,
                            Amount = q.Amount,
                            Description = q.Description,
                            Category = new CategoryViewModel
                            {
                                Id = q.Category.Id,
                                Created = q.Category.Created,
                                Description = q.Category.Description,
                                HouseholdId = q.Category.Household.Id,
                                Name = q.Category.Name,
                                Updated = q.Category.Updated
                            },
                            BankAccountId = q.BankAccount.Id,
                            Created = q.Created,
                            Initiated = q.Initiated,
                            Title = q.Title,
                            Updated = q.Updated
                        }).ToList()
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
        [Route("")]
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
                                Updated = bankAccount.Updated,
                                Transactions = bankAccount.Transactions
                                    .Select(q => new TransactionViewModel
                                    {
                                        Id = q.Id,
                                        Amount = q.Amount,
                                        Description = q.Description,
                                        Category = new CategoryViewModel
                                        {
                                            Id = q.Category.Id,
                                            Created = q.Category.Created,
                                            Description = q.Category.Description,
                                            HouseholdId = q.Category.Household.Id,
                                            Name = q.Category.Name,
                                            Updated = q.Category.Updated
                                        },
                                        BankAccountId = q.BankAccount.Id,
                                        Created = q.Created,
                                        Initiated = q.Initiated,
                                        Title = q.Title,
                                        Updated = q.Updated
                                    }).ToList()
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
                        bankAccount.Updated = DateTime.Now;

                        DbContext.SaveChanges();

                        BankAccountViewModel viewModel = new BankAccountViewModel
                        {
                            Id = bankAccount.Id,
                            Name = bankAccount.Name,
                            Description = bankAccount.Description,
                            Balance = bankAccount.Balance,
                            Created = bankAccount.Created,
                            HouseholdId = bankAccount.HouseholdId,
                            Updated = bankAccount.Updated,
                            Transactions = bankAccount.Transactions
                        .Select(q => new TransactionViewModel
                        {
                            Id = q.Id,
                            Amount = q.Amount,
                            Description = q.Description,
                            Category = new CategoryViewModel
                            {
                                Id = q.Category.Id,
                                Created = q.Category.Created,
                                Description = q.Category.Description,
                                HouseholdId = q.Category.Household.Id,
                                Name = q.Category.Name,
                                Updated = q.Category.Updated
                            },
                            BankAccountId = q.BankAccount.Id,
                            Created = q.Created,
                            Initiated = q.Initiated,
                            Title = q.Title,
                            Updated = q.Updated
                        }).ToList()
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

        [Route("UpdateBalance/{BankAccountId:int}")]
        public IHttpActionResult UpdateBalance(int BankAccountId)
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            BankAccount bankAccount = DbContext.BankAccounts.FirstOrDefault(p => p.Id == BankAccountId);

            if (user != null && bankAccount != null)
            {
                if (bankAccount.Household.Owner == user)
                {
                    bankAccount.Balance = bankAccount.Transactions
                        .Where(p => !p.IsVoid)
                        .Select(p => p.Amount).Sum();

                    return Ok($"Balance Updated, new Balance is {bankAccount.Balance}");
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
