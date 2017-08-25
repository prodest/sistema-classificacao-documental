using System;
using System.Threading.Tasks;

namespace Prodest.Scd.Persistence.Base
{
    public interface IUnitOfWork : IDisposable
    {
        bool AutoSave { get; set; }
        void Save();
        Task SaveAsync();
        void Attach(object entity);
        IGenericRepository<T> MakeGenericRepository<T>() where T : class;
    }
}
