using Microsoft.EntityFrameworkCore;
using ProductOrder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrder.Application.Interfaces
{
    public interface IProductOrderDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<Order> Orders { get; set; }

        Task SaveChangesAsync();
    }
}
