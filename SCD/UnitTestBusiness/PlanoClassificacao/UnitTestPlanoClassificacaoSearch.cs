using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Configuration;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Common.Exceptions;
using Prodest.Scd.Infrastructure.Repository;
using Prodest.Scd.Integration.Organograma;
using Prodest.Scd.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.PlanoClassificacao.UnitTestBusiness
{
    [TestClass]
    public class UnitTestPlanoClassificacaoSearch
    {
        private string _guidProdest = Environment.GetEnvironmentVariable("GuidProdest");
        private PlanoClassificacaoCore _core;

        [TestInitialize]
        public async Task Setup()
        {
            ScdRepositories repositories = new ScdRepositories();

            PlanoClassificacaoValidation planoClassificacaoValidation = new PlanoClassificacaoValidation(repositories);

            IOptions<AcessoCidadaoConfiguration> autenticacaoIdentityServerConfig = Options.Create(new AcessoCidadaoConfiguration { Authority = "https://acessocidadao.es.gov.br/is/" });

            AcessoCidadaoClientAccessToken acessoCidadaoClientAccessToken = new AcessoCidadaoClientAccessToken(autenticacaoIdentityServerConfig);

            OrganogramaService organogramaService = new OrganogramaService(acessoCidadaoClientAccessToken);

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BusinessProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation, mapper);

            _core = new PlanoClassificacaoCore(repositories, planoClassificacaoValidation, mapper, organogramaService, organizacaoCore);

            string codigo = "01";
            string descricao = "Descrição Teste";
            bool areaFim = true;
            string guidOrganizacao = _guidProdest;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = codigo, Descricao = descricao, AreaFim = areaFim, GuidOrganizacao = guidOrganizacao };

            await _core.InsertAsync(planoClassificacaoModel);
        }

        #region Search by Id
        #region Id
        [TestMethod]
        public async Task TestSearchWithInvalidId()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "O id deve não pode ser nulo ou vazio.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com id zero.");
        }
        #endregion

        [TestMethod]
        public async Task TestSearchWithIdNonexistentOnDataBase()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(-1);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "Plano de Classificação não encontrado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com id inexistente na base de dados.");
        }

        [TestMethod]
        public async Task TestSearchWithIdCorrect()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descrição Teste",
                AreaFim = true,
                GuidOrganizacao = _guidProdest
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            PlanoClassificacaoModel planoClassificacaoModelSearched = await _core.SearchAsync(planoClassificacaoModel.Id);

            Assert.AreEqual(planoClassificacaoModel.Id, planoClassificacaoModelSearched.Id);
            Assert.AreEqual(planoClassificacaoModel.Codigo, planoClassificacaoModelSearched.Codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, planoClassificacaoModelSearched.Descricao);
            Assert.AreEqual(planoClassificacaoModel.AreaFim, planoClassificacaoModelSearched.AreaFim);
            Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, planoClassificacaoModelSearched.GuidOrganizacao);
            Assert.IsFalse(planoClassificacaoModelSearched.Aprovacao.HasValue);
            Assert.IsFalse(planoClassificacaoModelSearched.Publicacao.HasValue);
            Assert.IsFalse(planoClassificacaoModelSearched.InicioVigencia.HasValue);
            Assert.IsFalse(planoClassificacaoModelSearched.FimVigencia.HasValue);
        }
        #endregion

        #region Search by GuidOrganização
        #region Guid Organização
        [TestMethod]
        public async Task TestSearchWithGuidOrganizacaoNull()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(null);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com guid da organização nulo.");
        }

        [TestMethod]
        public async Task TestSearchWithGuidOrganizacaoEmpty()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync("");

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com guid da organização vazio.");
        }

        [TestMethod]
        public async Task TestSearchWithGuidOrganizacaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(" ");

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com o guid da organização sendo um guid vazio.");
        }

        [TestMethod]
        public async Task TestSearchWithGuidOrganizacaoNotGuid()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync("ABC");

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com o guid da organização não válido.");
        }

        [TestMethod]
        public async Task TestSearchWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(new Guid().ToString());

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com o guid da organização sendo um guid vazio.");
        }
        #endregion

        [TestMethod]
        public async Task TestSearchWithGuidOrganizacaoNonexistentOnDataBase()
        {
            List<PlanoClassificacaoModel> planosClassificacaoModel = await _core.SearchAsync(Guid.NewGuid().ToString());
            Assert.IsNotNull(planosClassificacaoModel);
            Assert.IsTrue(planosClassificacaoModel.Count == 0);
        }

        [TestMethod]
        public async Task TestSearchWithGuidOrganizacaoCorrect()
        {
            List<PlanoClassificacaoModel> planosClassificacaoModel = await _core.SearchAsync(_guidProdest);
            Assert.IsNotNull(planosClassificacaoModel);
            Assert.IsTrue(planosClassificacaoModel.Count > 0);

            foreach (PlanoClassificacaoModel pcm in planosClassificacaoModel)
            {
                Assert.IsTrue(!string.IsNullOrWhiteSpace(pcm.Codigo));
                Assert.IsTrue(!string.IsNullOrWhiteSpace(pcm.Descricao));
                Assert.IsTrue(!string.IsNullOrWhiteSpace(pcm.GuidOrganizacao));
            }
        }
        #endregion

        #region Pagination Search by GuidOrganização
        #region Guid Organização
        [TestMethod]
        public async Task TestPaginationSearchWithGuidOrganizacaoNull()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(null, default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com guid da organização nulo.");
        }

        [TestMethod]
        public async Task TestPaginationSearchWithGuidOrganizacaoEmpty()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync("", default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com guid da organização vazio.");
        }

        [TestMethod]
        public async Task TestPaginationSearchWithGuidOrganizacaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(" ", default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com o guid da organização sendo um guid vazio.");
        }

        [TestMethod]
        public async Task TestPaginationSearchWithGuidOrganizacaoNotGuid()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync("ABC", default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com o guid da organização não válido.");
        }

        [TestMethod]
        public async Task TestPaginationSearchWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(new Guid().ToString(), default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com o guid da organização sendo um guid vazio.");
        }
        #endregion

        #region Page
        [TestMethod]
        public async Task TestPaginationSearchWithInvalidPage()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(_guidProdest, default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "Página inválida.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com a página inválida.");
        }
        #endregion

        #region Count
        [TestMethod]
        public async Task TestPaginationSearchWithInvalidCount()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(_guidProdest, 1, default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "Quantidade de rgistro por página inválida.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com a quantidade de registro por página inválida.");
        }
        #endregion

        [TestMethod]
        public async Task TestPaginationSearchWithGuidOrganizacaoNonexistentOnDataBase()
        {
            List<PlanoClassificacaoModel> planosClassificacaoModel = await _core.SearchAsync(Guid.NewGuid().ToString(), 1, 1);
            Assert.IsNotNull(planosClassificacaoModel);
            Assert.IsTrue(planosClassificacaoModel.Count == 0);
        }

        [TestMethod]
        public async Task TestPaginationSearch()
        {
            int page = 2;
            int count = 5;

            await InsertPlanosClassificacao(page * count);

            List<PlanoClassificacaoModel> planosClassificacaoModel = await _core.SearchAsync(_guidProdest, page, count);
            Assert.IsNotNull(planosClassificacaoModel);
            Assert.IsTrue(planosClassificacaoModel.Count == count);

            foreach (PlanoClassificacaoModel pcm in planosClassificacaoModel)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(pcm.Codigo));
                Assert.IsFalse(string.IsNullOrWhiteSpace(pcm.Descricao));
                Assert.IsFalse(string.IsNullOrWhiteSpace(pcm.GuidOrganizacao));
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
                string guidOrganizacao = _guidProdest;

                PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
                {
                    Codigo = codigo,
                    Descricao = descricao,
                    AreaFim = areaFim,
                    GuidOrganizacao = guidOrganizacao
                };

                planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);
            }
        }
    }
}
