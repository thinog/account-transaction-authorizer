using TransactionAuthorizer.Domain.Entities;
using TransactionAuthorizer.Domain.Interfaces.Repositories;

namespace TransactionAuthorizer.Infrastructure.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public Account GetAccount()
        {
            throw new System.NotImplementedException();
        }
    }
}