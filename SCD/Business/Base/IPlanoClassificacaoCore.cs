using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface IPlanoClassificacaoCore
    {
        int Count(string guidOrganizacao);

        Task<PlanoClassificacaoModel> InsertAsync(PlanoClassificacaoModel planoClassificacao);

        PlanoClassificacaoModel Search(int id);

        List<PlanoClassificacaoModel> Search(string guidOrganizacao);

        List<PlanoClassificacaoModel> Search(string guidOrganizacao, int page, int count);

        Task UpdateAsync(PlanoClassificacaoModel planoClassificacao);

        Task UpdateFimVigenciaAsync(int id, DateTime fimVigencia);

        Task DeleteAsync(int id);
    }
}
