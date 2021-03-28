using System;
using System.Collections.Generic;
using TransactionAuthorizer.Domain.Entities;
using TransactionAuthorizer.Domain.Interfaces.Repositories;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Infrastructure.IoC
{
    public class TransactionRepositoryFaker : ITransactionRepository
    {
        public IEnumerable<Transaction> GetTransactionsByTime(DateTime time)
        {
            throw new NotImplementedException();
        }

        public void Insert(Transaction entity)
        {
            throw new NotImplementedException();
        }
    }
}