using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Configuration;
using Prodest.Scd.Infrastructure.Integration;
using Prodest.Scd.Infrastructure.Repository;
using Prodest.Scd.Infrastructure.Repository.Specific;
using Prodest.Scd.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.ItemPlanoClassificacao
{
    public class UnitTestItemPlanoClassificacaoCommon
    {
        private IMapper _mapper;
        protected EFScdRepositories _repositories;
        protected List<int> _idsPlanosClassificacaoTestados = new List<int>();
        protected List<int> _idsNiveisClassificacaoTestados = new List<int>();
        protected List<int> _idsItensPlanoClassificacaoTestados = new List<int>();
        protected ItemPlanoClassificacaoCore _core;
        protected Guid _guidGees = new Guid(Environment.GetEnvironmentVariable("GuidGEES"));

        protected async Task<PlanoClassificacaoModel> InsertPlanoClassificacaoAsync()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Plano Classificação Teste", AreaFim = true };
            planoClassificacaoModel = await _repositories.PlanosClassificacaoSpecific.AddAsync(planoClassificacaoModel);

            _idsItensPlanoClassificacaoTestados.Add(planoClassificacaoModel.Id);

            return planoClassificacaoModel;
        }

        protected async Task<NivelClassificacaoModel> InsertNivelClassificacaoAsync()
        {
            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel { Descricao = "Nivel Classificação Teste", Ativo = true };
            nivelClassificacaoModel = await _repositories.NiveisClassificacaoSpecific.AddAsync(nivelClassificacaoModel);

            _idsNiveisClassificacaoTestados.Add(nivelClassificacaoModel.Id);

            return nivelClassificacaoModel;
        }

        protected async Task<ItemPlanoClassificacaoModel> InsertAsync()
        {
            PlanoClassificacaoModel planoClassificacaoModel = await InsertPlanoClassificacaoAsync();

            NivelClassificacaoModel nivelClassificacaoModel = await InsertNivelClassificacaoAsync();

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel { Codigo = "02", Descricao = "Item do Plano Classificação Teste", PlanoClassificacao = planoClassificacaoModel, NivelClassificacao = nivelClassificacaoModel };
            itemPlanoClassificacaoModel = await _repositories.ItensPlanoClassificacaoSpecific.AddAsync(itemPlanoClassificacaoModel);

            await _repositories.UnitOfWork.SaveAsync();

            _idsItensPlanoClassificacaoTestados.Add(itemPlanoClassificacaoModel.Id);

            return itemPlanoClassificacaoModel;
        }

        protected async Task<PlanoClassificacaoModel> InsertPlanoClassificacaoModelAsync()
        {
            PlanoClassificacaoModel planoClassificacaoModel = await InsertPlanoClassificacaoAsync();

            return planoClassificacaoModel;
        }

        protected async Task<NivelClassificacaoModel> InsertNivelClassificacaoModelAsync()
        {
            NivelClassificacaoModel nivelClassificacaoModel = await InsertNivelClassificacaoAsync();

            return nivelClassificacaoModel;
        }

        protected async Task<ItemPlanoClassificacaoModel> InsertModelAsync()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await InsertAsync();

            return itemPlanoClassificacaoModel;
        }

        protected async Task<ItemPlanoClassificacaoModel> SearchAsync(int id)
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await _repositories.ItensPlanoClassificacaoSpecific.SearchAsync(id);

            return itemPlanoClassificacaoModel;
        }

        protected async Task<ItemPlanoClassificacaoModel> SearchModelAsync(int id)
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await SearchAsync(id);

            return itemPlanoClassificacaoModel;
        }

        [TestInitialize]
        public void Setup()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<InfrastructureProfileAutoMapper>();
            });

            _mapper = Mapper.Instance;

            _repositories = new EFScdRepositories(_mapper);

            _repositories = new EFScdRepositories(_mapper);

            ItemPlanoClassificacaoValidation itemPlanoClassificacaoValidation = new ItemPlanoClassificacaoValidation();

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(_repositories, organizacaoValidation);

            _core = new ItemPlanoClassificacaoCore(_repositories, itemPlanoClassificacaoValidation, organizacaoCore);

            PlanoClassificacaoValidation planoClassificacaoValidation = new PlanoClassificacaoValidation();

            IOptions<AcessoCidadaoConfiguration> autenticacaoIdentityServerConfig = Options.Create(new AcessoCidadaoConfiguration { Authority = "https://acessocidadao.es.gov.br/is/" });

            AcessoCidadaoClientAccessToken acessoCidadaoClientAccessToken = new AcessoCidadaoClientAccessToken(autenticacaoIdentityServerConfig);

            OrganogramaService organogramaService = new OrganogramaService(acessoCidadaoClientAccessToken);

            //_planoClassificacaoCore = new PlanoClassificacaoCore(repositories, planoClassificacaoValidation, mapper, organogramaService, organizacaoCore);

            NivelClassificacaoValidation nivelClassificacaoValidation = new NivelClassificacaoValidation();

            //_nivelClassificacaoCore = new NivelClassificacaoCore(repositories, nivelClassificacaoValidation, mapper, organizacaoCore);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            foreach (int idPlanoClassificacao in _idsPlanosClassificacaoTestados)
            {
                PlanoClassificacaoModel planoClassificacaoModel = await _repositories.PlanosClassificacaoSpecific.SearchAsync(idPlanoClassificacao);

                if (planoClassificacaoModel != null)
                    await _repositories.PlanosClassificacaoSpecific.RemoveAsync(planoClassificacaoModel.Id);
            }

            foreach (int idNivelClassificacao in _idsNiveisClassificacaoTestados)
            {
                NivelClassificacaoModel nivelClassificacaoModel = await _repositories.NiveisClassificacaoSpecific.SearchAsync(idNivelClassificacao);

                if (nivelClassificacaoModel != null)
                    await _repositories.NiveisClassificacaoSpecific.RemoveAsync(nivelClassificacaoModel.Id);
            }

            foreach (int idItemPlanoClassificacao in _idsItensPlanoClassificacaoTestados)
            {
                ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await _repositories.ItensPlanoClassificacaoSpecific.SearchAsync(idItemPlanoClassificacao);

                if (itemPlanoClassificacaoModel != null)
                    await _repositories.ItensPlanoClassificacaoSpecific.RemoveAsync(itemPlanoClassificacaoModel.Id);
            }

            await _repositories.UnitOfWork.SaveAsync();
        }
    }
}