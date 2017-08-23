using Microsoft.EntityFrameworkCore;
using Prodest.Scd.Persistence.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Prodest.Scd.Infrastructure.Repository
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        protected DbSet<TEntity> _set;

        public EFGenericRepository(DbContext ctx)
        {
            _set = ctx.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            return _set.Add(entity).Entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _set.AddRange(entities);
        }

        public TEntity Remove(TEntity entity)
        {
            return _set.Remove(entity).Entity;
        }

        public TEntity Update (TEntity entity)
        {
            return _set.Update(entity).Entity;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _set.RemoveRange(entities);
        }

        public IQueryable<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> path) where TProperty : class
        {
            return _set.Include(path);
        }

        public Type ElementType
        {
            get { return _set.AsQueryable().ElementType; }
        }

        public Expression Expression
        {
            get { return _set.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _set.AsQueryable().Provider; }
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _set.AsEnumerable<TEntity>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _set.AsEnumerable().GetEnumerator();
        }
    }
}
