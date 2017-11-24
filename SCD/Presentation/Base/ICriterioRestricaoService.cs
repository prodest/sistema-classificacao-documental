using Prodest.Scd.Presentation.ViewModel;
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation.Base
{
    public interface ICriterioRestricaoService
    {
        Task<CriterioRestricaoViewModel> Search(FiltroCriterioRestricao filtro);
        Task<CriterioRestricaoViewModel> Delete(int id);
        Task<CriterioRestricaoViewModel> Edit(int id);
        Task<CriterioRestricaoViewModel> Update(CriterioRestricaoEntidade entidade);
        Task<CriterioRestricaoViewModel> Create(CriterioRestricaoEntidade entidade);
        Task<CriterioRestricaoViewModel> New(int idPlanoClassificacao);

    }
}