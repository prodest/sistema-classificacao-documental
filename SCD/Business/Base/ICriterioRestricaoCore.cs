using Prodest.Scd.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface ICriterioRestricaoCore
    {
        Task<CriterioRestricaoModel> InsertAsync(CriterioRestricaoModel criterioRestricaoModel);

        Task<CriterioRestricaoModel> SearchAsync(int id);

        Task<ICollection<CriterioRestricaoModel>> SearchByPlanoClassificacaoAsync(int idPlanoClassificacao);

        Task UpdateAsync(CriterioRestricaoModel criterioRestricaoModel);

        Task DeleteAsync(int id);
    }
}