using Microsoft.EntityFrameworkCore;
using TransactionAuthorizer.Domain.Interfaces.Repositories;

namespace TransactionAuthorizer.Infrastructure.Repositories.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}