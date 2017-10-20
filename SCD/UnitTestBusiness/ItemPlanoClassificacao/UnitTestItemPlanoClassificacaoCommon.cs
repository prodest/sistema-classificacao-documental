using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Configuration;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Integration;
using Prodest.Scd.Infrastructure.Repository;
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
        protected ScdRepositories _repositories;
        protected List<int> _idsPlanosClassificacaoTestados = new List<int>();
        protected List<int> _idsNiveisClassificacaoTestados = new List<int>();
        protected List<int> _idsItensPlanoClassificacaoTestados = new List<int>();
        protected ItemPlanoClassificacaoCore _core;
        protected Guid _guidGees = new Guid(Environment.GetEnvironmentVariable("GuidGEES"));

        protected async Task<Persistence.Model.PlanoClassificacao> InsertPlanoClassificacaoAsync()
        {
            Persistence.Model.PlanoClassificacao planoClassificacao = new Persistence.Model.PlanoClassificacao { Codigo = "01", Descricao = "Plano Classificação Teste", AreaFim = true };
            planoClassificacao = await _repositories.PlanosClassificacao.AddAsync(planoClassificacao);

            await _repositories.UnitOfWork.SaveAsync();

            _idsItensPlanoClassificacaoTestados.Add(planoClassificacao.Id);

            return planoClassificacao;
        }

        protected async Task<Persistence.Model.NivelClassificacao> InsertNivelClassificacaoAsync()
        {
            Persistence.Model.NivelClassificacao nivelClassificacao = new Persistence.Model.NivelClassificacao { Descricao = "Nivel Classificação Teste", Ativo = true };
            nivelClassificacao = await _repositories.NiveisClassificacao.AddAsync(nivelClassificacao);

            await _repositories.UnitOfWork.SaveAsync();

            _idsNiveisClassificacaoTestados.Add(nivelClassificacao.Id);

            return nivelClassificacao;
        }

        protected async Task<Persistence.Model.ItemPlanoClassificacao> InsertAsync()
        {
            Persistence.Model.PlanoClassificacao planoClassificacao = await InsertPlanoClassificacaoAsync();

            Persistence.Model.NivelClassificacao nivelClassificacao = await InsertNivelClassificacaoAsync();

            Persistence.Model.ItemPlanoClassificacao itemPlanoClassificacao = new Persistence.Model.ItemPlanoClassificacao { Codigo = "02", Descricao = "Item do Plano Classificação Teste", PlanoClassificacao = planoClassificacao, NivelClassificacao = nivelClassificacao };
            itemPlanoClassificacao = await _repositories.ItensPlanoClassificacao.AddAsync(itemPlanoClassificacao);

            await _repositories.UnitOfWork.SaveAsync();

            _idsItensPlanoClassificacaoTestados.Add(itemPlanoClassificacao.Id);

            return itemPlanoClassificacao;
        }

        protected async Task<PlanoClassificacaoModel> InsertPlanoClassificacaoModelAsync()
        {
            Persistence.Model.PlanoClassificacao planoClassificacao = await InsertPlanoClassificacaoAsync();

            PlanoClassificacaoModel planoClassificacaoModel = _mapper.Map<PlanoClassificacaoModel>(planoClassificacao);

            return planoClassificacaoModel;
        }

        protected async Task<NivelClassificacaoModel> InsertNivelClassificacaoModelAsync()
        {
            Persistence.Model.NivelClassificacao nivelClassificacao = await InsertNivelClassificacaoAsync();

            NivelClassificacaoModel nivelClassificacaoModel = _mapper.Map<NivelClassificacaoModel>(nivelClassificacao);

            return nivelClassificacaoModel;
        }

        protected async Task<ItemPlanoClassificacaoModel> InsertModelAsync()
        {
            Persistence.Model.ItemPlanoClassificacao itemPlanoClassificacao = await InsertAsync();

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = _mapper.Map<ItemPlanoClassificacaoModel>(itemPlanoClassificacao);

            return itemPlanoClassificacaoModel;
        }

        protected Persistence.Model.ItemPlanoClassificacao SearchAsync(int id)
        {
            Persistence.Model.ItemPlanoClassificacao itemPlanoClassificacao = _repositories.ItensPlanoClassificacao.Where(td => td.Id == id)
                                                                                            .SingleOrDefault();

            return itemPlanoClassificacao;
        }

        protected ItemPlanoClassificacaoModel SearchModelAsync(int id)
        {
            Persistence.Model.ItemPlanoClassificacao itemPlanoClassificacao = SearchAsync(id);

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = _mapper.Map<ItemPlanoClassificacaoModel>(itemPlanoClassificacao);

            return itemPlanoClassificacaoModel;
        }

        [TestInitialize]
        public void Setup()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BusinessProfileAutoMapper>();
            });

            _mapper = Mapper.Instance;

            _repositories = new ScdRepositories(_mapper);

            ItemPlanoClassificacaoValidation itemPlanoClassificacaoValidation = new ItemPlanoClassificacaoValidation();

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(_repositories, organizacaoValidation, _mapper);

            _core = new ItemPlanoClassificacaoCore(new Infrastructure.Repository.Specific.EFScdRepositories(_mapper), itemPlanoClassificacaoValidation, organizacaoCore);

            PlanoClassificacaoValidation planoClassificacaoValidation = new PlanoClassificacaoValidation();

            IOptions<AcessoCidadaoConfiguration> autenticacaoIdentityServerConfig = Options.Create(new AcessoCidadaoConfiguration { Authority = "https://acessocidadao.es.gov.br/is/" });

            AcessoCidadaoClientAccessToken acessoCidadaoClientAccessToken = new AcessoCidadaoClientAccessToken(autenticacaoIdentityServerConfig);

            OrganogramaService organogramaService = new OrganogramaService(acessoCidadaoClientAccessToken);

            //_planoClassificacaoCore = new PlanoClassificacaoCore(repositories, planoClassificacaoValidation, mapper, organogramaService, organizacaoCore);

            NivelClassificacaoValidation nivelClassificacaoValidation = new NivelClassificacaoValidation(_repositories);

            //_nivelClassificacaoCore = new NivelClassificacaoCore(repositories, nivelClassificacaoValidation, mapper, organizacaoCore);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            foreach (int idPlanoClassificacao in _idsPlanosClassificacaoTestados)
            {
                Persistence.Model.PlanoClassificacao planoClassificacao = _repositories.PlanosClassificacao.SingleOrDefault(pc => pc.Id == idPlanoClassificacao);

                if (planoClassificacao != null)
                    _repositories.PlanosClassificacao.Remove(planoClassificacao);
            }

            foreach (int idNivelClassificacao in _idsNiveisClassificacaoTestados)
            {
                Persistence.Model.NivelClassificacao nivelClassificacao = _repositories.NiveisClassificacao.SingleOrDefault(nc => nc.Id == idNivelClassificacao);

                if (nivelClassificacao != null)
                    _repositories.NiveisClassificacao.Remove(nivelClassificacao);
            }

            foreach (int idItemPlanoClassificacao in _idsItensPlanoClassificacaoTestados)
            {
                Persistence.Model.ItemPlanoClassificacao itemPlanoClassificacao = _repositories.ItensPlanoClassificacao.SingleOrDefault(ipc => ipc.Id == idItemPlanoClassificacao);

                if (itemPlanoClassificacao != null)
                    _repositories.ItensPlanoClassificacao.Remove(itemPlanoClassificacao);
            }

            await _repositories.UnitOfWork.SaveAsync();
        }
    }
}