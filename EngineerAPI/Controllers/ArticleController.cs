using AutoMapper;
using Engineer.Domain.Entities;
using Engineer.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engineer.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;

        public ArticleController(IArticleRepository repository, IMapper mapper)
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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Article[]>> GetAllArticles()
        {
            var articleEntities = await _repository.GetArticlesAsync();

            if (articleEntities == null)
            {
                return NotFound();
            }

            return Ok(articleEntities); 
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetArticle(Guid id)
        {
            var articleEntity = await _repository.GetArticleAsync(id);

            if (articleEntity == null)
            {
                return NotFound();
            }

            return Ok(articleEntity);
        }
    }

}
