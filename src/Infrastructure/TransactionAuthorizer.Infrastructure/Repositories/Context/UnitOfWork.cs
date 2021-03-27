using Microsoft.EntityFrameworkCore;
using TransactionAuthorizer.Domain.Interfaces.Repositories;

namespace TransactionAuthorizer.Infrastructure.Repositories.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        TransactionAuthorizerContext _context;

        public UnitOfWork(TransactionAuthorizerContext context)
        {
            _context = context;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}