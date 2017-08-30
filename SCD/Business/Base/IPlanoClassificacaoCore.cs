using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface IPlanoClassificacaoCore
    {
        Task<int> CountAsync(string guidOrganizacao);

        Task<PlanoClassificacaoModel> InsertAsync(PlanoClassificacaoModel planoClassificacao);

        Task<PlanoClassificacaoModel> SearchAsync(int id);

        Task<List<PlanoClassificacaoModel>> SearchAsync(string guidOrganizacao);

        Task<List<PlanoClassificacaoModel>> SearchAsync(string guidOrganizacao, int page, int count);

        Task UpdateAsync(PlanoClassificacaoModel planoClassificacao);

        Task UpdateFimVigenciaAsync(int id, DateTime fimVigencia);

        Task DeleteAsync(int id);
    }
}
