using Prodest.Scd.Presentation.ViewModel;
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation.Base
{
    public interface ISigiloService
    {
        Task<SigiloViewModel> Delete(int id);
        Task<SigiloViewModel> Edit(int id);
        Task<SigiloViewModel> Update(SigiloEntidade entidade);
        Task<SigiloViewModel> Create(SigiloEntidade entidade);
        Task<SigiloViewModel> New(int IdDocumento);

    }
}