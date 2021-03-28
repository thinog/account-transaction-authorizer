using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using TransactionAuthorizer.Domain.Entities;
using TransactionAuthorizer.Infrastructure.Repositories;
using TransactionAuthorizer.Infrastructure.Repositories.Context;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Infrastructure.Repositories
{
    public class TransactionRepositoryTest
    {
        [Fact]
        public void Should_ReturnAnEmptyListOfTransactions_When_NoTransactionsIsInTheTimeRange()
        {
            // Arrange
            var searchDate = DateTime.Now.AddDays(-1);

            var transactionList = new List<Transaction>()
            {
                new Transaction { Merchant = "Gas station", Value = 10, Time = DateTime.Now.AddDays(-10) },
                new Transaction { Merchant = "Gas station", Value = 20, Time = DateTime.Now.AddDays(-8) },
                new Transaction { Merchant = "Gas station", Value = 30, Time = DateTime.Now.AddDays(-6) },
                new Transaction { Merchant = "Gas station", Value = 40, Time = DateTime.Now.AddDays(-4) }
            };

            var dbSetMock = transactionList.MockDbSet<Transaction>();

            var contextMock = new Mock<TransactionAuthorizerContext>(new DbContextOptions<TransactionAuthorizerContext>());
            contextMock.Setup(c => c.Set<Transaction>()).Returns(dbSetMock.Object);

            var repository = new TransactionRepository(contextMock.Object);

            // Act
            var result = repository.GetTransactionsByTime(searchDate);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Should_ReturnAListOfTransactions_When_TransactionsMatchTheTimeRange()
        {
            // Arrange
            var searchDate = DateTime.Now.AddMinutes(-2);

            var transactionList = new List<Transaction>()
            {
                new Transaction { Merchant = "Gas station", Value = 10, Time = DateTime.Now.AddMinutes(-1) },
                new Transaction { Merchant = "Gas station", Value = 20, Time = DateTime.Now.AddMinutes(-1) },
                new Transaction { Merchant = "Gas station", Value = 30, Time = DateTime.Now.AddMinutes(-1) },
                new Transaction { Merchant = "Gas station", Value = 40, Time = DateTime.Now.AddMinutes(-10) }
            };

            var dbSetMock = transactionList.MockDbSet<Transaction>();

            var contextMock = new Mock<TransactionAuthorizerContext>(new DbContextOptions<TransactionAuthorizerContext>());
            contextMock.Setup(c => c.Set<Transaction>()).Returns(dbSetMock.Object);

            var repository = new TransactionRepository(contextMock.Object);

            // Act
            var result = repository.GetTransactionsByTime(searchDate);

            // Assert
            Assert.True(result.Count() == 3);
        }

        [Fact]
        public void Should_InsertTransaction_When_ReceiveAnTransactionObject()
        {
            // Arrange
            var transaction = new Transaction { Merchant = "Gas station", Value = 10, Time = DateTime.Now };
            var transactionList = new List<Transaction>();
            var dbSetMock = transactionList.MockDbSet<Transaction>();
            dbSetMock
                .Setup(ds => ds.Add(It.IsAny<Transaction>()))
                .Callback<Transaction>((Transaction transaction) => transactionList.Add(transaction));

            var contextMock = new Mock<TransactionAuthorizerContext>(new DbContextOptions<TransactionAuthorizerContext>());
            contextMock.Setup(c => c.Set<Transaction>()).Returns(dbSetMock.Object);

            var repository = new TransactionRepository(contextMock.Object);

            // Act
            repository.Insert(transaction);

            // Assert
            Assert.True(transactionList.Count == 1);
            Assert.Contains(transaction, transactionList);
        }
    }
}