using System.Linq;
using TransactionAuthorizer.Domain.Entities;
using TransactionAuthorizer.Domain.Interfaces.Repositories;
using TransactionAuthorizer.Infrastructure.Repositories.Context;

namespace TransactionAuthorizer.Infrastructure.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(TransactionAuthorizerContext context) : base(context) { }

        public Account GetAccount()
        {
            return _dbSet.FirstOrDefault();
        }

        public void Update(Account account)
        {
            _dbSet.Update(account);
        }
    }
}