using Prodest.Scd.Business.Model;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface ISigiloCore
    {
        Task<SigiloModel> InsertAsync(SigiloModel sigiloModel);

        Task<SigiloModel> SearchAsync(int id);

        Task UpdateAsync(SigiloModel sigiloModel);

        Task DeleteAsync(int id);
    }
}