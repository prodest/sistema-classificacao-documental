using Prodest.Scd.Business.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Prodest.Scd.Business.Repository
{
    public interface ITermoClassificacaoInformacaoRepository
    {
        Task<TermoClassificacaoInformacaoModel> AddAsync(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel);
        Task<TermoClassificacaoInformacaoModel> SearchAsync(int id);
        Task<ICollection<TermoClassificacaoInformacaoModel>> SearchByUserAsync(string cpfUsuario);
        Task UpdateAsync(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel);
        Task RemoveAsync(int id);
    }
}