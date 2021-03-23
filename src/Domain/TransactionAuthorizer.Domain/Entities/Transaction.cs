using System;
using TransactionAuthorizer.Domain.Interfaces.Entities;

namespace TransactionAuthorizer.Domain.Entities
{
    public class Transaction : IEntity
    {
        public Guid Id { get; set; }
        public string Merchant { get; set; }
        public int Value { get; set; }
        public DateTime Time { get; set; }
        public Account Account { get; set; }  
    }
}