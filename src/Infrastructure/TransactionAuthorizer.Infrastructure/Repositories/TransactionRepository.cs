using System.Collections.Generic;
using TransactionAuthorizer.Domain.Entities;
using TransactionAuthorizer.Domain.Interfaces.Repositories;

namespace TransactionAuthorizer.Infrastructure.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public IEnumerable<Transaction> GetTransactionsByTime(int minutes)
        {
            throw new System.NotImplementedException();
        }
    }
}