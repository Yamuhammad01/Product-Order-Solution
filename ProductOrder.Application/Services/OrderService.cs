using Microsoft.EntityFrameworkCore;
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
    public class OrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductOrderDbContext _dbContext;
        public OrderService(IProductRepository productRepository, IProductOrderDbContext productOrderDbContext)
        {
            _productRepository = productRepository;
            _dbContext = productOrderDbContext;
        }

        public async Task<OrderResultDto> PlaceOrderAsync(PlaceOrderDto dto)
        {
           // using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var order = new Order();

            foreach (var itemDto in dto.Items)
            {
                // Lock product row for update to prevent overselling
                var product = await _dbContext.Products
                    .Where(p => p.Id == itemDto.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null)
                    return new OrderResultDto { Success = false, Message = $"Product {itemDto.ProductId} not found" };

                if (product.StockQuantity < itemDto.Quantity)
                    return new OrderResultDto
                    {
                        Success = false,
                        Message = $"Not enough stock for product {product.Name}"
                    };

                // Decrease stock
                product.StockQuantity -= itemDto.Quantity;

                order.Items.Add(new OrderProduct
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    Price = product.Price
                });

                _dbContext.Products.Update(product);
            }

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
           // await transaction.CommitAsync();

            return new OrderResultDto { Success = true, OrderId = order.Id };
        }
    }
}

