using System.Collections.Generic;
using Moq;
using TransactionAuthorizer.Domain.Utils;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Domain.Utils
{
    public class ExtensionsTest
    {
        [Fact]
        public void Should_AddTheItemInTheEmptyList_When_ReceiveAnItemThatNotExistsInTheList()
        {
            // Arrange
            var listMock = new Mock<IList<string>>();
            listMock.Setup(l => l.Contains(It.IsAny<string>())).Returns(false);

            // Act
            listMock.Object.AddIfNotExists("");

            // Assert
            listMock.Verify(l => l.Add(It.IsAny<string>()));
        }

        [Fact]
        public void Should_AddTheItemInThePopulatedList_When_ReceiveAnItemThatNotExistsInTheList()
        {
            // Arrange
            var list = new List<string> { "plataformtec", "easynvest" };
            string value = "nubank";

            // Act
            list.AddIfNotExists(value);

            // Assert
            Assert.Contains(value, list);
            Assert.True(list.Count == 3);
        }

        [Fact]
        public void Should_NotAddTheItemInTheList_When_ReceiveAnItemThatExistsInTheList()
        {
            // Arrange
            var listMock = new Mock<IList<string>>();
            listMock.Setup(l => l.Contains(It.IsAny<string>())).Returns(true);

            // Act
            listMock.Object.AddIfNotExists("");

            // Assert
            listMock.Verify(l => l.Add(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Should_NotTryToAddTheItemInTheList_When_ReceiveAnNullList()
        {
            // Arrange
            List<string> list = null;

            // Act
            list.AddIfNotExists("");

            // Assert
            Assert.Null(list);
        }
    }
}