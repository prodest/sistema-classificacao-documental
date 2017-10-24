using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Configuration;
using Prodest.Scd.Infrastructure.Integration;
using Prodest.Scd.Infrastructure.Repository.Specific;
using Prodest.Scd.Integration.Organograma.Base;
using Prodest.Scd.Integration.Organograma.Model;
using Prodest.Scd.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.Common
{
    public class UnitTestBase
    {
        protected List<int> _idsItensPlanoClassificacaoTestados = new List<int>();
        protected List<int> _idsNiveisClassificacaoTestados = new List<int>();
        protected List<int> _idsPlanosClassificacaoTestados = new List<int>();
        protected List<int> _idsTiposDocumentaisTestados = new List<int>();
        protected EFScdRepositories _repositories;
        //protected Guid _guidGees = new Guid(Environment.GetEnvironmentVariable("GuidGEES"));

        private IOrganogramaService _organogramaService;
        private IOrganizacaoCore _organizacaoCore;

        [TestInitialize]
        public void Setup()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<InfrastructureProfileAutoMapper>();
            });

            IMapper _mapper = Mapper.Instance;

            _repositories = new EFScdRepositories(_mapper);

            IOptions<AcessoCidadaoConfiguration> autenticacaoIdentityServerConfig = Options.Create(new AcessoCidadaoConfiguration { Authority = "https://acessocidadao.es.gov.br/is/" });

            AcessoCidadaoClientAccessToken acessoCidadaoClientAccessToken = new AcessoCidadaoClientAccessToken(autenticacaoIdentityServerConfig);

            _organogramaService = new OrganogramaService(acessoCidadaoClientAccessToken);

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            _organizacaoCore = new OrganizacaoCore(_repositories, organizacaoValidation);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            foreach (int idItemPlanoClassificacao in _idsItensPlanoClassificacaoTestados)
            {
                ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await _repositories.ItensPlanoClassificacaoSpecific.SearchAsync(idItemPlanoClassificacao);

                if (itemPlanoClassificacaoModel != null)
                    await _repositories.ItensPlanoClassificacaoSpecific.RemoveAsync(itemPlanoClassificacaoModel.Id);
            }

            foreach (int idNivelClassificacao in _idsNiveisClassificacaoTestados)
            {
                NivelClassificacaoModel nivelClassificacaoModel = await _repositories.NiveisClassificacaoSpecific.SearchAsync(idNivelClassificacao);

                if (nivelClassificacaoModel != null)
                    await _repositories.NiveisClassificacaoSpecific.RemoveAsync(nivelClassificacaoModel.Id);
            }

            foreach (int idPlanoClassificacao in _idsPlanosClassificacaoTestados)
            {
                PlanoClassificacaoModel planoClassificacaoModel = await _repositories.PlanosClassificacaoSpecific.SearchAsync(idPlanoClassificacao);

                if (planoClassificacaoModel != null)
                    await _repositories.PlanosClassificacaoSpecific.RemoveAsync(planoClassificacaoModel.Id);
            }

            foreach (int idTipoDocumental in _idsTiposDocumentaisTestados)
            {
                TipoDocumentalModel tipoDocumentalModel = await _repositories.TiposDocumentaisSpecific.SearchAsync(idTipoDocumental);

                if (tipoDocumentalModel != null)
                    await _repositories.TiposDocumentaisSpecific.RemoveAsync(tipoDocumentalModel.Id);
            }
        }

        protected async Task<ItemPlanoClassificacaoModel> InsertItemPlanoClassificacaoAsync()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await InsertItemPlanoClassificacaoAsync(null);

            return itemPlanoClassificacaoModel;
        }

        protected async Task<ItemPlanoClassificacaoModel> InsertItemPlanoClassificacaoAsync(PlanoClassificacaoModel planoClassificacaoModel)
        {
            planoClassificacaoModel = planoClassificacaoModel ?? await InsertPlanoClassificacaoAsync();

            NivelClassificacaoModel nivelClassificacaoModel = await InsertNivelClassificacaoAsync();

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Item do Plano Classificação Teste",
                PlanoClassificacao = planoClassificacaoModel,
                NivelClassificacao = nivelClassificacaoModel
            };

            itemPlanoClassificacaoModel = await _repositories.ItensPlanoClassificacaoSpecific.AddAsync(itemPlanoClassificacaoModel);

            _idsItensPlanoClassificacaoTestados.Add(itemPlanoClassificacaoModel.Id);

            return itemPlanoClassificacaoModel;
        }

        protected async Task<NivelClassificacaoModel> InsertNivelClassificacaoAsync()
        {
            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel
            {
                Descricao = "Nivel Classificação Teste",
                Ativo = true,
                Organizacao = await GetOrganizacaoPatricarcaModel()
            };

            nivelClassificacaoModel = await _repositories.NiveisClassificacaoSpecific.AddAsync(nivelClassificacaoModel);

            _idsNiveisClassificacaoTestados.Add(nivelClassificacaoModel.Id);

            return nivelClassificacaoModel;
        }

        protected async Task<PlanoClassificacaoModel> InsertPlanoClassificacaoAsync()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Plano Classificação Teste",
                AreaFim = true,
                GuidOrganizacao = GetGuidOrganizacao(),
                Organizacao = await GetOrganizacaoPatricarcaModel()
            };

            planoClassificacaoModel = await _repositories.PlanosClassificacaoSpecific.AddAsync(planoClassificacaoModel);

            _idsPlanosClassificacaoTestados.Add(planoClassificacaoModel.Id);

            return planoClassificacaoModel;
        }

        protected async Task<TipoDocumentalModel> InsertTipoDocumentalAsync()
        {
            TipoDocumentalModel tipoDocumentalModel = new TipoDocumentalModel
            {
                Descricao = "Tipo Documental Teste",
                Ativo = true,
                Organizacao = await GetOrganizacaoPatricarcaModel()
            };

            tipoDocumentalModel = await _repositories.TiposDocumentaisSpecific.AddAsync(tipoDocumentalModel);

            _idsTiposDocumentaisTestados.Add(tipoDocumentalModel.Id);

            return tipoDocumentalModel;
        }

        protected async Task<ItemPlanoClassificacaoModel> SearchItemPlanoClassificacaoAsync(int idItemPlanoClassificacao)
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await _repositories.ItensPlanoClassificacaoSpecific.SearchAsync(idItemPlanoClassificacao);

            return itemPlanoClassificacaoModel;
        }

        protected async Task<NivelClassificacaoModel> SearchNivelClassificacaoAsync(int idNivelClassificacao)
        {
            NivelClassificacaoModel nivelClassificacaoModel = await _repositories.NiveisClassificacaoSpecific.SearchAsync(idNivelClassificacao);

            return nivelClassificacaoModel;
        }

        protected async Task<PlanoClassificacaoModel> SearchPlanoClassificacaoAsync(int idPlanoClassificacao)
        {
            PlanoClassificacaoModel planoClassificacaoModel = await _repositories.PlanosClassificacaoSpecific.SearchAsync(idPlanoClassificacao);

            return planoClassificacaoModel;
        }

        protected async Task<TipoDocumentalModel> SearchTipoDocumentalAsync(int idTipoDocumental)
        {
            TipoDocumentalModel tipoDocumentalModel = await _repositories.TiposDocumentaisSpecific.SearchAsync(idTipoDocumental);

            return tipoDocumentalModel;
        }

        private Guid GetGuidOrganizacao()
        {
            //TODO: Retirar este trecho quando o sistema conseguir obter organzação do usuário
            Guid guidProdest = new Guid(Environment.GetEnvironmentVariable("guidProdest"));

            return guidProdest;
        }

        private async Task<OrganizacaoModel> GetOrganizacaoPatricarcaModel()
        {
            //TODO: Retirar este trecho quando o sistema conseguir obter organzação do usuário
            OrganogramaOrganizacao organogramaOrganizacaoPatriarca = await _organogramaService.SearchPatriarcaAsync(GetGuidOrganizacao());

            OrganizacaoModel organizacaoPatriarca = await _organizacaoCore.SearchAsync(organogramaOrganizacaoPatriarca.Guid);

            return organizacaoPatriarca;
        }
    }
}
