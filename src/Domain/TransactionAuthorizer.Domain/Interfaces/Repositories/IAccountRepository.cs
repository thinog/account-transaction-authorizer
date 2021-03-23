using TransactionAuthorizer.Domain.Entities;

namespace TransactionAuthorizer.Domain.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetAccount();
    }
}