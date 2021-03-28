using System;
using TransactionAuthorizer.Presentation.CLI;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Presentation
{
    public class ProgramTest
    {
        [Fact]
        public void Should_SetVerboseEnvironmentVariableToTrue_When_ReceiveVerboseArgument()
        {
            // Arrange
            var arguments = new string[] {"--verbose"};            

            // Act
            Program.HandleArguments(arguments);
            string verbose = Environment.GetEnvironmentVariable("VERBOSE");

            // Assert
            Assert.Equal(Boolean.TrueString, verbose);
        }

        [Fact]
        public void Should_NotSetVerboseEnvironmentVariableToTrue_When_NotReceiveVerboseArgument()
        {
            // Arrange
            var arguments = new string[] { };            

            // Act
            Program.HandleArguments(arguments);
            string verbose = Environment.GetEnvironmentVariable("VERBOSE");

            // Assert
            Assert.Null(verbose);
        }

        [Fact]
        public void Should_NotCallMethods_When_HasNoLinesToRead()
        {
            // Arrange
            

            // Act
            // Program.Process();

            // Assert
            
        }

        [Fact]
        public void Should_CallMethods_When_HasLinesToRead()
        {
            // Arrange
            

            // Act
            // Program.Process();

            // Assert
            
        }
    }
}