using TransactionAuthorizer.Domain.Entities;
using TransactionAuthorizer.Domain.Interfaces.Repositories;

namespace TransactionAuthorizer.UnitTests.Infrastructure.IoC
{
    public class AccountRepositoryFaker : IAccountRepository
    {
        public Account GetAccount()
        {
            return new Account { Limit = 100, Active = true };
        }

        public void Insert(Account entity)
        {
            
        }

        public void Update(Account account)
        {
            
        }
    }
}