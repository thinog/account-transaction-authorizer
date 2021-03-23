using System;

namespace TransactionAuthorizer.Domain.Interfaces.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}