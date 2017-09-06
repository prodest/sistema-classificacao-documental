using Prodest.Scd.Presentation.ViewModel;
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation.Base
{
    public interface INivelClassificacaoService
    {
        Task<NivelClassificacaoViewModel> Search(FiltroNivelClassificacao filtro);
        Task<NivelClassificacaoViewModel> Delete(int id);
        Task<NivelClassificacaoViewModel> Edit(int id);
        Task<NivelClassificacaoViewModel> Update(NivelClassificacaoEntidade entidade);
        Task<NivelClassificacaoViewModel> Create(NivelClassificacaoEntidade entidade);
        Task<NivelClassificacaoViewModel> New();
    }
}