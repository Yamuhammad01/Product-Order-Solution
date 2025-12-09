using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductOrder.Application.DTOs;
using ProductOrder.Application.Interfaces;
using ProductOrder.Application.Services;

namespace ProductOrder.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly OrderService _orderService;

        public ProductsController(ProductService productService, OrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }
        // add a new product 
        [HttpPost("add-product")]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.AddProductAsync(dto);

            return Ok(product);
        }

        // return all products 
        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // get a product by id
        [HttpGet("get-a-product-by-id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(product);
        }

        // 
        // Update a product 
        [HttpPut("update-a-product-by-id")] // changed guid to int for easier access in swagger UI
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            var updated = await _productService.UpdateProductAsync(id, dto);

            if (updated == null)
                return NotFound(new { message = "Product not found" });

            return Ok(updated);
        }
        // DELETE
        [HttpDelete("delete-a-product-by-id")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);

            if (!deleted)
                return NotFound(new { message = "Product not found" });

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                return BadRequest(new { message = "No items in order" });

            var result = await _orderService.PlaceOrderAsync(dto);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { orderId = result.OrderId });
        }
    }
}
