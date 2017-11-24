using Prodest.Scd.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository
{
    public interface ICriterioRestricaoRepository
    {
        Task<CriterioRestricaoModel> AddAsync(CriterioRestricaoModel criterioRestricaoModel);
        Task<CriterioRestricaoModel> SearchAsync(int id);
        Task<ICollection<CriterioRestricaoModel>> SearchByPlanoClassificacaoAsync(int idPlanoClassificacao);
        Task UpdateAsync(CriterioRestricaoModel criterioRestricaoModel);
        Task RemoveAsync(int id);
    }
}