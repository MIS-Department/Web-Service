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
    [RoutePrefix("hrdapi/timecategory")]
    public class TimeCategoryController : ApiController
    {
        private readonly ITimeCategoryRepository _repository;

        public TimeCategoryController(ITimeCategoryRepository repository)
        {
            _repository = repository;
        }

        // GET: api/TimeCategory
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<TimeCategory>> GetAllTimeCategory()
        {
            return await _repository.SelectAll();
        }

        // GET: api/TimeCategory/5
        [HttpGet]
        [Route("{id:int:min(1)}")]
        [ResponseType(typeof(TimeCategory))]
        public async Task<IHttpActionResult> GetTimeCategory(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = await _repository.SelectById(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        // POST: api/TimeCategory
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> PostTimeCategory(TimeCategory model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _repository.Insert(model);

                string uri = Url.Link("DefaultApi", new { id });

                return Created(uri, model.TimeCategoryId = id);
            }
            catch (Exception)
            {

                throw;
            }

        }

        // PUT: api/TimeCategory/5
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> PutTimeCategory(TimeCategory model)
        {  
            var result = await _repository.SelectById(model.TimeCategoryId);
            if (result == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }        
            await _repository.Update(model);

            return Ok(model);
        }

        // DELETE: api/TimeCategory/5
        [HttpDelete]
        [Route("{id:int:min(1}")]
        [ResponseType(typeof(HttpResponseMessage))]        
        public async Task<IHttpActionResult> DeleteTimeCategory(int? id)
        {
            if (id == null)
            {
                return BadRequest();
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
