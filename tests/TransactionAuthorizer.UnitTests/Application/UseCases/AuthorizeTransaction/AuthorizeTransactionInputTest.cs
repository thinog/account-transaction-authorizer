using System;
using TransactionAuthorizer.Application.UseCases.AuthorizeTransaction;
using TransactionAuthorizer.Domain.Entities;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Application.UseCases.AuthorizeTransaction
{
    public class AuthorizeTransactionInputTest
    {
        [Fact]
        public void Should_ReturnAnValidTransactionObject_When_InputHasInstancedWithATransaction()
        {
            // Arrange
            var transaction = new Transaction { Merchant = "Gas station", Value = 50, Time = DateTime.Now };
            var input = new AuthorizeTransactionInput(transaction);

            // Act
            var result = input.ToTransactionEntity();

            // Assert
            Assert.Equal(transaction.Merchant, result.Merchant);
            Assert.Equal(transaction.Value, result.Value);
            Assert.Equal(transaction.Time, result.Time);
        }

        [Fact]
        public void Should_ReturnAnValidTransactionObject_When_InputHasInstancedWithoutATransaction()
        {
            // Arrange
            var expectedTransaction = new Transaction { Merchant = null, Value = 0, Time = DateTime.MinValue };
            var input = new AuthorizeTransactionInput();

            // Act
            var result = input.ToTransactionEntity();

            // Assert
            Assert.Equal(expectedTransaction.Merchant, result.Merchant);
            Assert.Equal(expectedTransaction.Value, result.Value);
            Assert.Equal(expectedTransaction.Time, result.Time);
        }
    }
}