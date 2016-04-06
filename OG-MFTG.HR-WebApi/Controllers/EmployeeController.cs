using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;

namespace OG_MFTG.HR_WebApi.Controllers
{
    [RoutePrefix("hrdapi/employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Employee>> SelectAll()
        {
            return await _repository.SelectAll();
        }

        [Route("{id:int}")]
        [HttpGet]
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> SelectById(int? id)
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

        [Route("{number}/employeenumber")]
        [HttpGet]
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> SelectByNumber(string number)
        {
            if (number == null)
            {
                return BadRequest();

            }
            var model = await _repository.SelectbyEmployeeNumber(number);

            if(model== null)
            {
                return NotFound();
            }
            return Ok(model);
        } 
    }
}
