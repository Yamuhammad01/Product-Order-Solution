using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrder.Application.DTOs
{
    public class PlaceOrderDto
    {
        public List<OrderItemDto> Items { get; set; } = new();
    }
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
