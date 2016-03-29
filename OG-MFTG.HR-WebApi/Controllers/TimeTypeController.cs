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
        private IIO<TimeType> _repository; 

        // GET: api/TimeType
        public async Task<IEnumerable<TimeType>> Get()
        {
            _repository = new TimeTypeRepository();
            return await _repository.SelectAll();
        }

        // GET: api/TimeType/5
        public async Task<IEnumerable<TimeType>> Get(int id)
        {
            _repository = new TimeTypeRepository();
            var item = _repository.SelectById(id);

            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return await item;
        }

        // POST: api/TimeType      
        public async Task<HttpResponseMessage> Post(TimeType model)
        {
            _repository = new TimeTypeRepository();
            await _repository.Insert(model);

            var response = Request.CreateResponse(HttpStatusCode.Created, model);

            string uri = Url.Link("DefaultApi", new {id = model.TimeTypeId});
            response.Headers.Location = new Uri(uri);
            return response;
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
