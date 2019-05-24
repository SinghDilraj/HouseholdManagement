using HouseholdManagement.Models.Domain;
using HouseholdManagement.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HouseholdManagement.Controllers
{
    [Authorize]
    [RoutePrefix("Api/Transaction")]
    public class TransactionsController : BaseController
    {
        // GET: api/Transactions
        public IHttpActionResult Get()
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (user != null)
            {
                List<TransactionViewModel> transactions = DbContext.Transactions
                    .Where(p => p.BankAccount.Household.Members.Contains(user) || p.BankAccount.Household.Owner == user)
                    .Select(l => new TransactionViewModel
                    {
                        Id = l.Id,
                        Amount = l.Amount,
                        BankAccountId = l.BankAccount.Id,
                        Category = new CategoryViewModel
                        {
                            Id = l.Category.Id,
                            Description = l.Category.Description,
                            Created = l.Category.Created,
                            HouseholdId = l.Category.Household.Id,
                            Name = l.Category.Name,
                            Updated = l.Category.Updated
                        },
                        CategoryId = l.Category.Id,
                        Created = l.Created,
                        Description = l.Description,
                        Initiated = l.Initiated,
                        IsVoid = l.IsVoid,
                        Title = l.Title,
                        Updated = l.Updated
                    }).ToList();

                return Ok(transactions);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("{transactionId:int}")]
        // GET: api/Transactions/5
        public IHttpActionResult Get(int transactionId)
        {
            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (user != null)
            {
                Transaction transaction = DbContext.Transactions.FirstOrDefault(p => p.Id == transactionId &&
                    p.BankAccount.Household.Members.Contains(user) || p.BankAccount.Household.Owner == user);

                if (transaction != null)
                {
                    TransactionViewModel viewModel = new TransactionViewModel
                    {
                        Id = transaction.Id,
                        Amount = transaction.Amount,
                        BankAccountId = transaction.BankAccount.Id,
                        Category = new CategoryViewModel
                        {
                            Id = transaction.Category.Id,
                            Description = transaction.Category.Description,
                            Created = transaction.Category.Created,
                            HouseholdId = transaction.Category.Household.Id,
                            Name = transaction.Category.Name,
                            Updated = transaction.Category.Updated
                        },
                        CategoryId = transaction.Category.Id,
                        Created = transaction.Created,
                        Description = transaction.Description,
                        Initiated = transaction.Initiated,
                        IsVoid = transaction.IsVoid,
                        Title = transaction.Title,
                        Updated = transaction.Updated
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
                return NotFound();
            }
        }

        // POST: api/Transactions
        public IHttpActionResult Post(TransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

                if (user != null)
                {
                    if (ModelState.IsValid)
                    {
                        Transaction transaction = new Transaction
                        {
                            Title = model.Title,
                            Description = model.Description,
                            Amount = model.Amount,
                            BankAccount = DbContext.BankAccounts.FirstOrDefault(p => p.Id == model.BankAccountId),
                            Category = DbContext.Categories.FirstOrDefault(p => p.Id == model.CategoryId),
                            Initiated = model.Initiated,
                            CreatedBy = user,
                            Owner = DbContext.BankAccounts.FirstOrDefault(p => p.Id == model.BankAccountId).Household.Owner,
                        };

                        DbContext.Transactions.Add(transaction);
                        DbContext.SaveChanges();

                        TransactionViewModel viewModel = new TransactionViewModel
                        {
                            Id = transaction.Id,
                            Amount = transaction.Amount,
                            BankAccountId = transaction.BankAccount.Id,
                            Category = new CategoryViewModel
                            {
                                Id = transaction.Category.Id,
                                Description = transaction.Category.Description,
                                Created = transaction.Category.Created,
                                HouseholdId = transaction.Category.Household.Id,
                                Name = transaction.Category.Name,
                                Updated = transaction.Category.Updated
                            },
                            CategoryId = transaction.Category.Id,
                            Created = transaction.Created,
                            Description = transaction.Description,
                            Initiated = transaction.Initiated,
                            IsVoid = transaction.IsVoid,
                            Title = transaction.Title,
                            Updated = transaction.Updated
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
                return BadRequest(ModelState);
            }
        }

        [Route("{transactionId:int}")]
        // PUT: api/Transactions/5
        public IHttpActionResult Put(int transactionId, TransactionEditModel model)
        {
            Transaction transaction = DbContext.Transactions.FirstOrDefault(p => p.Id == transactionId);

            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (transaction != null && user != null)
            {
                if (transaction.Owner == user || transaction.CreatedBy == user)
                {
                    if (ModelState.IsValid)
                    {
                        transaction.Title = model.Title;
                        transaction.Description = model.Description;
                        transaction.Amount = model.Amount;
                        transaction.Initiated = model.Initiated;
                        transaction.Category = DbContext.Categories.FirstOrDefault(p => p.Id == model.CategoryId);
                        transaction.BankAccount = DbContext.BankAccounts.FirstOrDefault(p => p.Id == model.BankAccountId);

                        DbContext.SaveChanges();

                        TransactionViewModel viewModel = new TransactionViewModel
                        {
                            Id = transaction.Id,
                            Amount = transaction.Amount,
                            BankAccountId = transaction.BankAccount.Id,
                            Category = new CategoryViewModel
                            {
                                Id = transaction.Category.Id,
                                Description = transaction.Category.Description,
                                Created = transaction.Category.Created,
                                HouseholdId = transaction.Category.Household.Id,
                                Name = transaction.Category.Name,
                                Updated = transaction.Category.Updated
                            },
                            CategoryId = transaction.Category.Id,
                            Created = transaction.Created,
                            Description = transaction.Description,
                            Initiated = transaction.Initiated,
                            IsVoid = transaction.IsVoid,
                            Title = transaction.Title,
                            Updated = transaction.Updated
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

        [Route("{transactionId:int}")]
        // DELETE: api/Transactions/5
        public IHttpActionResult Delete(int transactionId)
        {
            Transaction transaction = DbContext.Transactions.FirstOrDefault(p => p.Id == transactionId);

            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (transaction != null && user != null)
            {
                if (transaction.Owner == user || transaction.CreatedBy == user)
                {
                    DbContext.Transactions.Remove(transaction);
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

        [Route("Void/{transactionId:int}")]
        public IHttpActionResult VoidTransaction(int transactionId, bool isVoid)
        {
            Transaction transaction = DbContext.Transactions.FirstOrDefault(p => p.Id == transactionId);

            Models.ApplicationUser user = DefaultUserManager.FindById(User.Identity.GetUserId());

            if (transaction != null && user != null)
            {
                if (transaction != null && user != null)
                {
                    if (isVoid)
                    {
                        transaction.IsVoid = true;

                        DbContext.SaveChanges();

                        return Ok("Transaction successfully voided.");
                    }
                    else
                    {
                        transaction.IsVoid = false;

                        DbContext.SaveChanges();

                        return Ok("Transaction successfully unvoided.");
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
    }
}
