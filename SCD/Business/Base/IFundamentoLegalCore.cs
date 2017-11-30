using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface IFundamentoLegalCore
    {
        Task<int> CountAsync(Guid guidOrganizacao);

        Task DeleteAsync(int id);

        Task<FundamentoLegalModel> InsertAsync(FundamentoLegalModel nivelClassificacao);

        Task<FundamentoLegalModel> SearchAsync(int id);

        Task<ICollection<FundamentoLegalModel>> SearchAsync(Guid guidOrganizacao, int page, int count);

        Task UpdateAsync(FundamentoLegalModel nivelClassificacao);
    }
}