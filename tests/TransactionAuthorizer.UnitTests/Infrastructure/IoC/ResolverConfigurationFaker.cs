using System;
using TransactionAuthorizer.Infrastructure.IoC;
using Xunit;

namespace TransactionAuthorizer.UnitTests.Infrastructure.IoC
{
    public class ResolverConfigurationFaker : IResolverConfiguration
    {
        public ResolverConfigurationFaker()
        {
            IsTest = true;
        }

        public bool IsTest { get; set; }
        public Type UnitOfWork { get; set; }
        public Type TransactionRepository { get; set; }
        public Type AccountRepository { get; set; }
    }
}