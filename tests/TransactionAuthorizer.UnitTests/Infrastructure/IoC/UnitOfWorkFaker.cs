using TransactionAuthorizer.Domain.Interfaces.Repositories;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Infrastructure.IoC
{
    public class UnitOfWorkFaker : IUnitOfWork
    {
        public int Save()
        {
            return 0;
        }
    }
}