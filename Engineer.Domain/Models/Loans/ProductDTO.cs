using System;
using System.Collections.Generic;
using System.Text;

namespace Engineer.Domain.Models.Loans
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
