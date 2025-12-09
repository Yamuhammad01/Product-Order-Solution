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
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.AddProductAsync(dto);

            return Ok(product);
        }

    }
}
