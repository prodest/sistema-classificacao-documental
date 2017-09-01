using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Configuration;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Integration;
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
        public void TestSearchWithInvalidId()
        {
            bool ok = false;

            try
            {
                _core.Search(default(int));

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
        public void TestSearchWithIdNonexistentOnDataBase()
        {
            bool ok = false;

            try
            {
                _core.Search(-1);

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

            PlanoClassificacaoModel planoClassificacaoModelSearched = _core.Search(planoClassificacaoModel.Id);

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
        public void TestSearchWithGuidOrganizacaoNull()
        {
            bool ok = false;

            try
            {
                _core.Search(null);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com guid da organização nulo.");
        }

        [TestMethod]
        public void TestSearchWithGuidOrganizacaoEmpty()
        {
            bool ok = false;

            try
            {
                _core.Search("");

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com guid da organização vazio.");
        }

        [TestMethod]
        public void TestSearchWithGuidOrganizacaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                _core.Search(" ");

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com o guid da organização sendo um guid vazio.");
        }

        [TestMethod]
        public void TestSearchWithGuidOrganizacaoNotGuid()
        {
            bool ok = false;

            try
            {
                _core.Search("ABC");

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com o guid da organização não válido.");
        }

        [TestMethod]
        public void TestSearchWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                _core.Search(new Guid().ToString());

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

        [TestMethod]
        public void TestSearchWithGuidOrganizacaoNonexistentOnDataBase()
        {
            List<PlanoClassificacaoModel> planosClassificacaoModel = _core.Search(Guid.NewGuid().ToString());
            Assert.IsNotNull(planosClassificacaoModel);
            Assert.IsTrue(planosClassificacaoModel.Count == 0);
        }

        [TestMethod]
        public void TestSearchWithGuidOrganizacaoCorrect()
        {
            List<PlanoClassificacaoModel> planosClassificacaoModel = _core.Search(_guidProdest);
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
        public void TestPaginationSearchWithGuidOrganizacaoNull()
        {
            bool ok = false;

            try
            {
                _core.Search(null, default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com guid da organização nulo.");
        }

        [TestMethod]
        public void TestPaginationSearchWithGuidOrganizacaoEmpty()
        {
            bool ok = false;

            try
            {
                _core.Search("", default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com guid da organização vazio.");
        }

        [TestMethod]
        public void TestPaginationSearchWithGuidOrganizacaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                _core.Search(" ", default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com o guid da organização sendo um guid vazio.");
        }

        [TestMethod]
        public void TestPaginationSearchWithGuidOrganizacaoNotGuid()
        {
            bool ok = false;

            try
            {
                _core.Search("ABC", default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com o guid da organização não válido.");
        }

        [TestMethod]
        public void TestPaginationSearchWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                _core.Search(new Guid().ToString(), default(int), default(int));

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
        public void TestPaginationSearchWithInvalidPage()
        {
            bool ok = false;

            try
            {
                _core.Search(_guidProdest, default(int), default(int));

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
        public void TestPaginationSearchWithInvalidCount()
        {
            bool ok = false;

            try
            {
                _core.Search(_guidProdest, 1, default(int));

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
        public void TestPaginationSearchWithGuidOrganizacaoNonexistentOnDataBase()
        {
            List<PlanoClassificacaoModel> planosClassificacaoModel = _core.Search(Guid.NewGuid().ToString(), 1, 1);
            Assert.IsNotNull(planosClassificacaoModel);
            Assert.IsTrue(planosClassificacaoModel.Count == 0);
        }

        [TestMethod]
        public async Task TestPaginationSearch()
        {
            int page = 2;
            int count = 5;

            await InsertPlanosClassificacao(page * count);

            List<PlanoClassificacaoModel> planosClassificacaoModel = _core.Search(_guidProdest, page, count);
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
