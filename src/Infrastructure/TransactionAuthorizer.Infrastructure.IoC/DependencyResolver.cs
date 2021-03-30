using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TransactionAuthorizer.Infrastructure.Repositories.Context;
using TransactionAuthorizer.Infrastructure.Repositories;
using TransactionAuthorizer.Domain.Interfaces.Repositories;
using System;
using TransactionAuthorizer.Application.UseCases.AuthorizeTransaction;
using TransactionAuthorizer.Application.UseCases.CreateAccount;

namespace TransactionAuthorizer.Infrastructure.IoC
{
    public class DependencyResolver
    {
        public static IServiceProvider Resolve(IResolverConfiguration configuration = null)
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            return Configure(serviceCollection, configuration)
                .BuildServiceProvider();
        }

        private static IServiceCollection Configure(IServiceCollection serviceCollection, IResolverConfiguration configuration)
        {
            if(configuration is not null && configuration.IsTest)
                serviceCollection.AddDbContext<TransactionAuthorizerContext>(options => options.UseInMemoryDatabase("TestTransactionAuthorizer"));
            else
                serviceCollection.AddDbContext<TransactionAuthorizerContext>(options => options.UseInMemoryDatabase("TransactionAuthorizer"));

            serviceCollection.AddScoped(typeof(IUnitOfWork), configuration?.UnitOfWork ?? typeof(UnitOfWork));
            serviceCollection.AddScoped(typeof(ITransactionRepository), configuration?.TransactionRepository ?? typeof(TransactionRepository));
            serviceCollection.AddScoped(typeof(IAccountRepository), configuration?.AccountRepository ?? typeof(AccountRepository));

            serviceCollection.AddTransient<AuthorizeTransactionUseCase>();
            serviceCollection.AddTransient<CreateAccountUseCase>();

            return serviceCollection;
        }
    }
}