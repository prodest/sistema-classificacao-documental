using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business
{
    public class ItemPlanoClassificacaoCore : IItemPlanoClassificacaoCore
    {
        private IUnitOfWork _unitOfWork;
        private IItemPlanoClassificacaoRepository _itensPlanoClassificacao;
        private ItemPlanoClassificacaoValidation _validation;
        private IOrganizacaoCore _organizacaoCore;

        public ItemPlanoClassificacaoCore(IScdRepositories repositories, ItemPlanoClassificacaoValidation validation)
        {
            _unitOfWork = repositories.UnitOfWork;
            _itensPlanoClassificacao = repositories.ItensPlanoClassificacaoSpecific;
            _validation = validation;
        }

        public async Task<ItemPlanoClassificacaoModel> InsertAsync(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            await _validation.BasicValid(itemPlanoClassificacaoModel);

            _validation.IdInsertValid(itemPlanoClassificacaoModel.Id);

            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário
            itemPlanoClassificacaoModel = await _itensPlanoClassificacao.AddAsync(itemPlanoClassificacaoModel);

            return itemPlanoClassificacaoModel;
        }

        public async Task<ItemPlanoClassificacaoModel> SearchAsync(int id)
        {
            _validation.IdValid(id);

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await _itensPlanoClassificacao.SearchAsync(id);

            _validation.Found(itemPlanoClassificacaoModel);

            return itemPlanoClassificacaoModel;
        }

        public async Task<ICollection<ItemPlanoClassificacaoModel>> SearchAsync(int idPlanoClassificacao, int page, int count)
        {
            _validation.IdValid(idPlanoClassificacao);

            _validation.PaginationSearch(page, count);

            ICollection<ItemPlanoClassificacaoModel> itemPlanosClassificacaoModel = await _itensPlanoClassificacao.SearchByPlanoClassificacaoAsync(idPlanoClassificacao, page, count);

            return itemPlanosClassificacaoModel;
        }

        public async Task<int> CountAsync(int idPlanoClassificacao)
        {
            var count = await _itensPlanoClassificacao.CountByPlanoClassificacao(idPlanoClassificacao);

            return count;
        }

        public async Task UpdateAsync(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            await _validation.Valid(itemPlanoClassificacaoModel);

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModelOld = await _itensPlanoClassificacao.SearchAsync(itemPlanoClassificacaoModel.Id);

            _validation.Found(itemPlanoClassificacaoModelOld);

            _validation.PlanoClassificacaoEquals(itemPlanoClassificacaoModel, itemPlanoClassificacaoModelOld);

            await _validation.CanUpdate(itemPlanoClassificacaoModelOld);

            await _itensPlanoClassificacao.UpdateAsync(itemPlanoClassificacaoModel);
        }

        public async Task DeleteAsync(int id)
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await _itensPlanoClassificacao.SearchAsync(id);

            _validation.Found(itemPlanoClassificacaoModel);

            int countChildren = await _itensPlanoClassificacao.CountChildren(id);

            await _validation.CanDelete(itemPlanoClassificacaoModel, countChildren);

            await _itensPlanoClassificacao.RemoveAsync(itemPlanoClassificacaoModel.Id);
        }
    }
}