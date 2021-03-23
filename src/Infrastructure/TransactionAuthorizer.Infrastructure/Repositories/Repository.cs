using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TransactionAuthorizer.Domain.Interfaces.Entities;
using TransactionAuthorizer.Domain.Interfaces.Repositories;

namespace TransactionAuthorizer.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        public Repository()
        {
            
        }

        public IEnumerable<TEntity> GetByExpression(Expression<Func<TEntity, bool>> where)
        {
            throw new NotImplementedException();
        }

        public bool Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}