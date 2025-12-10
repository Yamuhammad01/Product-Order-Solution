using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrder.Application.DTOs
{
    public class OrderResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? OrderId { get; set; }

        public static OrderResultDto Successful(int orderId)
            => new() { Success = true, OrderId = orderId };

        public static OrderResultDto Failed(string message)
            => new() { Success = false, Message = message };
    }
}
