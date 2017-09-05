using Prodest.Scd.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface INivelClassificacaoCore
    {
        int Count(string guidOrganizacao);

        Task DeleteAsync(int id);

        Task<NivelClassificacaoModel> InsertAsync(NivelClassificacaoModel nivelClassificacao);

        NivelClassificacaoModel Search(int id);

        List<NivelClassificacaoModel> Search(string guidOrganizacao, int page, int count);

        Task UpdateAsync(NivelClassificacaoModel nivelClassificacao);
    }
}