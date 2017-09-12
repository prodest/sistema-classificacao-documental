using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface INivelClassificacaoCore
    {
        int Count(Guid guidOrganizacao);

        Task DeleteAsync(int id);

        Task<NivelClassificacaoModel> InsertAsync(NivelClassificacaoModel nivelClassificacao);

        NivelClassificacaoModel Search(int id);

        List<NivelClassificacaoModel> Search(Guid guidOrganizacao, int page, int count);

        Task UpdateAsync(NivelClassificacaoModel nivelClassificacao);
    }
}