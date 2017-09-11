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
    public class ItemClassificacaoCore : IItemPlanoClassificacaoCore
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<ItemPlanoClassificacao> _itensPlanoClassificacao;
        //private ItemPlanoClassificacaoValidation _validation;
        private IMapper _mapper;
        private IOrganizacaoCore _organizacaoCore;

        //public ItemPlanoClassificacaoCore(IScdRepositories repositories, ItemPlanoClassificacaoValidation validation, IMapper mapper, IOrganizacaoCore organizacaoCore)
        //{
        //    _unitOfWork = repositories.UnitOfWork;
        //    _itensPlanoClassificacao = repositories.NiveisClassificacao;
        //    _validation = validation;
        //    _mapper = mapper;
        //    _organizacaoCore = organizacaoCore;
        //}

        public int Count(Guid guidOrganizacao)
        {
            //_validation.OrganizacaoValid(guidOrganizacao);

            //var count = _itensPlanoClassificacao.Where(pc => pc.Organizacao.GuidOrganizacao.Equals(guidOrganizacao))
            //                                .Count();

            //return count;
            return 10;
        }

        public async Task DeleteAsync(int id)
        {
            //ItemPlanoClassificacao itemPlanoClassificacao = SearchPersistence(id);

            //_validation.CanDelete(itemPlanoClassificacao);

            //_itensPlanoClassificacao.Remove(itemPlanoClassificacao);

            //await _unitOfWork.SaveAsync();
        }

        public async Task<ItemPlanoClassificacaoModel> InsertAsync(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {

            //_validation.BasicValid(itemPlanoClassificacaoModel);

            //_validation.IdInsertValid(itemPlanoClassificacaoModel.Id);

            ////TODO: Retirar este trecho quando o sistema conseguir obter organzação do usuário
            //Guid guidProdest = new Guid(Environment.GetEnvironmentVariable("guidGEES"));
            //OrganizacaoModel organizacao = _organizacaoCore.SearchAsync(guidProdest);

            //itemPlanoClassificacaoModel.Organizacao = organizacao;

            //ItemPlanoClassificacao itemPlanoClassificacao = _mapper.Map<ItemPlanoClassificacao>(itemPlanoClassificacaoModel);
            //itemPlanoClassificacao.Ativo = true;

            //await _itensPlanoClassificacao.AddAsync(itemPlanoClassificacao);

            //await _unitOfWork.SaveAsync();

            //itemPlanoClassificacaoModel = _mapper.Map<ItemPlanoClassificacaoModel>(itemPlanoClassificacao);

            //return itemPlanoClassificacaoModel;
            return new ItemPlanoClassificacaoModel { Id = 1, Codigo = "A", Descricao = "Descrição A", NivelClassificacao = new NivelClassificacaoModel {Id = 1, Descricao = "Função", Ativo = true }, PlanoClassificacao= new PlanoClassificacaoModel { Id = 1, Descricao = "Descrição Plano Teste", GuidOrganizacao = new Guid(Environment.GetEnvironmentVariable("guidProdest")), Codigo = "PCA" } };
        }

        public ItemPlanoClassificacaoModel Search(int id)
        {
            //ItemPlanoClassificacao itemPlanoClassificacao = SearchPersistence(id);

            //ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = _mapper.Map<ItemPlanoClassificacaoModel>(itemPlanoClassificacao);

            //return itemPlanoClassificacaoModel;
            return new ItemPlanoClassificacaoModel { Id = 1, Codigo = "A", Descricao = "Descrição A", NivelClassificacao = new NivelClassificacaoModel { Id = 1, Descricao = "Função", Ativo = true }, PlanoClassificacao = new PlanoClassificacaoModel { Id = 1, Descricao = "Descrição Plano Teste", GuidOrganizacao = new Guid(Environment.GetEnvironmentVariable("guidProdest")), Codigo = "PCA" } };
        }

        public List<ItemPlanoClassificacaoModel> Search(Guid guidOrganizacao, int page, int count)
        {
            //            _validation.OrganizacaoValid(guidOrganizacao);

            //            _validation.PaginationSearch(page, count);

            //            int skip = (page - 1) * count;

            //            List<ItemPlanoClassificacao> itemPlanosClassificacao = _itensPlanoClassificacao.Where(pc => pc.Organizacao.GuidOrganizacao.Equals(guidOrganizacao))
            //                                                                               .OrderBy(pc => pc.Ativo)
            //                                                                               .ThenBy(pc => pc.Descricao)
            //                                                                               .Skip(skip)
            //                                                                               .Take(count)
            //                                                                               .ToList()
            //;
            //            List<ItemPlanoClassificacaoModel> itemPlanosClassificacaoModel = _mapper.Map<List<ItemPlanoClassificacaoModel>>(itemPlanosClassificacao);

            //            return itemPlanosClassificacaoModel;
            return new List<ItemPlanoClassificacaoModel> { new ItemPlanoClassificacaoModel { Id = 1, Codigo = "A", Descricao = "Descrição A", NivelClassificacao = new NivelClassificacaoModel { Id = 1, Descricao = "Função", Ativo = true }, PlanoClassificacao = new PlanoClassificacaoModel { Id = 1, Descricao = "Descrição Plano Teste", GuidOrganizacao = new Guid(Environment.GetEnvironmentVariable("guidProdest")), Codigo = "PCA" } } };
        }

        public async Task UpdateAsync(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            //_validation.Valid(itemPlanoClassificacaoModel);

            //ItemPlanoClassificacao itemPlanoClassificacao = SearchPersistence(itemPlanoClassificacaoModel.Id);

            //_validation.CanUpdate(itemPlanoClassificacaoModel, itemPlanoClassificacao);

            //_mapper.Map(itemPlanoClassificacaoModel, itemPlanoClassificacao);

            //await _unitOfWork.SaveAsync();
        }

        //private ItemPlanoClassificacao SearchPersistence(int id)
        //{
        //    _validation.IdValid(id);

        //    ItemPlanoClassificacao itemPlanoClassificacao = _itensPlanoClassificacao.Where(pc => pc.Id == id)
        //                                                                .SingleOrDefault();

        //    _validation.Found(itemPlanoClassificacao);

        //    return itemPlanoClassificacao;
        //}
    }
}