using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TransactionAuthorizer.Domain.Interfaces.Entities;

namespace TransactionAuthorizer.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetByExpression(Expression<Func<TEntity, bool>> where);
        bool Insert(TEntity entity);
    }
}