using System;

namespace Prodest.Scd.Persistence.Base
{
    public interface IUnitOfWork : IDisposable
    {
        bool AutoSave { get; set; }
        void Save();
        void Attach(object entity);
        IGenericRepository<T> MakeGenericRepository<T>() where T : class;
    }
}
