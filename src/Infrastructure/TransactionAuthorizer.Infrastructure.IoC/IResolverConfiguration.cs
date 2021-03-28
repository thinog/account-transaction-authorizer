using System;
using Microsoft.EntityFrameworkCore;

namespace TransactionAuthorizer.Infrastructure.IoC
{
    public interface IResolverConfiguration
    {
        bool IsTest { get; set; }
        Type UnitOfWork { get; set; }
        Type TransactionRepository { get; set; }
        Type AccountRepository { get; set; }
    }
}