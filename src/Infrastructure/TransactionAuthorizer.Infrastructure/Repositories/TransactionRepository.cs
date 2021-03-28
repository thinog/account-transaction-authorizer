using System;
using System.Collections.Generic;
using System.Linq;
using TransactionAuthorizer.Domain.Entities;
using TransactionAuthorizer.Domain.Interfaces.Repositories;
using TransactionAuthorizer.Infrastructure.Repositories.Context;

namespace TransactionAuthorizer.Infrastructure.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(TransactionAuthorizerContext context) : base(context) { }

        public IEnumerable<Transaction> GetTransactionsByTime(DateTime time)
        {
            return _dbSet.Where(t => t.Time >= time);
        }
    }
}