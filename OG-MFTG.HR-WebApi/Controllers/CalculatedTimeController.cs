﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;

namespace OG_MFTG.HR_WebApi.Controllers
{
    [RoutePrefix("hrdapi/calculatedtime")]
    public class CalculatedTimeController : ApiController
    {
        private readonly ICalculatedTimeRepository _repository;

        public CalculatedTimeController(ICalculatedTimeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<CalculatedTime>> GetAllCalculatedTime()
        {
            return await _repository.SelectAll();
        }

        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(CalculatedTime))]
        public async Task<IHttpActionResult> GetCalculatedTime(int? id)
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
        public async Task<IHttpActionResult> PostCalculatedTime([FromBody]CalculatedTime model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _repository.Insert(model);
            string uri = Url.Link("hrdapi", new { id });
            return Created(uri, model.TimeTypeId = id);
        }

        [HttpPut]
        [Route("")]
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<IHttpActionResult> PutCalculatedTime([FromBody]CalculatedTime model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repository.SelectById(model.CalculatedTimeId);
            if (result == null)
            {
                return NotFound();
            }
            
            await _repository.Update(model);
            return Ok(model);
        }

        [HttpDelete]    
        [ResponseType(typeof(HttpResponseMessage))]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteCalculatedTime(int? id)
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
