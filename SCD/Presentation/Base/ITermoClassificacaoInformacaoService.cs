using Prodest.Scd.Presentation.ViewModel;
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation.Base
{
    public interface ITermoClassificacaoInformacaoService
    {
        Task<TermoClassificacaoInformacaoViewModel> Search(FiltroTermoClassificacaoInformacao filtro);
        Task<TermoClassificacaoInformacaoViewModel> SearchDocumentosByCriterio(int IdCriterio);
        Task<TermoClassificacaoInformacaoViewModel> Delete(int id);
        Task<TermoClassificacaoInformacaoViewModel> Edit(int id);
        Task<TermoClassificacaoInformacaoViewModel> Update(TermoClassificacaoInformacaoEntidade entidade);
        Task<TermoClassificacaoInformacaoViewModel> Create(TermoClassificacaoInformacaoEntidade entidade);
        Task<TermoClassificacaoInformacaoViewModel> New(int idPlanoClassificacao);
    }
}