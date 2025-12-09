using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrder.Application.DTOs
{
    public class PlaceOrderDto
    {
        public List<OrderProductDto> Items { get; set; } = new();
    }
    public class OrderProductDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
