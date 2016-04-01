using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;

namespace OG_MFTG.HR_WebApi.Controllers
{
    public class TimeCategoryController : ApiController
    {
        private readonly ITimeCategoryRepository _repository;

        public TimeCategoryController(ITimeCategoryRepository repository)
        {
            _repository = repository;
        }

        // GET: api/TimeCategory
        public async Task<IEnumerable<TimeCategory>> GetAllTimeCategory()
        {
            return await _repository.SelectAll();
        }

        // GET: api/TimeCategory/5
        [ResponseType(typeof(TimeCategory))]
        public async Task<IHttpActionResult> GetTimeCategory(int id)
        {

            var model = await _repository.SelectById(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        // POST: api/TimeCategory
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> PostTimeCategory(TimeCategory model)
        {
            //var id = await _repository.Insert(model); 

            //var response = Request.CreateResponse(HttpStatusCode.Created, id);

            //string uri = Url.Link("DefaultApi", new { id });
            //response.Headers.Location = new Uri(uri);
            //return response;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _repository.Insert(model);
                var response = Request.CreateResponse(HttpStatusCode.Created, id);
                string uri = Url.Link("DefaultApi", new { id });
                
                response.Headers.Location = new Uri(uri);
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }        

        }

        // PUT: api/TimeCategory/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> PutTimeCategory(int id,TimeCategory model)
        {
            //model.TimeCategoryId = id;
            //await _repository.Update(model);
            var result = await _repository.SelectById(id);
            if (result == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.TimeCategoryId = id;
            await _repository.Update(model);

            return Ok(model);
        }

        // DELETE: api/TimeCategory/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> DeleteTimeCategory(int id)
        {
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
