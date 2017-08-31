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
            string descricao = "Descrição Teste";
            bool areaFim = true;
            string guidOrganizacao = _guidProdest;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = codigo, Descricao = descricao, AreaFim = areaFim, GuidOrganizacao = guidOrganizacao };

            await _core.InsertAsync(planoClassificacaoModel);
        }

        #region Guid Organização
        [TestMethod]
        public async Task TestCountWithGuidOrganizacaoNull()
        {
            bool ok = false;

            try
            {
                await _core.CountAsync(null);

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
        public async Task TestCountWithGuidOrganizacaoEmpty()
        {
            bool ok = false;

            try
            {
                await _core.CountAsync("");

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
        public async Task TestCountWithGuidOrganizacaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                await _core.CountAsync(" ");

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
        public async Task TestCountWithGuidOrganizacaoNotGuid()
        {
            bool ok = false;

            try
            {
                await _core.CountAsync("ABC");

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
        public async Task TestCountWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                await _core.CountAsync(new Guid().ToString());

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
        public async Task TestCountWithGuidOrganizacaoNonexistentOnDataBase()
        {
            int count = await _core.CountAsync(Guid.NewGuid().ToString());
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public async Task TestCountWithGuidOrganizacaoCorrect()
        {
            int count = await _core.CountAsync(_guidProdest);
            Assert.IsTrue(count > 0);
        }
    }
}
