using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductOrder.Application.Interfaces;
using ProductOrder.Domain.Entities;
using ProductOrder.Infrastructure.Persistence;

namespace ProductOrder.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductOrderDbContext _dbContext;
        public ProductRepository(ProductOrderDbContext context) 
        {
            _dbContext = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
