using TransactionAuthorizer.Application.UseCases.CreateAccount;
using TransactionAuthorizer.Domain.Entities;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Application.UseCases.CreateAccount
{
    public class CreateAccountInputTest
    {
        [Fact]
        public void Should_ReturnAnValidAccountObject_When_InputHasInstancedWithAAccount()
        {
            // Arrange
            var account = new Account { Active = true, Limit = 100 };
            var input = new CreateAccountInput(account);

            // Act
            var result = input.ToAccountEntity();

            // Assert
            Assert.Equal(account.Active, result.Active);
            Assert.Equal(account.Limit, result.Limit);
        }

        [Fact]
        public void Should_ReturnAnValidTransactionObject_When_InputHasInstancedWithoutATransaction()
        {
            // Arrange
            var expectedTransaction = new Account { Active = false, Limit = 0 };
            var input = new CreateAccountInput();

            // Act
            var result = input.ToAccountEntity();

            // Assert
            Assert.Equal(expectedTransaction.Active, result.Active);
            Assert.Equal(expectedTransaction.Limit, result.Limit);
        }
    }
}