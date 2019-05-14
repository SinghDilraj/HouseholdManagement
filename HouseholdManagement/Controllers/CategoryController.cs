using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HouseholdManagement.Controllers
{
    //[Authorize]
    [RoutePrefix("Api/Category")]
    public class CategoryController : BaseController
    {
        // GET: api/Category
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Route("categoryId:int")]
        // GET: api/Category/5
        public IHttpActionResult Get(int? categoryId)
        {
            return Ok();
        }

        // POST: api/Category
        public IHttpActionResult Post()
        {
            return Ok();
        }

        // PUT: api/Category/5
        public IHttpActionResult Put(int? categoryId)
        {
            return Ok();
        }

        // DELETE: api/Category/5
        public IHttpActionResult Delete(int? categoryId)
        {
            return Ok();
        }
    }
}
