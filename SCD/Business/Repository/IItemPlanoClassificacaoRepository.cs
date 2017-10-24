using Prodest.Scd.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository
{
    public interface IItemPlanoClassificacaoRepository
    {
        Task<ItemPlanoClassificacaoModel> AddAsync(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel);
        Task<ItemPlanoClassificacaoModel> SearchAsync(int id);
        Task<ICollection<ItemPlanoClassificacaoModel>> SearchByPlanoClassificacaoAsync(int idPlanoClassificacao, int page, int count);
        Task<int> CountByPlanoClassificacao(int idPlanoClassificacao);
        Task UpdateAsync(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel);
        Task RemoveAsync(int id);
        Task<int> CountChildren(int id);
    }
}