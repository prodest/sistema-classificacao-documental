using Prodest.Scd.Presentation.ViewModel;
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation.Base
{
    public interface IPlanoClassificacaoService
    {
        Task<PlanoClassificacaoViewModel> Search(FiltroPlanoClassificacao filtro);
        Task<PlanoClassificacaoViewModel> Delete(int id);
        Task<PlanoClassificacaoViewModel> Edit(int id);
        Task<PlanoClassificacaoViewModel> Update(PlanoClassificacaoEntidade entidade);
        Task<PlanoClassificacaoViewModel> Create(PlanoClassificacaoEntidade entidade);
        Task<PlanoClassificacaoViewModel> New();

        Task<PlanoClassificacaoViewModel> UpdateVigencia(PlanoClassificacaoEntidade entidade);
        Task<PlanoClassificacaoViewModel> EncerrarVigencia(int id);

    }
}