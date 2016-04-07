using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;

namespace OG_MFTG.HR_WebApi.Controllers
{
    [RoutePrefix("hrdapi/tempalte")]
    public class TemplateController : ApiController
    {
        private readonly ITemplateRepository _repository;

        public TemplateController(ITemplateRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Template
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Template>> GetAllTemplate()
        {
            return await _repository.SelectAll();
        }

        // GET: api/Template/5
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(Template))]
        public async Task<IHttpActionResult> GetTemplate(int? id)
        {
            if (id == null)
            {
                return BadRequest("TempalteId is null");
            }
            var model = await _repository.SelectById(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        // POST: api/Template
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Template/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Template/5
        public void Delete(int id)
        {
        }
    }
}
