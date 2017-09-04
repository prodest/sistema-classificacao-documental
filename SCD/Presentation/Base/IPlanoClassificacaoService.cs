using Prodest.Scd.Presentation.ViewModel;
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation.Base
{
    public interface IPlanoClassificacaoService
    {
        Task<PlanoClassificacaoViewModel> Search(Filtro filtro);
        Task<PlanoClassificacaoViewModel> Delete(int id);
        Task<PlanoClassificacaoViewModel> Edit(int id);
        Task<PlanoClassificacaoViewModel> Update(PlanoClassificacaoEntidade entidade);
        Task<PlanoClassificacaoViewModel> Create(PlanoClassificacaoEntidade entidade);
        Task<PlanoClassificacaoViewModel> New();
    }
}