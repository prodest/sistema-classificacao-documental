using Prodest.Scd.Business.Model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface ITermoClassificacaoInformacaoCore
    {
        Task<TermoClassificacaoInformacaoModel> InsertAsync(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel);

        Task<TermoClassificacaoInformacaoModel> SearchAsync(int id);

        Task<ICollection<TermoClassificacaoInformacaoModel>> SearchByUserAsync();

        Task UpdateAsync(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel);

        Task DeleteAsync(int id);
    }
}