using Prodest.Scd.Business.Model;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository
{
    public interface ITemporalidadeRepository
    {
        Task<TemporalidadeModel> AddAsync(TemporalidadeModel temporalidadeModel);
        Task<TemporalidadeModel> SearchAsync(int id);
        Task UpdateAsync(TemporalidadeModel temporalidadeModel);
        Task RemoveAsync(int id);
    }
}