using TransactionAuthorizer.Domain.Entities;
using TransactionAuthorizer.Domain.Interfaces.Repositories;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Infrastructure.IoC
{
    public class AccountRepositoryFaker : IAccountRepository
    {
        public Account GetAccount()
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Account entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Account account)
        {
            throw new System.NotImplementedException();
        }
    }
}