using Prodest.Scd.Presentation.ViewModel;
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation.Base
{
    public interface ITemporalidadeService
    {
        Task<TemporalidadeViewModel> Delete(int id);
        Task<TemporalidadeViewModel> Edit(int id);
        Task<TemporalidadeViewModel> Update(TemporalidadeEntidade entidade);
        Task<TemporalidadeViewModel> Create(TemporalidadeEntidade entidade);
        Task<TemporalidadeViewModel> New(int IdDocumento);

    }
}