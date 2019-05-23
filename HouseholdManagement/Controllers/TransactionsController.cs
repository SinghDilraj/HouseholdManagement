using HouseholdManagement.Models.Domain;
using HouseholdManagement.Models.ViewModels;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Transactions/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Transactions
        public IHttpActionResult Post(TransactionViewModel model)
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
                    Initiated = model.Initiated
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

        [Route("{transactionId:int}")]
        // PUT: api/Transactions/5
        public IHttpActionResult Put(int transactionId, TransactionEditModel model)
        {
            return Ok();
        }

        [Route("{transactionId:int}")]
        // DELETE: api/Transactions/5
        public void Delete(int transactionId)
        {

        }

        [Route("{transactionId:int}")]
        public IHttpActionResult VoidTransaction(int transactionId)
        {
            return Ok();
        }
    }
}
