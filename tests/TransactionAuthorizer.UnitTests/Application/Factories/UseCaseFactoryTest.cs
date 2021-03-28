using System;
using Newtonsoft.Json;
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
            Assert.IsType<CreateAccountUseCase>(result);
        }

        [Fact]
        public void Should_ReturnTheAuthorizeTransactionUseCase_When_ReceiveAnExpectedJson()
        {
            // Arrange
            string json = "{\"transaction\": {\"merchant\": \"Habbib's\", \"amount\": 90, \"time\":\"2019-02-13T11:00:00.000Z\"}}";
            
            // Act
            var result = UseCaseFactory.CreateUseCase(json, _serviceProvider);

            // Assert
            Assert.IsType<AuthorizeTransactionUseCase>(result);
        }

        [Fact]
        public void Should_ReturnNullUseCase_When_ReceiveAnUnexpectedJson()
        {
            // Arrange
            string json = "{\"close-account\": {\"account-number\": 123}}";
            
            // Act
            var result = UseCaseFactory.CreateUseCase(json, _serviceProvider);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Should_ReturnNullUseCase_When_ReceiveAnInvalidJson()
        {
            // Arrange
            string json = "{\"transaction\": {\"merchant\":";
            string expectedError = "Invalid JSON format!";
            
            // Act
            var result = Assert.Throws<JsonReaderException>(() => UseCaseFactory.CreateUseCase(json,_serviceProvider));

            // Assert
            Assert.Equal(expectedError, result.Message);
        }

        [Fact]
        public void Should_ReturnAnCreateAccountInputInstance_When_ReceiveAValidJson()
        {
            // Arrange
            string json = "{\"account\": {\"active-card\": true, \"available-limit\": 100}}";
            
            // Act
            var result = UseCaseFactory.CreateInputPort(json, new CreateAccountUseCase(null, null));

            // Assert
            Assert.IsType<CreateAccountInput>(result);
        }

        [Fact]
        public void Should_ReturnAnError_When_ReceiveAJsonWithExtraProperties()
        {
            // Arrange
            string json = "{\"transaction\": {\"merchant\": \"Habbib's\", \"amount\": 90, \"time\":\"2019-02-13T11:00:00.000Z\", \"virtual-card\": true}}";
            string expectedError = "Unknown properties detected!";
            
            // Act
            var result = Assert.Throws<JsonReaderException>(() => UseCaseFactory.CreateInputPort(json, new CreateAccountUseCase(null, null)));

            // Assert
            Assert.Equal(expectedError, result.Message);
        }
    }
}