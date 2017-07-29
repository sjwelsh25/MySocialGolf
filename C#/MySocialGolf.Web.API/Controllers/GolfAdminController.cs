using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MySocialGolf.Web.API.Controllers
{
    public class GolfAdminController : ApiController
    {
        // GET: api/GolfAdmin
        /// <summary>
        /// Get a Golf Admin Summary
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "GolfAdmin", "DefaultValue" };
        }

        // GET: api/GolfAdmin/5
        /// <summary>
        /// Get a specific GolfAdmin by id Summary
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GolfAdmin
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GolfAdmin/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GolfAdmin/5
        public void Delete(int id)
        {
        }
    }
}
