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
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Order> Orders { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
           => base.SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // OrderProduct (Join Table)
            modelBuilder.Entity<OrderProduct>(entity =>
            {
                // Composite Key
                entity.HasKey(op => new { op.OrderId, op.ProductId });
             
                // Relationship: Order -> OrderProduct (1-Many)
                entity.HasOne(op => op.Order)
                    .WithMany(o => o.Items)
                    .HasForeignKey(op => op.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relationship: Product -> OrderProduct (1-Many)
                entity.HasOne(op => op.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(op => op.ProductId)
                     .OnDelete(DeleteBehavior.Cascade); // for development only, deleting product that has been ordered should be restricted when moving to production
                    //.OnDelete(DeleteBehavior.Restrict); // Prevent deleting product that is in orders
            });

            // Order table configs
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(o => o.Id);
            });

            // Product table  configs
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id);
            });
        }

    }
}
