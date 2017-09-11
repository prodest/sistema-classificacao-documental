using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface IItemPlanoClassificacaoCore
    {
        int Count(Guid guidOrganizacao);

        Task DeleteAsync(int id);

        Task<ItemPlanoClassificacaoModel> InsertAsync(ItemPlanoClassificacaoModel itemPlanoClassificacao);

        ItemPlanoClassificacaoModel Search(int id);

        List<ItemPlanoClassificacaoModel> Search(Guid guidOrganizacao, int page, int count);

        Task UpdateAsync(ItemPlanoClassificacaoModel itemPlanoClassificacao);
    }
}