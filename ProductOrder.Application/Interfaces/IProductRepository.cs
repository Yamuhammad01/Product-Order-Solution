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
       // void Add(Product product);
    }
}
