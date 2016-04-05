using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Management;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;

namespace OG_MFTG.HR_WebApi.Controllers
{
    public class DailyTimeRecordController : ApiController
    {
        private readonly IDailyTimeRecordRepository _repository;

        public DailyTimeRecordController(IDailyTimeRecordRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<DailyTimeRecord>> GetAllDailyTimeRecord()
        {
            return await _repository.SelectAll();
        }

        [HttpGet]
        [ResponseType(typeof(DailyTimeRecord))]
        public async Task<IHttpActionResult> GetDailyTimeRecord(int? id)
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

        [HttpPost]
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> PostDailyTimeRecord(DailyTimeRecord model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _repository.Insert(model);
            string uri = Url.Link("DefaultApi", new { id });
            return Created(uri, model.DailyTimeRecordId = id);
        }

        [HttpPut]
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> PutDailyTimeRecord(DailyTimeRecord model)
        {
            var result = await _repository.SelectById(model.DailyTimeRecordId);
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

        [HttpDelete]
        [ResponseType(typeof (HttpResponseMessage))]
        public async Task<IHttpActionResult> DeleteDailyTimeRecord(int? id)
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

        //[HttpGet]
        //public async Task<IHttpActionResult>  
    }
}
