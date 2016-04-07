using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;

namespace OG_MFTG.HR_WebApi.Controllers
{
    [RoutePrefix("hrdapi/employeeschedule")]
    public class EmployeeScheduleController : ApiController
    {
        private readonly IEmployeeScheduleRepository _repository;

        public EmployeeScheduleController(IEmployeeScheduleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<EmployeeSchedule>> GetlAllEmplo()
        {
            return await _repository.SelectAll();
        }
        
        //public async  Task<IHttpActionResult>  
    }
}
