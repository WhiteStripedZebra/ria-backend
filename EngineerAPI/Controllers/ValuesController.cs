using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Engineer.Domain.Authorization;
using Engineer.Domain.Entities;
using Engineer.Domain.Models.Todo;
using Engineer.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Engineer.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly IMapper _mapper;

        public ValuesController(ITodoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of Tasks
        /// </summary>
        /// <returns>A list of Tasks</returns>
        /// <response code="200">Returns list of all Tasks </response>
        /// <response code="404">Items not found</response>
        [HttpGet]
        [Authorize(Policy = nameof(Policies.BoardMember))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ToDo[]>> GetTasks()
        {
            var taskEntities = await _repository.GetTasksAsync();

            if (taskEntities == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ToDoDTO>>(taskEntities));
        }


        [HttpGet("claims")]
        public async Task<IEnumerable<Claim>> GetClaims()
        {
            return HttpContext.User.Claims;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetToDo(Guid id)
        {
            var taskEntity = await _repository.GetTaskAsync(id);

            if (taskEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ToDoDTO>(taskEntity));
        }


        [HttpPost]
        public async Task<IActionResult> CreateToDo([FromBody] CreateToDoDTO todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<ToDo>(todo);

            _repository.AddToDo(entity);

            await _repository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDo), new {id = entity.Id}, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDo(Guid id, [FromBody] CreateToDoDTO todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entity = await _repository.GetTaskAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var updateEntity = _mapper.Map<ToDo>(todo);

            entity.IsCompleted = updateEntity.IsCompleted;
            entity.Task = updateEntity.Task;

            await _repository.UpdateTask(entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _repository.DeleteTask(id);

            return Ok();
        }
    }
}