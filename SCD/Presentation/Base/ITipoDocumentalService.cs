using Prodest.Scd.Presentation.ViewModel;
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation.Base
{
    public interface ITipoDocumentalService
    {
        Task<TipoDocumentalViewModel> Search(FiltroTipoDocumental filtro);
        Task<TipoDocumentalViewModel> Delete(int id);
        Task<TipoDocumentalViewModel> Edit(int id);
        Task<TipoDocumentalViewModel> Update(TipoDocumentalEntidade entidade);
        Task<TipoDocumentalViewModel> Create(TipoDocumentalEntidade entidade);
        Task<TipoDocumentalViewModel> New();
    }
}