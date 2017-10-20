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
using Prodest.Scd.Integration.Organograma.Base;
using Prodest.Scd.Integration.Organograma.Model;
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
        private IOrganogramaService _organogramaService;

        protected EFScdRepositories _efRepositories;
        protected ScdRepositories _repositories;
        protected List<int> _idsPlanosClassificacaoTestados = new List<int>();
        protected List<int> _idsNiveisClassificacaoTestados = new List<int>();
        protected List<int> _idsItensPlanoClassificacaoTestados = new List<int>();
        protected ItemPlanoClassificacaoCore _core;
        protected Guid _guidGees = new Guid(Environment.GetEnvironmentVariable("GuidGEES"));

        protected async Task<PlanoClassificacaoModel> InsertPlanoClassificacaoModelAsync()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Plano Classificação Teste",
                AreaFim = true
            };

            Guid guidProdest = new Guid(Environment.GetEnvironmentVariable("guidProdest"));
            OrganogramaOrganizacao organogramaOrganizacaoPatriarca = await _organogramaService.SearchPatriarcaAsync(guidProdest);

            Persistence.Model.Organizacao organizacaoPatriarca = _repositories.Organizacoes.Where(o => o.GuidOrganizacao.Equals(organogramaOrganizacaoPatriarca.Guid))
                                                                                           .SingleOrDefault();

            OrganizacaoModel organizacaoPatriarcaModel = _mapper.Map<OrganizacaoModel>(organizacaoPatriarca);

            planoClassificacaoModel.GuidOrganizacao = guidProdest;
            planoClassificacaoModel.Organizacao = organizacaoPatriarcaModel;

            planoClassificacaoModel = await _efRepositories.PlanosClassificacaoSpecific.AddAsync(planoClassificacaoModel);

            await _efRepositories.UnitOfWork.SaveAsync();

            _idsItensPlanoClassificacaoTestados.Add(planoClassificacaoModel.Id);

            return planoClassificacaoModel;
        }

        protected async Task<Persistence.Model.NivelClassificacao> InsertNivelClassificacaoAsync()
        {
            Persistence.Model.NivelClassificacao nivelClassificacao = new Persistence.Model.NivelClassificacao { Descricao = "Nivel Classificação Teste", Ativo = true };
            nivelClassificacao = await _repositories.NiveisClassificacao.AddAsync(nivelClassificacao);

            await _efRepositories.UnitOfWork.SaveAsync();

            _idsNiveisClassificacaoTestados.Add(nivelClassificacao.Id);

            return nivelClassificacao;
        }

        protected async Task<ItemPlanoClassificacaoModel> InsertModelAsync()
        {
            PlanoClassificacaoModel planoClassificacaoModel = await InsertPlanoClassificacaoModelAsync();

            Persistence.Model.NivelClassificacao nivelClassificacao = await InsertNivelClassificacaoAsync();

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel { Codigo = "02", Descricao = "Item do Plano Classificação Teste", PlanoClassificacao = planoClassificacaoModel, NivelClassificacao = new NivelClassificacaoModel { Id = nivelClassificacao.Id } };
            itemPlanoClassificacaoModel = await _efRepositories.ItensPlanoClassificacaoSpecific.AddAsync(itemPlanoClassificacaoModel);

            await _efRepositories.UnitOfWork.SaveAsync();

            _idsItensPlanoClassificacaoTestados.Add(itemPlanoClassificacaoModel.Id);

            return itemPlanoClassificacaoModel;
        }

        protected async Task<NivelClassificacaoModel> InsertNivelClassificacaoModelAsync()
        {
            Persistence.Model.NivelClassificacao nivelClassificacao = await InsertNivelClassificacaoAsync();

            NivelClassificacaoModel nivelClassificacaoModel = _mapper.Map<NivelClassificacaoModel>(nivelClassificacao);

            return nivelClassificacaoModel;
        }

        protected async Task<ItemPlanoClassificacaoModel> SearchModelAsync(int id)
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await _efRepositories.ItensPlanoClassificacaoSpecific.SearchAsync(id);

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

            _efRepositories = new EFScdRepositories(_mapper);

            _repositories = new ScdRepositories(_mapper);

            ItemPlanoClassificacaoValidation itemPlanoClassificacaoValidation = new ItemPlanoClassificacaoValidation();

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(_repositories, organizacaoValidation, _mapper);

            _core = new ItemPlanoClassificacaoCore(new Infrastructure.Repository.Specific.EFScdRepositories(_mapper), itemPlanoClassificacaoValidation, organizacaoCore);

            PlanoClassificacaoValidation planoClassificacaoValidation = new PlanoClassificacaoValidation();

            IOptions<AcessoCidadaoConfiguration> autenticacaoIdentityServerConfig = Options.Create(new AcessoCidadaoConfiguration { Authority = "https://acessocidadao.es.gov.br/is/" });

            AcessoCidadaoClientAccessToken acessoCidadaoClientAccessToken = new AcessoCidadaoClientAccessToken(autenticacaoIdentityServerConfig);

            _organogramaService = new OrganogramaService(acessoCidadaoClientAccessToken);

            //_planoClassificacaoCore = new PlanoClassificacaoCore(repositories, planoClassificacaoValidation, mapper, organogramaService, organizacaoCore);

            NivelClassificacaoValidation nivelClassificacaoValidation = new NivelClassificacaoValidation(_repositories);

            //_nivelClassificacaoCore = new NivelClassificacaoCore(repositories, nivelClassificacaoValidation, mapper, organizacaoCore);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            foreach (int idPlanoClassificacao in _idsPlanosClassificacaoTestados)
            {
                PlanoClassificacaoModel planoClassificacaoModel = await _efRepositories.PlanosClassificacaoSpecific.SearchAsync(idPlanoClassificacao);

                if (planoClassificacaoModel != null)
                    await _efRepositories.PlanosClassificacaoSpecific.RemoveAsync(planoClassificacaoModel.Id);
            }

            foreach (int idNivelClassificacao in _idsNiveisClassificacaoTestados)
            {
                Persistence.Model.NivelClassificacao nivelClassificacao = _repositories.NiveisClassificacao.SingleOrDefault(nc => nc.Id == idNivelClassificacao);

                if (nivelClassificacao != null)
                    _repositories.NiveisClassificacao.Remove(nivelClassificacao);
            }

            foreach (int idItemPlanoClassificacao in _idsItensPlanoClassificacaoTestados)
            {
                ItemPlanoClassificacaoModel itemPlanoClassificacao = await _efRepositories.ItensPlanoClassificacaoSpecific.SearchAsync(idItemPlanoClassificacao);

                if (itemPlanoClassificacao != null)
                    await _efRepositories.ItensPlanoClassificacaoSpecific.RemoveAsync(itemPlanoClassificacao.Id);
            }

            await _efRepositories.UnitOfWork.SaveAsync();
        }
    }
}