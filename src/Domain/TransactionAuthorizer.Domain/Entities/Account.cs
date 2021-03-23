using System;
using System.Collections.Generic;
using TransactionAuthorizer.Domain.Interfaces.Entities;

namespace TransactionAuthorizer.Domain.Entities
{
    public class Account : IEntity
    {
        public Guid Id { get; set; }
        public int Limit { get; set; }
        public bool Active { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}