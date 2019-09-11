using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Engineer.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UniversityId { get; set; }
        public ICollection<OrderItem> Products { get; set; }
    }
}