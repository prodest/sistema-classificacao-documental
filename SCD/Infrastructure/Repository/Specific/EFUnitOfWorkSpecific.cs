using Microsoft.EntityFrameworkCore;
using Prodest.Scd.Business.Repository.Base;
using System.Threading.Tasks;

namespace Prodest.Scd.Infrastructure.Repository.Specific
{
    public class EFUnitOfWorkSpecific : IUnitOfWork
    {
        public bool AutoSave { get; set; }
        private DbContext _context { get; set; }

        public EFUnitOfWorkSpecific(DbContext ctx)
        {
            AutoSave = false;
            _context = ctx;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (AutoSave)
                _context.SaveChanges();

            _context.Dispose();
        }
    }
}
