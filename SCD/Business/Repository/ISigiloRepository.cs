using Prodest.Scd.Business.Model;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository
{
    public interface ISigiloRepository
    {
        Task<SigiloModel> AddAsync(SigiloModel sigiloModel);
        Task<SigiloModel> SearchAsync(int id);
        Task UpdateAsync(SigiloModel sigiloModel);
        Task RemoveAsync(int id);
    }
}