using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductOrder.Application.DTOs;
using ProductOrder.Application.Services;

namespace ProductOrder.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
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
        [HttpGet("(get-a-product-by-id)")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(product);
        }

        // 
        // Update a product 
        [HttpPut("update-a-product-by-id")] // i used guid all through for security purposes 
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto dto)
        {
            var updated = await _productService.UpdateProductAsync(id, dto);

            if (updated == null)
                return NotFound(new { message = "Product not found" });

            return Ok(updated);
        }
        // DELETE
        [HttpDelete("delete-a-product-by-id")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var deleted = await _productService.DeleteProductAsync(id);

            if (!deleted)
                return NotFound(new { message = "Product not found" });

            return NoContent();
        }
    }
}
