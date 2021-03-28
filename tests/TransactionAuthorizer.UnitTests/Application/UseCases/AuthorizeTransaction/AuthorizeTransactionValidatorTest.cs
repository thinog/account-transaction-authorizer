using System.Collections.Generic;
using System.Linq;
using Moq;
using TransactionAuthorizer.Application.UseCases.AuthorizeTransaction;
using TransactionAuthorizer.Domain.Entities;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Application.UseCases.AuthorizeTransaction
{
    public class AuthorizeTransactionValidatorTest
    {
        [Fact]
        public void Should_ReturnTrueAndNotCallAnyOutputMethod_When_ReceiveAValidSetOfObjects()
        {
            // Arrange
            var outputMock = new Mock<IAuthorizeTransactionOutput>();
            outputMock.Setup(o => o.HasErrors).Returns(false);

            var transaction = new Transaction { Value = 10 };
            var input = new AuthorizeTransactionInput(transaction);

            var account = new Account { Active = true, Limit = 20 };

            // Act
            bool valid = AuthorizeTransactionValidator.Validate(outputMock.Object, input, account);

            // Assert
            Assert.True(valid);
            outputMock.Verify(o => o.HasErrors);
            outputMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_CallOnlyAccountNotInitializedAndReturnInvalid_When_ReceiveANullAccount()
        {
            // Arrange
            var outputMock = new Mock<IAuthorizeTransactionOutput>();
            outputMock.Setup(o => o.HasErrors).Returns(true);

            var transaction = new Transaction { Value = 10 };
            var input = new AuthorizeTransactionInput(transaction);

            Account account = null;

            // Act
            bool valid = AuthorizeTransactionValidator.Validate(outputMock.Object, input, account);

            // Assert
            Assert.False(valid);
            outputMock.Verify(o => o.AccountNotInitialized());
            outputMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_CallOnlyCardNotActiveAndReturnInvalid_When_ReceiveAnAccountNotActive()
        {
            // Arrange
            var outputMock = new Mock<IAuthorizeTransactionOutput>();
            outputMock.Setup(o => o.HasErrors).Returns(true);

            var transaction = new Transaction { Value = 10 };
            var input = new AuthorizeTransactionInput(transaction);

            Account account = new Account { Active = false, Limit = 20 };

            // Act
            bool valid = AuthorizeTransactionValidator.Validate(outputMock.Object, input, account);

            // Assert
            Assert.False(valid);
            outputMock.Verify(o => o.HasErrors);
            outputMock.Verify(o => o.CardNotActive());
            outputMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_CallOnlyInsufficientLimitAndReturnInvalid_When_ReceiveATransactionValueHigherThanAccountAvailableLimit()
        {
            // Arrange
            var outputMock = new Mock<IAuthorizeTransactionOutput>();
            outputMock.Setup(o => o.HasErrors).Returns(true);

            var transaction = new Transaction { Value = 100 };
            var input = new AuthorizeTransactionInput(transaction);

            Account account = new Account { Active = true, Limit = 20 };

            // Act
            bool valid = AuthorizeTransactionValidator.Validate(outputMock.Object, input, account);

            // Assert
            Assert.False(valid);
            outputMock.Verify(o => o.HasErrors);
            outputMock.Verify(o => o.InsufficientLimit());
            outputMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_CallOnlyHighFrequencySmallIntervalAndReturnInvalid_When_ReceiveExistsThreeOrMoreTransactionsInLastTwoMinutes()
        {
            // Arrange
            var outputMock = new Mock<IAuthorizeTransactionOutput>();
            outputMock.Setup(o => o.HasErrors).Returns(true);

            var transaction = new Transaction { Value = 10 };
            var input = new AuthorizeTransactionInput(transaction);

            Account account = new Account 
            { 
                Active = true, 
                Limit = 20,  
                Transactions = Enumerable.Range(0, 3).Select(n => new Transaction())
            };

            // Act
            bool valid = AuthorizeTransactionValidator.Validate(outputMock.Object, input, account);

            // Assert
            Assert.False(valid);
            outputMock.Verify(o => o.HasErrors);
            outputMock.Verify(o => o.HighFrequencySmallInterval());
            outputMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_CallOnlyDoubledTransactionAndReturnInvalid_When_ReceiveExistsTwoOrMoreTransactionsFromTheSameMerchantAndWithSameValue()
        {
            // Arrange
            var outputMock = new Mock<IAuthorizeTransactionOutput>();
            outputMock.Setup(o => o.HasErrors).Returns(true);

            var transaction = new Transaction { Merchant = "Gas station", Value = 10 };
            var input = new AuthorizeTransactionInput(transaction);

            Account account = new Account 
            { 
                Active = true, 
                Limit = 20,  
                Transactions = new List<Transaction> { new Transaction { Merchant = transaction.Merchant, Value = transaction.Value } }
            };

            // Act
            bool valid = AuthorizeTransactionValidator.Validate(outputMock.Object, input, account);

            // Assert
            Assert.False(valid);
            outputMock.Verify(o => o.HasErrors);
            outputMock.Verify(o => o.DoubledTransaction());
            outputMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_CallAllMethodsExceptAccountNotInitializedAndReturnInvalid_When_ReceiveObjectsThatMatchAllValidations()
        {
            // Arrange
            var outputMock = new Mock<IAuthorizeTransactionOutput>();
            outputMock.Setup(o => o.HasErrors).Returns(true);

            var transaction = new Transaction { Merchant = "Gas station", Value = 100 };
            var input = new AuthorizeTransactionInput(transaction);

            Account account = new Account 
            { 
                Active = false, 
                Limit = 20,  
                Transactions = new List<Transaction> 
                { 
                    new Transaction { Merchant = transaction.Merchant, Value = transaction.Value }, 
                    new Transaction(), 
                    new Transaction() 
                }
            };

            // Act
            bool valid = AuthorizeTransactionValidator.Validate(outputMock.Object, input, account);

            // Assert
            Assert.False(valid);
            outputMock.Verify(o => o.HasErrors);
            outputMock.Verify(o => o.CardNotActive());
            outputMock.Verify(o => o.InsufficientLimit());
            outputMock.Verify(o => o.HighFrequencySmallInterval());
            outputMock.Verify(o => o.DoubledTransaction());
            outputMock.VerifyNoOtherCalls();
        }
    }
}