using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductOrder.Application.DTOs;
using ProductOrder.Application.Services;

namespace ProductOrder.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService) 
        { 
            _orderService = orderService;
        }

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                return BadRequest(new { message = "No items in order" });

            var result = await _orderService.PlaceOrderAsync(dto);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok("Order Placed Successfully");
        }
    }
}
