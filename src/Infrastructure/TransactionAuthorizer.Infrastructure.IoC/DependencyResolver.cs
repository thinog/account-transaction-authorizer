using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TransactionAuthorizer.Infrastructure.Repositories.Context;
using TransactionAuthorizer.Infrastructure.Repositories;
using TransactionAuthorizer.Domain.Interfaces.Repositories;
using System;
using TransactionAuthorizer.Application.UseCases.AuthorizeTransaction;
using TransactionAuthorizer.Application.UseCases.CreateAccount;
using Microsoft.EntityFrameworkCore.Storage;

namespace TransactionAuthorizer.Infrastructure.IoC
{
    public class DependencyResolver
    {
        public static IServiceProvider Resolve()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            return Configure(serviceCollection)
                .BuildServiceProvider();
        }

        private static IServiceCollection Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<TransactionAuthorizerContext>(options => options.UseInMemoryDatabase("TransactionAuthorizer"));

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ITransactionRepository, TransactionRepository>();
            serviceCollection.AddScoped<IAccountRepository, AccountRepository>();

            serviceCollection.AddScoped<AuthorizeTransactionUseCase>();
            serviceCollection.AddScoped<CreateAccountUseCase>();

            return serviceCollection;
        }
    }
}