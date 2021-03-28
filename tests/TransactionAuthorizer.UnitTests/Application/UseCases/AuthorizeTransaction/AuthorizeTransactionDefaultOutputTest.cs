using TransactionAuthorizer.Application.Enums;
using TransactionAuthorizer.Application.Models;
using TransactionAuthorizer.Application.UseCases.AuthorizeTransaction;
using TransactionAuthorizer.Domain.Entities;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Application.UseCases.AuthorizeTransaction
{
    public class AuthorizeTransactionDefaultOutputTest
    {
        [Fact]
        public void Should_AddViolations_When_ViolationMethodsAreCalled()
        {
            // Arrange
            var outputPort = new AuthorizeTransactionDefaultOutput();

            // Act
            outputPort.AccountNotInitialized();
            outputPort.CardNotActive();
            outputPort.DoubledTransaction();
            outputPort.HighFrequencySmallInterval();
            outputPort.InsufficientLimit();

            // Assert
            Assert.True(outputPort.HasErrors);
            Assert.Contains(Violation.AccountNotInitialized, outputPort.Account.Violations);
            Assert.Contains(Violation.CardNotActive, outputPort.Account.Violations);
            Assert.Contains(Violation.DoubledTransaction, outputPort.Account.Violations);
            Assert.Contains(Violation.HighFrequencySmallInterval, outputPort.Account.Violations);
            Assert.Contains(Violation.InsuficientLimit, outputPort.Account.Violations);
        }

        [Fact]
        public void Should_FillAccountObject_When_FillMethodIsCalled()
        {
            // Arrange
            var outputPort = new AuthorizeTransactionDefaultOutput();
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
            var outputPort = new AuthorizeTransactionDefaultOutput();
            var account = new Account { Active = true, Limit = 100 };
            var accountDetails = new AccountDetailsModel(account);
            string expectedJson = $"{{\"account\":{{\"active-card\":{account.Active.ToString().ToLower()},\"available-limit\":{account.Limit}}},\"violations\":[\"{Violation.CardNotActive}\"]}}";

            // Act
            outputPort.Fill(accountDetails);
            outputPort.CardNotActive();
            string json = outputPort.ToString();

            // Assert
            Assert.Equal(expectedJson, json);
        }
    }
}