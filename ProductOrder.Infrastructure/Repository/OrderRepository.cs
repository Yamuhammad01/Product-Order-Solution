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
    public class OrderRepository : IOrderRepository 
    {
        private readonly ProductOrderDbContext _dbContext;
        public OrderRepository(ProductOrderDbContext productOrderDbContext) 
        {
            _dbContext = productOrderDbContext;
        }

        public async Task AddAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }
    }
}
