using System.Collections.Generic;
using System.Web.Http;
using OG_MFTG.DataLayer.Interfaces;

namespace OG_MFTG.HR_WebApi.Controllers
{
    public class TimeTypeController : ApiController
    {
        private readonly ITimeTypeRepository _repository;

        public TimeTypeController(ITimeTypeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/TimeType
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TimeType/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TimeType
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TimeType/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TimeType/5
        public void Delete(int id)
        {
        }
    }
}
