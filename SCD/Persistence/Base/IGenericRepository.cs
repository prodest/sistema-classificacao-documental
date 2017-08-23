using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Prodest.Scd.Persistence.Base
{
    public interface IGenericRepository<T> : IQueryable<T> where T : class
    {
        T Add(T entity);
        void AddRange(IEnumerable<T> entities);
        T Update(T entity);
        T Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path) where TProperty : class;
    }
}
