using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;
using SharedModels.Fluent.Developer;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly IDeveloperService developerService;

        public DevelopersController(IDeveloperService developerService) => this.developerService = developerService;

        // GET: api/Developers
        [HttpGet]
        public ActionResult<PagedResponse<Developer>> Get(DeveloperSearchRequest request)
        {
            return Ok(developerService.All(request));
        }

        // GET: api/Developers/5
        [HttpGet("{id}")]
        public ActionResult<Developer> Get(int id)
        {
            try
            {
                var developer = developerService.Find(id);
                return Ok(developer);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, please try later.");
            }
        }

        // POST: api/Developers
        [HttpPost]
        public IActionResult Post([FromBody] CreateDeveloperDTO dto)
        {
            try
            {
                //var validator = new DeveloperFluentValidatior();
                //var valid = await validator.ValidateAsync(dto);

                //if (!valid.IsValid)
                //{
                //    return UnprocessableEntity(ValidationFormatter.Format(valid));
                //}
                developerService.Create(dto);
                return Created("/Developers", dto);
            }
            catch (ValidationException e)
            {
                return UnprocessableEntity();
            }
            catch (Exception)
            {

                return StatusCode(500, "Server error, please try later.");
            }
        }

        // PUT: api/Developers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]  CreateDeveloperDTO dto)
        {
            try
            {
                developerService.Update(dto);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {

                return StatusCode(500, "Server error, please try later.");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                developerService.Delete(id);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {

                return StatusCode(500, "Server error, please try later.");
            }
        }
    }
}
