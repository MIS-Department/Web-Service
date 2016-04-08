using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;
using OG_MFTG.DataLayer.Repositories;
using OG_MFTG.Models.DTO;

namespace OG_MFTG.HR_WebApi.Controllers
{
    [RoutePrefix("hrdapi/dailytimerecord")]
    public class DailyTimeRecordController : ApiController
    {
        private readonly IDailyTimeRecordRepository _dailyTimeRepo;
        private readonly IEmployeeRepository _employeeRepo;

        public DailyTimeRecordController(IDailyTimeRecordRepository dailyTimeRepo, EmployeeRepository employeeRepo)
        {
            _dailyTimeRepo = dailyTimeRepo;
            _employeeRepo = employeeRepo;
        }

        [Route("")]
        [HttpGet]
        [ResponseType(typeof(EmployeeNotify))]
        public async Task<IHttpActionResult> GetEmployeeDetails([FromUri]string employeeNumber,[FromUri] int? timeCategoryId)
        {
            if (employeeNumber == null || timeCategoryId == null)
            {
                return BadRequest("employeeNumber or timeCategoryId is null");
            }

            int? id = await _dailyTimeRepo.SelectByEmployeeNumber(employeeNumber);

            if (id == null)
            {
                return NotFound();
            }

            var employeeNotif = new EmployeeNotify
            {
                IsNotify = await _dailyTimeRepo.GetEmplopyeeNotification(id),
                Employee = await _employeeRepo.SelectById(id)
            };


            if (employeeNotif.IsNotify)
            {   
                return Ok(employeeNotif);
            }

            var model = new DailyTimeRecord
            {
                DateCreated = DateTime.Now,
                EmployeeId = id,
                TimeCategoryId = timeCategoryId

            };
            await _dailyTimeRepo.Insert(model);
            //var result = await _employeeRepo.SelectById(id);
            return Ok(employeeNotif);
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<DailyTimeRecord>> GetAllDailyTimeRecord()
        {
            return await _dailyTimeRepo.SelectAll();
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
            var model = await _dailyTimeRepo.SelectById(id);
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
            var id = await _dailyTimeRepo.Insert(model);
            string uri = Url.Link("DefaultApi", new { id });
            return Created(uri, model.DailyTimeRecordId = id);
        }

        [HttpPut]
        [Route("")]
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> PutDailyTimeRecord(DailyTimeRecord model)
        {
            var result = await _dailyTimeRepo.SelectById(model.DailyTimeRecordId);
            if (result == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dailyTimeRepo.Update(model);
            return Ok(model);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> DeleteDailyTimeRecord(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = await _dailyTimeRepo.SelectById(id);
            if (model == null)
            {
                return NotFound();
            }
            await _dailyTimeRepo.Delete(id);
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
            var result = await _dailyTimeRepo.SelectByEmployeeId(id);
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
