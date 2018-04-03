using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UMATestApi.Auth;

namespace UMATestApi.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {

        // GET api/values
        [SecureApi]
        [Route("api/values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [SecureApi]
        [Route("values")]
        public IEnumerable<string> GetValues()
        {
            return new string[] { "value11", "value12" };
        }

        [SecureApi]
        [Route("api/persons")]
        public IEnumerable<string> GetPersons()
        {
            return new string[] { "Person1", "Person2" };
        }

        // GET api/values/5
        [SecureApi]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
