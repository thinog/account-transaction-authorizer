using System;
using System.IO;
using Moq;
using TransactionAuthorizer.Presentation.CLI.Streams;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Presentation
{
    public class OutputWriterTest
    {
        [Fact]
        public void Should_CallWriteLineMethodFromTextWriter_When_ReceiveAValidString()
        {
            // Arrange
            var textWriterMock = new Mock<TextWriter>();
            var reader = new OutputWriter(textWriterMock.Object);

            // Act
            reader.WriteLine("abc");

            // Assert
            textWriterMock.Verify(tw => tw.WriteLine(It.IsAny<object>()));
        }

        [Fact]
        public void Should_NotCallWriteLineMethodFromTextWriter_When_IsNotInVerboseMode()
        {
            // Arrange
            var textWriterMock = new Mock<TextWriter>();
            var reader = new OutputWriter(textWriterMock.Object);
            Environment.SetEnvironmentVariable("VERBOSE", null);

            // Act
            reader.WriteDebug("abc");

            // Assert
            textWriterMock.Verify(tw => tw.WriteLine(It.IsAny<object>()), Times.Never);
        }

        [Fact]
        public void Should_CallWriteLineMethodFromTextWriter_When_IsInVerboseMode()
        {
            // Arrange
            var textWriterMock = new Mock<TextWriter>();
            var reader = new OutputWriter(textWriterMock.Object);
            Environment.SetEnvironmentVariable("VERBOSE", Boolean.TrueString);

            // Act
            reader.WriteDebug("abc");

            // Assert
            textWriterMock.Verify(tw => tw.WriteLine(It.IsAny<object>()));
        }
    }
}