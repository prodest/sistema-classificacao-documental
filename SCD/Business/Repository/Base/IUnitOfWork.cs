using System;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        bool AutoSave { get; set; }
        Task SaveAsync();
    }
}