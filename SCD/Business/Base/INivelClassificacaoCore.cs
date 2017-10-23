using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface INivelClassificacaoCore
    {
        Task<int> CountAsync(Guid guidOrganizacao);

        Task DeleteAsync(int id);

        Task<NivelClassificacaoModel> InsertAsync(NivelClassificacaoModel nivelClassificacao);

        Task<NivelClassificacaoModel> SearchAsync(int id);

        Task<ICollection<NivelClassificacaoModel>> SearchAsync(Guid guidOrganizacao, int page, int count);

        Task UpdateAsync(NivelClassificacaoModel nivelClassificacao);
    }
}