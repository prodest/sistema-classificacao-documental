using Prodest.Scd.Business.Model;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository
{
    public interface ITermoClassificacaoInformacaoRepository
    {
        Task<TermoClassificacaoInformacaoModel> AddAsync(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel);
        Task<TermoClassificacaoInformacaoModel> SearchAsync(int id);
        Task UpdateAsync(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel);
        Task RemoveAsync(int id);
    }
}