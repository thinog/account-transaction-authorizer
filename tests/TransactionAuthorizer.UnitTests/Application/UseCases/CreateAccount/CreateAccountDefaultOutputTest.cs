using TransactionAuthorizer.Application.Enums;
using TransactionAuthorizer.Application.Models;
using TransactionAuthorizer.Application.UseCases.CreateAccount;
using TransactionAuthorizer.Domain.Entities;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Application.UseCases.CreateAccount
{
    public class CreateAccountDefaultOutputTest
    {
        [Fact]
        public void Should_AddViolations_When_ViolationMethodsAreCalled()
        {
            // Arrange
            var outputPort = new CreateAccountDefaultOutput();

            // Act
            outputPort.AccountAlreadyInitialized();

            // Assert
            Assert.True(outputPort.HasErrors);
            Assert.Contains(Violation.AccountAlreadyInitialized, outputPort.Account.Violations);
        }

        [Fact]
        public void Should_FillAccountObject_When_FillMethodIsCalled()
        {
            // Arrange
            var outputPort = new CreateAccountDefaultOutput();
            var account = new Account { Active = true, Limit = 100 };
            var accountDetails = new AccountDetailsModel(account);

            // Act
            outputPort.Fill(accountDetails);

            // Assert
            Assert.Equal(account.Active, outputPort.Account.AccountDetails.ActiveCard);
            Assert.Equal(account.Limit, outputPort.Account.AccountDetails.AvailableLimit);
        }

        [Fact]
        public void Should_ReturnAnValidJson_When_ToStringMethodIsCalled()
        {
            // Arrange
            var outputPort = new CreateAccountDefaultOutput();
            var account = new Account { Active = true, Limit = 100 };
            var accountDetails = new AccountDetailsModel(account);
            string expectedJson = $"{{\"account\":{{\"active-card\":{account.Active.ToString().ToLower()},\"available-limit\":{account.Limit}}},\"violations\":[]}}";

            // Act
            outputPort.Fill(accountDetails);
            string json = outputPort.ToString();

            // Assert
            Assert.Equal(expectedJson, json);
        }
    }
}