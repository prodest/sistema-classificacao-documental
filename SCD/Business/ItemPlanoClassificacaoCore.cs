using AutoMapper;
using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Persistence.Base;
using Prodest.Scd.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Business
{
    public class ItemPlanoClassificacaoCore : IItemPlanoClassificacaoCore
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<ItemPlanoClassificacao> _itensPlanoClassificacao;
        private ItemPlanoClassificacaoValidation _validation;
        private IMapper _mapper;
        private IOrganizacaoCore _organizacaoCore;

        public ItemPlanoClassificacaoCore(IScdRepositories repositories, ItemPlanoClassificacaoValidation validation, IMapper mapper, IOrganizacaoCore organizacaoCore)
        {
            _unitOfWork = repositories.UnitOfWork;
            _itensPlanoClassificacao = repositories.ItensPlanoClassificacao;
            _validation = validation;
            _mapper = mapper;
            _organizacaoCore = organizacaoCore;
        }

        public int Count(int idPlanoClassificacao)
        {
            var count = _itensPlanoClassificacao.Where(ipc => ipc.PlanoClassificacao.Id == idPlanoClassificacao)
                                                .Count();

            return count;
        }

        public async Task DeleteAsync(int id)
        {
            ItemPlanoClassificacao itemPlanoClassificacao = SearchPersistence(id);

            _validation.Found(itemPlanoClassificacao);

            _validation.CanDelete(itemPlanoClassificacao);

            _itensPlanoClassificacao.Remove(itemPlanoClassificacao);

            await _unitOfWork.SaveAsync();
        }

        public async Task<ItemPlanoClassificacaoModel> InsertAsync(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            _validation.BasicValid(itemPlanoClassificacaoModel);

            _validation.IdInsertValid(itemPlanoClassificacaoModel.Id);

            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário

            ItemPlanoClassificacao itemPlanoClassificacao = _mapper.Map<ItemPlanoClassificacao>(itemPlanoClassificacaoModel);

            await _itensPlanoClassificacao.AddAsync(itemPlanoClassificacao);

            await _unitOfWork.SaveAsync();

            itemPlanoClassificacaoModel = _mapper.Map<ItemPlanoClassificacaoModel>(itemPlanoClassificacao);

            return itemPlanoClassificacaoModel;
        }

        public ItemPlanoClassificacaoModel Search(int id)
        {
            ItemPlanoClassificacao itemPlanoClassificacao = SearchPersistence(id);

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = _mapper.Map<ItemPlanoClassificacaoModel>(itemPlanoClassificacao);

            return itemPlanoClassificacaoModel;
        }

        public List<ItemPlanoClassificacaoModel> Search(int idPlanoClassificacao, int page, int count)
        {
            _validation.PaginationSearch(page, count);

            int skip = (page - 1) * count;

            List<ItemPlanoClassificacao> itemPlanosClassificacao = _itensPlanoClassificacao.Where(ipc => ipc.PlanoClassificacao.Id == idPlanoClassificacao)
                                                                               .OrderBy(ipc => !ipc.IdItemPlanoClassificacaoPai.HasValue)
                                                                               .OrderBy(ipc => ipc.IdItemPlanoClassificacaoPai.Value)
                                                                               .ThenBy(pc => pc.Descricao)
                                                                               .Skip(skip)
                                                                               .Take(count)
                                                                               .ToList()
;
            List<ItemPlanoClassificacaoModel> itemPlanosClassificacaoModel = _mapper.Map<List<ItemPlanoClassificacaoModel>>(itemPlanosClassificacao);

            return itemPlanosClassificacaoModel;
        }

        public async Task UpdateAsync(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            _validation.Valid(itemPlanoClassificacaoModel);

            ItemPlanoClassificacao itemPlanoClassificacao = SearchPersistence(itemPlanoClassificacaoModel.Id);

            _validation.Found(itemPlanoClassificacao);

            //_validation.CanUpdate(itemPlanoClassificacaoModel, itemPlanoClassificacao);

            _mapper.Map(itemPlanoClassificacaoModel, itemPlanoClassificacao);

            await _unitOfWork.SaveAsync();
        }

        private ItemPlanoClassificacao SearchPersistence(int id)
        {
            ItemPlanoClassificacao itemPlanoClassificacao = _itensPlanoClassificacao.Where(ipc => ipc.Id == id)
                                                                                    .SingleOrDefault();

            return itemPlanoClassificacao;
        }
    }
}