using System;
using System.ComponentModel.DataAnnotations;

namespace Engineer.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public Order Order { get; set; }
    }
}