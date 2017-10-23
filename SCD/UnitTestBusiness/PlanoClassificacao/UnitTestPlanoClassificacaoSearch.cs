using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Configuration;
using Prodest.Scd.Infrastructure.Integration;
using Prodest.Scd.Infrastructure.Repository;
using Prodest.Scd.Infrastructure.Repository.Specific;
using Prodest.Scd.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.PlanoClassificacao
{
    [TestClass]
    public class UnitTestPlanoClassificacaoSearch
    {
        private Guid _guidProdest = new Guid(Environment.GetEnvironmentVariable("GuidProdest"));
        private PlanoClassificacaoCore _core;

        [TestInitialize]
        public async Task Setup()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<InfrastructureProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            EFScdRepositories repositories = new EFScdRepositories(mapper);

            PlanoClassificacaoValidation planoClassificacaoValidation = new PlanoClassificacaoValidation();

            IOptions<AcessoCidadaoConfiguration> autenticacaoIdentityServerConfig = Options.Create(new AcessoCidadaoConfiguration { Authority = "https://acessocidadao.es.gov.br/is/" });

            AcessoCidadaoClientAccessToken acessoCidadaoClientAccessToken = new AcessoCidadaoClientAccessToken(autenticacaoIdentityServerConfig);

            OrganogramaService organogramaService = new OrganogramaService(acessoCidadaoClientAccessToken);

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation);

            _core = new PlanoClassificacaoCore(repositories, organogramaService, organizacaoCore);

            string codigo = "01";
            string descricao = "Descrição Teste";
            bool areaFim = true;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = codigo, Descricao = descricao, AreaFim = areaFim };

            await _core.InsertAsync(planoClassificacaoModel);
        }

        #region Search by Id
        #region Id
        [TestMethod]
        public async Task PlanoClassificacaoTestSearchWithInvalidId()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O id não pode ser nulo ou vazio.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com id zero.");
        }
        #endregion

        [TestMethod]
        public async Task PlanoClassificacaoTestSearchWithIdNonexistentOnDataBase()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(-1);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Plano de Classificação não encontrado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com id inexistente na base de dados.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestSearchWithIdCorrect()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descrição Teste",
                AreaFim = true
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            PlanoClassificacaoModel planoClassificacaoModelSearched = await _core.SearchAsync(planoClassificacaoModel.Id);

            Assert.AreEqual(planoClassificacaoModel.Id, planoClassificacaoModelSearched.Id);
            Assert.AreEqual(planoClassificacaoModel.Codigo, planoClassificacaoModelSearched.Codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, planoClassificacaoModelSearched.Descricao);
            Assert.AreEqual(planoClassificacaoModel.AreaFim, planoClassificacaoModelSearched.AreaFim);
            //Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, planoClassificacaoModelSearched.GuidOrganizacao);
            Assert.IsFalse(planoClassificacaoModelSearched.Aprovacao.HasValue);
            Assert.IsFalse(planoClassificacaoModelSearched.Publicacao.HasValue);
            Assert.IsFalse(planoClassificacaoModelSearched.InicioVigencia.HasValue);
            Assert.IsFalse(planoClassificacaoModelSearched.FimVigencia.HasValue);
        }
        #endregion
                
        #region Pagination Search by GuidOrganização
        #region Guid Organização
        [TestMethod]
        public async Task PlanoClassificacaoTestPaginationSearchWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(Guid.Empty, default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com o guid da organização sendo um guid vazio.");
        }
        #endregion

        #region Page
        [TestMethod]
        public async Task PlanoClassificacaoTestPaginationSearchWithInvalidPage()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(_guidProdest, default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Página inválida.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com a página inválida.");
        }
        #endregion

        #region Count
        [TestMethod]
        public async Task PlanoClassificacaoTestPaginationSearchWithInvalidCount()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(_guidProdest, 1, default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Quantidade de rgistro por página inválida.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com a quantidade de registro por página inválida.");
        }
        #endregion

        [TestMethod]
        public async Task PlanoClassificacaoTestPaginationSearchWithGuidOrganizacaoNonexistentOnDataBase()
        {
            ICollection<PlanoClassificacaoModel> planosClassificacaoModel = await _core.SearchAsync(Guid.NewGuid(), 1, 1);
            Assert.IsNotNull(planosClassificacaoModel);
            Assert.IsTrue(planosClassificacaoModel.Count == 0);
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestPaginationSearch()
        {
            int page = 2;
            int count = 5;

            await InsertPlanosClassificacao(page * count);

            ICollection<PlanoClassificacaoModel> planosClassificacaoModel = await _core.SearchAsync(_guidProdest, page, count);
            Assert.IsNotNull(planosClassificacaoModel);
            Assert.IsTrue(planosClassificacaoModel.Count == count);

            foreach (PlanoClassificacaoModel pcm in planosClassificacaoModel)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(pcm.Codigo));
                Assert.IsFalse(string.IsNullOrWhiteSpace(pcm.Descricao));
                Assert.IsFalse(Guid.Empty.Equals(pcm.GuidOrganizacao));
            }
        }
        #endregion

        private async Task InsertPlanosClassificacao(int count)
        {
            for (int i = 0; i < count; i++)
            {
                string codigo = "01";
                string descricao = "Descrição Teste";
                bool areaFim = true;

                PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
                {
                    Codigo = codigo,
                    Descricao = descricao,
                    AreaFim = areaFim
                };

                planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);
            }
        }
    }
}
