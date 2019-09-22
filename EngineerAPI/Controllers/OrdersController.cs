using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Engineer.Application.Services.Calendar;
using Engineer.Application.Services.Mail;
using Engineer.Application.Utilities;
using Engineer.Domain.Entities;
using Engineer.Domain.Enums;
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
        private readonly IGoogleCalendarService _googleCalendar;

        public OrdersController(IOrderRepository repository, IMapper mapper, ILogger<OrdersController> log, IMailService mailService, IGoogleCalendarService googleCalendar)
        {
            _repository = repository;
            _mapper = mapper;
            _log = log;
            _mailService = mailService;
            _googleCalendar = googleCalendar;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order[]>> GetOrders(int page = 0, int pageSize = 5)
        {
            try
            {
                var orderEntities = await _repository.GetOrdersAsync();

                if (orderEntities == null)
                {
                    return NotFound();
                }

                var orders = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orderEntities);

                return Ok(orders.Skip(page * pageSize).Take(pageSize));
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

            // TO DO: Check if order is coming from a member in database - else delete it

            try
            {
                if (order == null)
                {
                    return BadRequest();
                }

                var entity = _mapper.Map<OrderDTO, Order>(order);

                _repository.AddOrder(entity);

                await _repository.SaveChangesAsync();

                _mailService.SendNewOrderMailAsync(order.Email, entity);

                return CreatedAtAction(nameof(AddOrder), new {id = entity.Id}, entity);
            }
            catch (Exception ex)
            {
                _log.LogError($"Failed to add new order: {ex}");
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] OrderStatusDTO order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entity = await _repository.GetOrderAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.Status = order.status;

            if (order.status == OrderStatus.Approved)
            {
                _googleCalendar.CreateLoanOrder(_mapper.Map<Order, OrderDTO>(entity));
            }

            await _repository.UpdateOrder(entity);

            return NoContent();
        }
    }
}

