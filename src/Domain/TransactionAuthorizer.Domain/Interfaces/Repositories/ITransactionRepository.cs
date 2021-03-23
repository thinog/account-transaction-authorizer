using System.Collections.Generic;
using TransactionAuthorizer.Domain.Entities;

namespace TransactionAuthorizer.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IEnumerable<Transaction> GetTransactionsByTime(int minutes);
    }
}