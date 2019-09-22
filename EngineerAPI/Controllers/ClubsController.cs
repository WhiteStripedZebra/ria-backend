using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineer.Domain.Entities;
using Engineer.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Engineer.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : Controller
    {
        private readonly IClubRepository _repository;
        private readonly ILogger<ClubsController> _log;

        public ClubsController(IClubRepository repository, ILogger<ClubsController> log)
        {
            _repository = repository;
            _log = log;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Club[]>> GetClubs()
        {
            try
            {
                var clubEntities = await _repository.GetClubsAsync();

                if (clubEntities == null)
                {
                    return NotFound();
                }

                return Ok(clubEntities);
            }
            catch (Exception ex)
            {
                _log.LogError($"Failed to get clubs: {ex}");
                return BadRequest();
            }
        }
    }
}
