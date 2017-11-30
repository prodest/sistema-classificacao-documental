using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository
{
    public interface IFundamentoLegalRepository
    {
        Task<FundamentoLegalModel> AddAsync(FundamentoLegalModel fundamentoLegalModel);
        Task<FundamentoLegalModel> SearchAsync(int id);
        Task<ICollection<FundamentoLegalModel>> SearchByOrganizacaoAsync(Guid guidOrganizacao, int page, int count);
        Task<int> CountByOrganizacaoAsync(Guid guidOrganizacao);
        Task UpdateAsync(FundamentoLegalModel fundamentoLegalModel);
        Task RemoveAsync(int id);
    }
}