using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Engineer.Domain.Entities;
using Engineer.Domain.Models.Loans;
using Engineer.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Engineer.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductsController> _log;
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> log, IProductRepository repository, IMapper mapper)
        {
            _log = log;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Product[]>> GetProducts()
        {
            try
            {
                var productEntities = await _repository.GetProductsAsync();

                if (productEntities == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(productEntities));
            }
            catch (Exception ex)
            {
                _log.LogError($"Failed to get products: {ex}");
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            try
            {
                var productEntity = await _repository.GetProductAsync(id);

                if (productEntity == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<Product, ProductDTO>(productEntity));
            }
            catch (Exception ex)
            {
                _log.LogError($"Failed to get product: {ex}");
                return BadRequest();
            }
        }


    }
}