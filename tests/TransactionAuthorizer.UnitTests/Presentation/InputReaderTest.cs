using System.IO;
using Moq;
using TransactionAuthorizer.Presentation.CLI.Streams;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Presentation
{
    public class InputReaderTest
    {
        [Fact]
        public void Should_ReturnText_When_ReadLineFromTextReader()
        {
            // Arrange
            string expectedText = "abc";
            var textReaderMock = new Mock<TextReader>();
            textReaderMock.Setup(r => r.ReadLine()).Returns(expectedText);
            var reader = new InputReader(textReaderMock.Object);

            // Act
            string result = reader.ReadLine();

            // Assert
            Assert.Equal(expectedText, result);
        }
    }
}