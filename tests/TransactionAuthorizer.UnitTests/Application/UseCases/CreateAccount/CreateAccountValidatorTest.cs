using Moq;
using TransactionAuthorizer.Application.UseCases.CreateAccount;
using TransactionAuthorizer.Domain.Entities;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Application.UseCases.CreateAccount
{
    public class CreateAccountValidatorTest
    {
        [Fact]
        public void Should_ReturnTrueAndNotCallAnyOutputMethod_When_ReceiveAValidSetOfObjects()
        {
            // Arrange
            var outputMock = new Mock<ICreateAccountOutput>();
            outputMock.Setup(o => o.HasErrors).Returns(false);

            Account account = null;

            // Act
            bool valid = CreateAccountValidator.Validate(outputMock.Object, account);

            // Assert
            Assert.True(valid);
            outputMock.Verify(o => o.HasErrors);
            outputMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_CallOnlyAccountAlreadyInitializedAndReturnInvalid_When_ReceiveANotInitializedAccount()
        {
            // Arrange
            var outputMock = new Mock<ICreateAccountOutput>();
            outputMock.Setup(o => o.HasErrors).Returns(true);

            Account account = new Account { Active = true, Limit = 20 };

            // Act
            bool valid = CreateAccountValidator.Validate(outputMock.Object, account);

            // Assert
            Assert.False(valid);
            outputMock.Verify(o => o.HasErrors);
            outputMock.Verify(o => o.AccountAlreadyInitialized());
            outputMock.VerifyNoOtherCalls();
        }
    }
}