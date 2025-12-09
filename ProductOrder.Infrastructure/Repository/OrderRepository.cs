using Microsoft.EntityFrameworkCore;
using ProductOrder.Domain.Entities;
using ProductOrder.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrder.Infrastructure.Repository
{
    public class OrderRepository
    {
        private readonly ProductOrderDbContext _dbContext;
        public OrderRepository(ProductOrderDbContext productOrderDbContext) 
        {
            _dbContext = productOrderDbContext;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }
    }
}
