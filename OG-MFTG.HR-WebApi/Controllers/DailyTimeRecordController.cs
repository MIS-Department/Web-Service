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
        public async Task<EmployeeNotify> GetEmployeeDetails([FromUri]string employeeNumber, [FromUri] int? timeCategoryId)
        {
            EmployeeNotify employeeNotif;
            if (employeeNumber == null || timeCategoryId == null)
            {
                var error = new ErrorReturn
                {
                    Code = "Error",
                    Message = "EmployeeNumber or TimeCategoryId is not set"
                };
                employeeNotif = new EmployeeNotify
                {
                    Error = error
                };
                return employeeNotif;
            }

            int? employeeId = await _dailyTimeRepo.SelectByEmployeeNumber(employeeNumber);

            

            if (employeeId == null)
            {
                var error = new ErrorReturn
                {  
                    Code = "Error",
                    Message = "Employee Number do not exist"
                };
                employeeNotif = new EmployeeNotify
                {
                    Error = error
                };
                return employeeNotif;
            }


            var notify = await _dailyTimeRepo.GetEmplopyeeNotification(employeeId, timeCategoryId);

            employeeNotif = new EmployeeNotify
            {
                IsSuspended = notify.IsSuspended,
                IsResign =   notify.IsResign,
                IsTimeCheck = notify.IsTimeCheck,
                Employee = await _employeeRepo.SelectById(employeeId),
                DailyTimeRecord = await _dailyTimeRepo.GetDailyTimeRecordTopFive(employeeId)
            };


            if (employeeNotif.IsSuspended || employeeNotif.IsTimeCheck || employeeNotif.IsResign)
            {
                return employeeNotif;
            }

            var model = new DailyTimeRecord
            {
                DateCreated = DateTime.Now,
                EmployeeId = employeeId,
                TimeCategoryId = timeCategoryId,
                Time = DateTime.Now

            };

            await _dailyTimeRepo.Insert(model);

            employeeNotif.DailyTimeRecord = await _dailyTimeRepo.GetDailyTimeRecordTopFive(employeeId);
            return employeeNotif;
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
