using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Engineer.Application.Services.Mail;
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
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _log;
        private readonly IMailService _mailService;

        public OrdersController(IOrderRepository repository, IMapper mapper, ILogger<OrdersController> log, IMailService mailService)
        {
            _repository = repository;
            _mapper = mapper;
            _log = log;
            _mailService = mailService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order[]>> GetOrders()
        {
            try
            {
                var orderEntities = await _repository.GetOrdersAsync();

                if (orderEntities == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orderEntities));
            }
            catch (Exception ex)
            {
                _log.LogError($"Failed to get orders: {ex}");
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            try
            {
                var orderEntity = await _repository.GetOrderAsync(id);

                if (orderEntity == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<Order, OrderDTO>(orderEntity));
            }
            catch (Exception ex)
            {
                _log.LogError($"Failed to get order: {ex}");
                return BadRequest();
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderDTO order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest();
                }

                var entity = _mapper.Map<OrderDTO, Order>(order);

                _repository.AddOrder(entity);

                await _repository.SaveChangesAsync();

                await _mailService.SendNewOrderMailAsync(order.Email, entity);

                return CreatedAtAction(nameof(AddOrder), new {id = entity.Id}, entity);
            }
            catch (Exception ex)
            {
                _log.LogError($"Failed to add new order: {ex}");
                return BadRequest();
            }
        }
    }
}

