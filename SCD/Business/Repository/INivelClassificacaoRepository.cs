using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository
{
    public interface INivelClassificacaoRepository
    {
        Task<NivelClassificacaoModel> AddAsync(NivelClassificacaoModel nivelClassificacaoModel);
        Task<NivelClassificacaoModel> SearchAsync(int id);
        Task<ICollection<NivelClassificacaoModel>> SearchByOrganizacaoAsync(Guid guidOrganizacao, int page, int count);
        Task<int> CountByOrganizacaoAsync(Guid guidOrganizacao);
        Task UpdateAsync(NivelClassificacaoModel nivelClassificacaoModel);
        Task RemoveAsync(int id);
    }
}