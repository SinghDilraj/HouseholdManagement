using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HouseholdManagement.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: api/Category
        public IHttpActionResult Get()
        {
            return Ok();
        }

        // GET: api/Category/5
        public IHttpActionResult Get(int? householdId)
        {
            return Ok();
        }

        // POST: api/Category
        public IHttpActionResult Post()
        {
            return Ok();
        }

        // PUT: api/Category/5
        public IHttpActionResult Put(int? householdId)
        {
            return Ok();
        }

        // DELETE: api/Category/5
        public IHttpActionResult Delete(int? householdId)
        {
            return Ok();
        }
    }
}
