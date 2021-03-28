using System;
using System.IO;
using Moq;
using TransactionAuthorizer.Infrastructure.IoC;
using TransactionAuthorizer.Presentation.CLI;
using TransactionAuthorizer.Presentation.CLI.Streams;
using TransactionAuthorizer.UnitTests.Infrastructure.IoC;
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
        public void Should_NotCallWriterMethods_When_HasNoLinesToRead()
        {
            // Arrange
            var service = DependencyResolver.Resolve(new ResolverConfigurationFaker());

            var outputMock = new Mock<OutputWriter>();
            
            var inputMock = new Mock<InputReader>();
            inputMock.Setup(r => r.ReadLine()).Returns((string)null);

            // Act
            Program.Process(service, inputMock.Object, outputMock.Object);

            // Assert
            outputMock.Verify(tr => tr.WriteLine(It.IsAny<object>()), Times.Never);
            inputMock.Verify(tr => tr.ReadLine());
        }

        [Fact]
        public void Should_CallWriterMethods_When_HasLinesToRead()
        {
            // Arrange
            var json = "{\"account\": {\"active-card\": true, \"available-limit\": 100}}";
            
            var resolverConfiguration = new ResolverConfigurationFaker
            {
                UnitOfWork = typeof(UnitOfWorkFaker),
                AccountRepository = typeof(AccountRepositoryFaker)
            };

            var service = DependencyResolver.Resolve(resolverConfiguration);

            var outputMock = new Mock<OutputWriter>();
            
            var inputMock = new Mock<InputReader>();
            inputMock
                .SetupSequence(r => r.ReadLine())
                .Returns(json)
                .Returns((string)null);

            // Act
            Program.Process(service, inputMock.Object, outputMock.Object);

            // Assert
            outputMock.Verify(tw => tw.WriteLine(It.IsAny<object>()), Times.Once);
            inputMock.Verify(tr => tr.ReadLine(), Times.Exactly(2));
        }
    }
}