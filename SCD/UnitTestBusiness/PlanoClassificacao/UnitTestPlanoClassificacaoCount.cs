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
using System.Threading.Tasks;

namespace Prodest.Scd.PlanoClassificacao.UnitTestBusiness
{
    [TestClass]
    public class UnitTestPlanoClassificacaoCount
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
            string descricao = "Descri��o Teste";
            bool areaFim = true;
            string guidOrganizacao = _guidProdest;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = codigo, Descricao = descricao, AreaFim = areaFim, GuidOrganizacao = guidOrganizacao };

            await _core.InsertAsync(planoClassificacaoModel);
        }

        #region Guid Organiza��o
        [TestMethod]
        public void TestCountWithGuidOrganizacaoNull()
        {
            bool ok = false;

            try
            {
                _core.Count(null);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organiza��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquisado com guid da organiza��o nulo.");
        }

        [TestMethod]
        public void TestCountWithGuidOrganizacaoEmpty()
        {
            bool ok = false;

            try
            {
                _core.Count("");

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organiza��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquisado com guid da organiza��o vazio.");
        }

        [TestMethod]
        public void TestCountWithGuidOrganizacaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                _core.Count(" ");

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organiza��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquisado com o guid da organiza��o sendo um guid vazio.");
        }

        [TestMethod]
        public void TestCountWithGuidOrganizacaoNotGuid()
        {
            bool ok = false;

            try
            {
                _core.Count("ABC");

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organiza��o inv�lido.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquisado com o guid da organiza��o n�o v�lido.");
        }

        [TestMethod]
        public void TestCountWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                _core.Count(new Guid().ToString());

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organiza��o inv�lido.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquido com o guid da organiza��o sendo um guid vazio.");
        }
        #endregion

        [TestMethod]
        public void TestCountWithGuidOrganizacaoNonexistentOnDataBase()
        {
            int count = _core.Count(Guid.NewGuid().ToString());
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void TestCountWithGuidOrganizacaoCorrect()
        {
            int count = _core.Count(_guidProdest);
            Assert.IsTrue(count > 0);
        }
    }
}