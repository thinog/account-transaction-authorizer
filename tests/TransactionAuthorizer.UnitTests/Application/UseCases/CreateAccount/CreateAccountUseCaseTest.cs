using Moq;
using TransactionAuthorizer.Application.Models;
using TransactionAuthorizer.Application.UseCases.CreateAccount;
using TransactionAuthorizer.Domain.Entities;
using TransactionAuthorizer.Domain.Interfaces.Repositories;
using TransactionAuthorizer.Infrastructure.Repositories;
using TransactionAuthorizer.UnitTests.Infrastructure.IoC;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Application.UseCases.CreateAccount
{
    public class CreateAccountUseCaseTest
    {
        [Fact]
        public void Should_CreateAccount_When_ReceiveAValidAccountAndAccountIsNotInitialized()
        {
            // Arrange
            var account = new Account
            {
                Active = true,
                Limit = 10
            };

            var input = new CreateAccountInput { Account = new AccountDetailsModel(account) };

            var accountRepositoryMock = new Mock<IAccountRepository>();
            accountRepositoryMock.Setup(ar => ar.GetAccount()).Returns((Account)null);

            var useCase = new CreateAccountUseCase(
                new UnitOfWorkFaker(),
                accountRepositoryMock.Object);

            // Act
            var output = (CreateAccountDefaultOutput)useCase.Execute(input);

            // Assert
            Assert.NotNull(output.Account);
            Assert.Empty(output.Account.Violations);
        }

        [Fact]
        public void Should_NotCreateAccount_When_ReceiveAValidAccountAndAccountIsInitialized()
        {
            // Arrange
            var account = new Account
            {
                Active = true,
                Limit = 10
            };

            var input = new CreateAccountInput { Account = new AccountDetailsModel(account) };

            var useCase = new CreateAccountUseCase(
                new UnitOfWorkFaker(),
                new AccountRepositoryFaker());

            // Act
            var output = (CreateAccountDefaultOutput)useCase.Execute(input);

            // Assert
            Assert.NotNull(output.Account);
            Assert.NotEmpty(output.Account.Violations);
        }

        [Fact]
        public void Should_CallCustomOutputPortMethods_When_OutputPortIsSet()
        {
            // Arrange
            var account = new Account
            {
                Active = true,
                Limit = 10
            };

            var input = new CreateAccountInput { Account = new AccountDetailsModel(account) };

            var customOutput = new CreateAccountOutputFaker();

            var useCase = new CreateAccountUseCase(
                new UnitOfWorkFaker(),
                new AccountRepositoryFaker());

            // Act
            useCase.SetOutputPort(customOutput);
            var output = (CreateAccountOutputFaker)useCase.Execute(input);

            // Assert
            Assert.True(output.AccountAlreadyInitializedCalled);
            Assert.True(output.FillCalled);
        }
    }
}