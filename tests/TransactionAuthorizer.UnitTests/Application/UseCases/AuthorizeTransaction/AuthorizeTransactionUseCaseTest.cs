using System;
using TransactionAuthorizer.Application.Enums;
using TransactionAuthorizer.Application.Models;
using TransactionAuthorizer.Application.UseCases.AuthorizeTransaction;
using TransactionAuthorizer.Domain.Entities;
using TransactionAuthorizer.Infrastructure.IoC;
using TransactionAuthorizer.UnitTests.Infrastructure.IoC;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Application.UseCases.AuthorizeTransaction
{
    public class AuthorizeTransactionUseCaseTest
    {
        [Fact]
        public void Should_AuthorizeTransaction_When_ReceiveAValidTransaction()
        {
            // Arrange
            var transaction = new Transaction
            {
                Value = 10,
                Merchant = "Gas station",
                Time = DateTime.Now
            };

            var input = new AuthorizeTransactionInput { Transaction = new TransactionDetailsModel(transaction) };

            var useCase = new AuthorizeTransactionUseCase(
                new UnitOfWorkFaker(),
                new AccountRepositoryFaker(),
                new TransactionRepositoryFaker());

            // Act
            var output = (AuthorizeTransactionDefaultOutput)useCase.Execute(input);

            // Assert
            Assert.NotNull(output.Account);
            Assert.Empty(output.Account.Violations);
        }

        [Fact]
        public void Should_NotAuthorizeTransaction_When_ReceiveAnInvalidTransaction()
        {
            // Arrange
            var transaction = new Transaction
            {
                Value = 1000,
                Merchant = "Gas station",
                Time = DateTime.Now
            };

            var input = new AuthorizeTransactionInput { Transaction = new TransactionDetailsModel(transaction) };

            var useCase = new AuthorizeTransactionUseCase(
                new UnitOfWorkFaker(),
                new AccountRepositoryFaker(),
                new TransactionRepositoryFaker());

            // Act
            var output = (AuthorizeTransactionDefaultOutput)useCase.Execute(input);

            // Assert
            Assert.NotNull(output.Account);
            Assert.NotEmpty(output.Account.Violations);
            Assert.Contains(Violation.InsuficientLimit, output.Account.Violations);
        }

        [Fact]
        public void Should_CallCustomOutputPortMethods_When_OutputPortIsSet()
        {
            // Arrange
            var transaction = new Transaction
            {
                Value = 1000,
                Merchant = "Gas station",
                Time = DateTime.Now
            };

            var input = new AuthorizeTransactionInput { Transaction = new TransactionDetailsModel(transaction) };

            var customOutput = new AuthorizeTransactionOutputFaker();

            var useCase = new AuthorizeTransactionUseCase(
                new UnitOfWorkFaker(),
                new AccountRepositoryFaker(),
                new TransactionRepositoryFaker());

            // Act
            useCase.SetOutputPort(customOutput);
            var output = (AuthorizeTransactionOutputFaker)useCase.Execute(input);

            // Assert
            Assert.True(output.InsufficientLimitCalled);
            Assert.True(output.FillCalled);
        }
    }
}