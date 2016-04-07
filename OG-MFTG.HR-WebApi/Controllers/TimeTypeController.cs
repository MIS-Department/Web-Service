using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;

namespace OG_MFTG.HR_WebApi.Controllers
{
    [RoutePrefix("hrdapi/timetype")]
    public class TimeTypeController : ApiController
    {
        private readonly ITimeTypeRepository _repository;

        public TimeTypeController(ITimeTypeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/TimeType
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<TimeType>> GetAllTimeType()
        {
            return await _repository.SelectAll();
        }

        // GET: api/TimeType/5
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(TimeType))]
        public async Task<IHttpActionResult> GetTimeType(int? id)
        {
            if (id == null)
            {
                return BadRequest("TimeType id null");
            }
            var model = await _repository.SelectById(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);

        }

        // POST: api/TimeType
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> PostTimeType([FromBody]TimeType model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _repository.Insert(model);
            string uri = Url.Link("hrdapi", new { id });
            return Created(uri, model.TimeTypeId = id);
        }

        // PUT: api/TimeType/5
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> PutTimeType([FromBody]TimeType model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repository.SelectById(model.TimeTypeId);
            if (result == null)
            {
                return NotFound();
            }
            await _repository.Update(model);
            return Ok(model);
        }

        // DELETE: api/TimeType/5
        [HttpDelete]
        [ResponseType(typeof(HttpResponseMessage))]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteTimeType(int? id)
        {
            if (id == null)
            {
                return BadRequest("TimeTypeId is null");
            }
            var model = await _repository.SelectById(id);
            if (model == null)
            {
                return NotFound();
            }
            await _repository.Delete(id);
            return Ok();
        }
    }
}
