using Prodest.Scd.Presentation.ViewModel;
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation.Base
{
    public interface IFundamentoLegalService
    {
        Task<FundamentoLegalViewModel> Search(FiltroFundamentoLegal filtro);
        Task<FundamentoLegalViewModel> Delete(int id);
        Task<FundamentoLegalViewModel> Edit(int id);
        Task<FundamentoLegalViewModel> Update(FundamentoLegalEntidade entidade);
        Task<FundamentoLegalViewModel> Create(FundamentoLegalEntidade entidade);
        Task<FundamentoLegalViewModel> New();
    }
}