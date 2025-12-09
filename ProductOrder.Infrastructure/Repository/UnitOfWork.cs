using Microsoft.EntityFrameworkCore.Storage;
using ProductOrder.Application.Interfaces;
using ProductOrder.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrder.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductOrderDbContext _dbContext;
        private IDbContextTransaction? _tx;
        public UnitOfWork(ProductOrderDbContext db)
        {
            _dbContext = db;
        }

        public async Task BeginTransactionAsync()
        {
            _tx = await _dbContext.Database.BeginTransactionAsync(); // start a db transaction to ensure atomicity
        }

        public async Task CommitAsync()
        {
            if (_tx != null)
            {
                await _dbContext.SaveChangesAsync();
                await _tx.CommitAsync(); _tx.Dispose(); _tx = null;
            }
        }
        public async Task RollbackAsync()
        {
            if (_tx != null)
            {
                await _tx.RollbackAsync(); _tx.Dispose(); _tx = null;
            }
        }
        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
