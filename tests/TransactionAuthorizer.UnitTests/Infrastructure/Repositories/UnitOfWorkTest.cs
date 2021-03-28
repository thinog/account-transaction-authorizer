using Microsoft.EntityFrameworkCore;
using Moq;
using TransactionAuthorizer.Infrastructure.Repositories.Context;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Infrastructure.Repositories
{
    public class UnitOfWorkTest
    {
        [Fact]
        public void Should_CallContextSaveChanges_When_SaveMethodHasCalled()
        {
            // Arrange
            var contextMock = new Mock<TransactionAuthorizerContext>(new DbContextOptions<TransactionAuthorizerContext>());
            var unitOfWork = new UnitOfWork(contextMock.Object);

            // Act
            unitOfWork.Save();

            // Assert
            contextMock.Verify(c => c.SaveChanges());
            contextMock.VerifyNoOtherCalls();
        }
    }
}