using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using TransactionAuthorizer.Domain.Entities;
using TransactionAuthorizer.Infrastructure.Repositories;
using TransactionAuthorizer.Infrastructure.Repositories.Context;
using Xunit;
using TransactionAuthorizer.UnitTests;

namespace TransactionAuthorizer.UnitTests.Infrastructure.Repositories
{
    public class AccountRepositoryTest
    {
        [Fact]
        public void Should_ReturnAnValidAccount_When_GetAccountMethodHasCalled()
        {
            // Arrange
            var account = new Account { Active = true, Limit = 10 };
            var accountList = new List<Account> { account };
            var dbSetMock = accountList.MockDbSet<Account>();

            var contextMock = new Mock<TransactionAuthorizerContext>(new DbContextOptions<TransactionAuthorizerContext>());
            contextMock.Setup(c => c.Set<Account>()).Returns(dbSetMock.Object);

            var repository = new AccountRepository(contextMock.Object);

            // Act
            var result = repository.GetAccount();

            // Assert
            Assert.Equal(account.Limit, result.Limit);
            Assert.Equal(account.Active, result.Active);
        }

        [Fact]
        public void Should_ReturnAnNullAccount_When_GetAccountMethodHasCalledAndCantFindAnyAccount()
        {
            // Arrange
            var accountList = new List<Account>();
            var dbSetMock = accountList.MockDbSet<Account>();

            var contextMock = new Mock<TransactionAuthorizerContext>(new DbContextOptions<TransactionAuthorizerContext>());
            contextMock.Setup(c => c.Set<Account>()).Returns(dbSetMock.Object);

            var repository = new AccountRepository(contextMock.Object);

            // Act
            var result = repository.GetAccount();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Should_UpdateAccount_When_ReceiveAnExistingAccountObject()
        {
            // Arrange
            var account = new Account { Active = true, Limit = 10 };
            var accountNewValues = new Account { Active = false, Limit = 80 };
            var accountList = new List<Account>() { account };

            var dbSetMock = accountList.MockDbSet<Account>();
            dbSetMock
                .Setup(ds => ds.Update(It.IsAny<Account>()))
                .Callback<Account>((Account account) => accountList[0] = accountNewValues);

            var contextMock = new Mock<TransactionAuthorizerContext>(new DbContextOptions<TransactionAuthorizerContext>());
            contextMock.Setup(c => c.Set<Account>()).Returns(dbSetMock.Object);

            var repository = new AccountRepository(contextMock.Object);

            // Act
            repository.Update(account);

            // Assert
            Assert.True(accountList.Count == 1);
            Assert.Equal(accountNewValues.Active, accountList.FirstOrDefault().Active);
            Assert.Equal(accountNewValues.Limit, accountList.FirstOrDefault().Limit);
        }

        [Fact]
        public void Should_InsertAccount_When_ReceiveAnAccountObject()
        {
            // Arrange
            var account = new Account { Active = true, Limit = 10 };
            var accountList = new List<Account>();
            var dbSetMock = accountList.MockDbSet<Account>();
            dbSetMock
                .Setup(ds => ds.Add(It.IsAny<Account>()))
                .Callback<Account>((Account account) => accountList.Add(account));

            var contextMock = new Mock<TransactionAuthorizerContext>(new DbContextOptions<TransactionAuthorizerContext>());
            contextMock.Setup(c => c.Set<Account>()).Returns(dbSetMock.Object);

            var repository = new AccountRepository(contextMock.Object);

            // Act
            repository.Insert(account);

            // Assert
            Assert.True(accountList.Count == 1);
            Assert.Contains(account, accountList);
        }
    }
}