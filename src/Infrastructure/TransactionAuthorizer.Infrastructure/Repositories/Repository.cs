using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TransactionAuthorizer.Domain.Interfaces.Entities;
using TransactionAuthorizer.Domain.Interfaces.Repositories;
using TransactionAuthorizer.Infrastructure.Repositories.Context;

namespace TransactionAuthorizer.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly TransactionAuthorizerContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(TransactionAuthorizerContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetByExpression(Expression<Func<TEntity, bool>> where)
        {
            return _dbSet.Where(where);
        }

        public bool Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return _context.SaveChanges() > 0;
        }
    }
}