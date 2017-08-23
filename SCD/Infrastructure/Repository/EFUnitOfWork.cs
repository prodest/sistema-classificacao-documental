using Microsoft.EntityFrameworkCore;
using Prodest.Scd.Persistence.Base;

namespace Prodest.Scd.Infrastructure.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public bool AutoSave { get; set; }
        private DbContext _context { get; set; }

        public EFUnitOfWork(DbContext ctx)
        {
            AutoSave = false;
            _context = ctx;
        }

        public IGenericRepository<T> MakeGenericRepository<T>() where T : class
        {
            return new EFGenericRepository<T>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        
        public void Attach(object entity)
        {
            _context.Attach(entity);
        }

        public void Dispose()
        {
            if (AutoSave)
                Save();

            _context.Dispose();
        }
    }
}
