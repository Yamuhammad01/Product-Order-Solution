using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductOrder.Domain.Entities;

namespace ProductOrder.Application.Interfaces
{
    public interface IProductRepository
    {
        Task <Product> AddProductAsync(Product product);
        Task<Product?> UpdateProductAsync(Product product); // update a product interface
        Task<Product?> GetProductByIdAsync(Guid id); // get a product by id 
        Task<IEnumerable<Product>> GetAllProductsAsync(); // return a list of all products 
        Task<bool> DeleteProductAsync(Guid id);
        // void Add(Product product);
    }
}
