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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        public async Task<OrderResultDto> PlaceOrderAsync(PlaceOrderDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                return OrderResultDto.Failed("Order must contain at least one item.");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var order = new Order();

                foreach (var item in dto.Items)  // item from the dto
                {
                    // Get product via repository 
                    var product = await _productRepository.GetProductByIdAsync(item.ProductId);

                    if (product == null)
                        return OrderResultDto.Failed($"Product not found.");

                    if (product.StockQuantity < item.Quantity)
                        return OrderResultDto.Failed(
                            $"Not enough stock for product '{product.Name}'."
                        );

                    // Reduce stock
                    product.StockQuantity -= item.Quantity;

                    // Add item to order
                    order.Items.Add(new OrderProduct
                    {
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        Price = product.Price
                    });

                    await _productRepository.UpdateAsync(product); 
                }

                await _orderRepository.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync(); // commit everything together

                return OrderResultDto.Successful(order.Id);
            }
            catch
            {
                await _unitOfWork.RollbackAsync(); // rollback everything if an error occurs mid-operation
                return OrderResultDto.Failed("An error occurred while placing the order.");
            }
        }




    }
}

