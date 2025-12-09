using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductOrder.Domain.Entities;

namespace ProductOrder.Infrastructure.Persistence
{
    public class ProductOrderDbContext : DbContext
    {
        public ProductOrderDbContext (DbContextOptions<ProductOrderDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
