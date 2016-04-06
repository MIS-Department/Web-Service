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
    [RoutePrefix("hrdapi/dailytimerecord")]
    public class DailyTimeRecordController : ApiController
    {
        private readonly IDailyTimeRecordRepository _repository;

        public DailyTimeRecordController(IDailyTimeRecordRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<DailyTimeRecord>> GetAllDailyTimeRecord()
        {
            return await _repository.SelectAll();
        }

        [HttpGet]
        [Route("{id:int}")]
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
        [Route("")]
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
        [Route("")]
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
        [Route("{id:int}")]
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

        [HttpGet]
        [Route("{id:int}/employee")]
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> GetByEmployeeId(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = await _repository.SelectByEmployeeId(id);
            return Ok(result);
        }


        //[ResponseType(typeof(IEnumerable<DailyTimeRecord>))]
        //[Route("{id:int}/startdate/{startdate:datetime}/enddate/{enddate:datetime}")]
        //public async Task<IEnumerable<DailyTimeRecord>> GetByEmployeeIdDateCreated(int? id, DateTime startDate,
        //    DateTime endTime)
        //{  

        //    if (id == null)
        //    {
        //        return
        //    }    

        //}
    }
}
