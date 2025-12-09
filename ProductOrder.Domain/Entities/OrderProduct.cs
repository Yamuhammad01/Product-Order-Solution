using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrder.Domain.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; } // price at time of order
        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;
    }
}
