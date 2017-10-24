using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository
{
    public interface IPlanoClassificacaoRepository
    {
        Task<PlanoClassificacaoModel> AddAsync(PlanoClassificacaoModel planoClassificacaoModel);
        Task<PlanoClassificacaoModel> SearchAsync(int id);
        Task<ICollection<PlanoClassificacaoModel>> SearchByOrganizacaoAsync(Guid guidOrganizacao, int page, int count);
        Task<int> CountByOrganizacaoAsync(Guid guidOrganizacao);
        Task UpdateAsync(PlanoClassificacaoModel planoClassificacaoModel);
        Task RemoveAsync(int id);
    }
}