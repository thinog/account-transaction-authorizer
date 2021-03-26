using System;
using TransactionAuthorizer.Application.Factories;
using TransactionAuthorizer.Application.UseCases.AuthorizeTransaction;
using TransactionAuthorizer.Application.UseCases.CreateAccount;
using TransactionAuthorizer.Domain.Interfaces.UseCases;
using TransactionAuthorizer.Infrastructure.IoC;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Application.Factories
{
    public class UseCaseFactoryTest
    {
        private IServiceProvider _serviceProvider;

        public UseCaseFactoryTest()
        {
            _serviceProvider = DependencyResolver.Resolve();
        }

        [Fact]
        public void Should_ReturnTheCreateAccountUseCase_When_ReceiveAnExpectedJson()
        {
            // Arrange
            string json = "{\"account\": {\"active-card\": true, \"available-limit\": 100}}";
            
            // Act
            var result = UseCaseFactory.CreateUseCase(json, _serviceProvider);

            // Assert
            Assert.IsType<CreateAccountUseCase>(result.UseCase);
            Assert.IsType<CreateAccountInput>(result.InputPort);
        }

        [Fact]
        public void Should_ReturnTheAuthorizeTransactionUseCase_When_ReceiveAnExpectedJson()
        {
            // Arrange
            string json = "{\"transaction\": {\"merchant\": \"Habbib's\", \"amount\": 90, \"time\":\"2019-02-13T11:00:00.000Z\"}}";
            
            // Act
            var result = UseCaseFactory.CreateUseCase(json, _serviceProvider);

            // Assert
            Assert.IsType<AuthorizeTransactionUseCase>(result.UseCase);
            Assert.IsType<AuthorizeTransactionInput>(result.InputPort);
        }

        [Fact]
        public void Should_ReturnNull_When_ReceiveAnUnexpectedJson()
        {
            // Arrange
            string json = "{\"close-account\": {\"account-number\": 123}}";
            
            // Act
            var result = UseCaseFactory.CreateUseCase(json, _serviceProvider);

            // Assert
            Assert.Null(result.UseCase);
            Assert.Null(result.InputPort);
        }

        [Fact]
        public void Should_ReturnNull_When_ReceiveAnInvalidJson()
        {
            // Arrange
            string json = "{\"transaction\": {\"merchant\":";
            
            // Act
            var result = UseCaseFactory.CreateUseCase(json,_serviceProvider);

            // Assert
            Assert.Null(result.UseCase);
            Assert.Null(result.InputPort);
        }
    }
}