using TransactionAuthorizer.Domain.Interfaces.Repositories;
using TransactionAuthorizer.Infrastructure.IoC;
using TransactionAuthorizer.Infrastructure.Repositories;
using TransactionAuthorizer.Infrastructure.Repositories.Context;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Infrastructure.IoC
{
    public class DependencyResolverTest
    {
        [Fact]
        public void Should_InjectDefaultDependencies_When_ResolveMethodIsCalled()
        {
            // Arrange
            string expectedProvider = "InMemory";

            // Act
            var service = DependencyResolver.Resolve();
            var unitOfWork = (IUnitOfWork)service.GetService(typeof(IUnitOfWork));
            var accountRepository = (IAccountRepository)service.GetService(typeof(IAccountRepository));
            var transactionRepository = (ITransactionRepository)service.GetService(typeof(ITransactionRepository));
            var dbContext = (TransactionAuthorizerContext)service.GetService(typeof(TransactionAuthorizerContext));

            // Assert
            Assert.IsType<UnitOfWork>(unitOfWork);
            Assert.IsType<AccountRepository>(accountRepository);
            Assert.IsType<TransactionRepository>(transactionRepository);
            Assert.Contains(expectedProvider, dbContext.Database.ProviderName);
        }

        [Fact]
        public void Should_InjectCustomClasses_When_ReceiveAValidResolverCofiguration()
        {
            // Arrange
            string expectedProvider = "InMemory";
            var resolver = new ResolverConfigurationFaker
            {
                IsTest = true,
                UnitOfWork = typeof(UnitOfWorkFaker),
                AccountRepository = typeof(AccountRepositoryFaker),
                TransactionRepository = typeof(TransactionRepositoryFaker)
            };

            // Act
            var service = DependencyResolver.Resolve(resolver);
            var unitOfWork = (IUnitOfWork)service.GetService(typeof(IUnitOfWork));
            var accountRepository = (IAccountRepository)service.GetService(typeof(IAccountRepository));
            var transactionRepository = (ITransactionRepository)service.GetService(typeof(ITransactionRepository));
            var dbContext = (TransactionAuthorizerContext)service.GetService(typeof(TransactionAuthorizerContext));

            // Assert            
            Assert.IsType<UnitOfWorkFaker>(unitOfWork);
            Assert.IsType<AccountRepositoryFaker>(accountRepository);
            Assert.IsType<TransactionRepositoryFaker>(transactionRepository);
            Assert.Contains(expectedProvider, dbContext.Database.ProviderName);            
        }
    }
}