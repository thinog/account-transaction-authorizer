using System;
using System.Collections.Generic;
using TransactionAuthorizer.Domain.Entities;
using TransactionAuthorizer.Domain.Interfaces.Repositories;

namespace TransactionAuthorizer.UnitTests.Infrastructure.IoC
{
    public class TransactionRepositoryFaker : ITransactionRepository
    {
        public IEnumerable<Transaction> GetTransactionsByTime(DateTime time)
        {
            return new List<Transaction>();
        }

        public void Insert(Transaction entity)
        {
            
        }
    }
}