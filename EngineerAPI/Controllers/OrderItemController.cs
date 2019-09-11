using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Engineer.Domain.Entities;
using Engineer.Domain.Models.Loans;
using Engineer.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Engineer.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/orders/{orderid}/items")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<OrderItemController> _log;
        private readonly IMapper _mapper;

        public OrderItemController(IOrderRepository repository, ILogger<OrderItemController> log, IMapper mapper)
        {
            _repository = repository;
            _log = log;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<OrderItemDTO[]>> GetOrderItems(Guid orderid)
        {
            try
            {
                var orderEntity = await _repository.GetOrderAsync(orderid);

                if (orderEntity == null)
                {
                    return NotFound();
                }

                return Ok(
                    _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(orderEntity
                        .Products));
            }
            catch (Exception ex)
            {
                _log.LogError($"Failed to get order items: {ex}");
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItem(Guid orderId, Guid id)
        {
            try
            {
                var orderEntity = await _repository.GetOrderAsync(orderId);

                if (orderEntity == null)
                {
                    return NotFound();
                }

                var orderItem = orderEntity.Products.FirstOrDefault(o => o.Id == id);

                if (orderItem == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<OrderItem, OrderItemDTO>(orderItem));
            }
            catch (Exception ex)
            {
                _log.LogError($"Failed to get item from order: {ex}");
                return BadRequest();
            }
        }
 
    }
}