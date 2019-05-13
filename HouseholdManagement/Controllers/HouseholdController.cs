using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HouseholdManagement.Controllers
{
    public class HouseholdController : BaseController
    {
        // GET: api/Household
        public IHttpActionResult Get()
        {
            return Ok();
        }

        // GET: api/Household/5
        public IHttpActionResult Get(int? householdId)
        {
            return Ok();
        }

        // POST: api/Household
        public IHttpActionResult Post()
        {
            return Ok();
        }

        // PUT: api/Household/5
        public IHttpActionResult Put(int? householdId)
        {
            return Ok();
        }

        // DELETE: api/Household/5
        public IHttpActionResult Delete(int? householdId)
        {
            return Ok();
        }
    }
}
