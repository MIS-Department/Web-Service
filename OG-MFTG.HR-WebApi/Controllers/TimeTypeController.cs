using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer;
using OG_MFTG.DataLayer.Repositories;

namespace OG_MFTG.HR_WebApi.Controllers
{
    public class TimeTypeController : ApiController
    {
        private TimeTypeRepository _repository; 

        // GET: api/TimeType
        public async Task<IEnumerable<TimeType>> GetAllTimeType()
        {
            _repository = new TimeTypeRepository();
            return await _repository.SelectAll();
        }

        // GET: api/TimeType/5
        public async Task<TimeType> GetTimeType(int id)
        {
            _repository = new TimeTypeRepository();
            var item = await _repository.SelectById(id);

            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return item;
        }

        public async Task<IEnumerable<TimeType>> GetTimeTypeByName(string name)
        {
            _repository = new TimeTypeRepository();

            return await _repository.Search(name);

        }

        // POST: api/TimeType      
        //public async Task<HttpResponseMessage> Post(TimeType model)
        //{
            
        //}

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
