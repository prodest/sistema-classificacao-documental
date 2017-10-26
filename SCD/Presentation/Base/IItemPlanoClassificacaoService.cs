using Prodest.Scd.Presentation.ViewModel;
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation.Base
{
    public interface IItemPlanoClassificacaoService
    {
        Task<ItemPlanoClassificacaoViewModel> Search(FiltroItemPlanoClassificacao filtro);
        Task<ItemPlanoClassificacaoViewModel> Delete(int id);
        Task<ItemPlanoClassificacaoViewModel> Edit(int id);
        Task<ItemPlanoClassificacaoViewModel> Update(ItemPlanoClassificacaoEntidade entidade);
        Task<ItemPlanoClassificacaoViewModel> Create(ItemPlanoClassificacaoEntidade entidade);
        Task<ItemPlanoClassificacaoViewModel> New(int idPlanoClassificacao, int? IdItemPlanoClassificacaoParent);

    }
}