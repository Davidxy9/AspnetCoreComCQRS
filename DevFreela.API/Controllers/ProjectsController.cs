using DevFreela.Application.Commands.CompleteProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.InsertComment;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/projects?search=crm
        [HttpGet]
        public async Task<IActionResult> Get(string search = "")
        {
            var query = new GetAllProjectsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        // GET api/projects/1234
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // POST api/projects
        [HttpPost]
        public async Task<IActionResult> Post(InsertProjectCommand command)
        {
            var result = await _mediator.Send(command);

            if(!result.IsSuccess) return BadRequest(result.Message);

             return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data }, command);
        }

        // PUT api/projects/1234
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        //  DELETE api/projects/1234
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // PUT api/projects/1234/start
        [HttpPut("{id}/start")]
        public async Task<IActionResult> StartAsync(int id)
        {
            var result = await _mediator.Send(new StartProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // PUT api/projects/1234/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteAsync(int id)
        {
            var result = await _mediator.Send(new CompleteProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // POST api/projects/1234/comments
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostCommentAsync(int id, InsertCommentCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
