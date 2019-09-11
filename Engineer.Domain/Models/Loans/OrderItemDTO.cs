using System;
using Engineer.Domain.Entities;

namespace Engineer.Domain.Models.Loans
{
    public class OrderItemDTO
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }

    }
}