using ProductOrder.Application.DTOs;
using ProductOrder.Application.Interfaces;
using ProductOrder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrder.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddProductAsync(AddProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            return await _productRepository.AddProductAsync(product);
        }

        // get a product by an id 
        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        // return a list of all products
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product?> UpdateProductAsync(Guid id, UpdateProductDto dto)
        {
            var existing = await _productRepository.GetProductByIdAsync(id);

            if (existing == null)
                return null;

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.StockQuantity = dto.StockQuantity;

            return await _productRepository.UpdateProductAsync(existing);
        }

        // delete a product 
        public async Task<bool> DeleteProductAsync(Guid id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }
    }
}
