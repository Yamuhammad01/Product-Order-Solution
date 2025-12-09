using Microsoft.EntityFrameworkCore;
using ProductOrder.Application.Interfaces;
using ProductOrder.Domain.Entities;
using ProductOrder.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrder.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductOrderDbContext _dbContext;
        public ProductRepository(ProductOrderDbContext context) 
        {
            _dbContext = context;
        }
        // create a new product 
        public async Task<Product> AddProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        // get a product by id
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        // return a list of all products from the db 
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        // update a product method for crud operation
        public async Task<Product?> UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        // product update for orderservice 
        public async Task UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await Task.CompletedTask;
        }

        // find a product by id and delete it
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null) // check if the product is in the db 
                return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
