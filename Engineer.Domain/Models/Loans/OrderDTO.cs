using System;
using System.Collections.Generic;
using Engineer.Domain.Entities;
using Engineer.Domain.Enums;

namespace Engineer.Domain.Models.Loans
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UniversityId { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderItemDTO> Products { get; set; }
    }
}