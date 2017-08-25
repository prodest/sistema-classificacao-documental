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
        private PlanoClassificacaoCore _core;

        [TestInitialize]
        public void Setup()
        {
            ScdRepositories repositories = new ScdRepositories();

            PlanoClassificacaoValidation planoClassificacaoValidation = new PlanoClassificacaoValidation(repositories);

            IOptions<AcessoCidadaoConfiguration> autenticacaoIdentityServerConfig = Options.Create(new AcessoCidadaoConfiguration { Authority = "https://acessocidadao.es.gov.br/is/" });

            AcessoCidadaoClientAccessToken acessoCidadaoClientAccessToken = new AcessoCidadaoClientAccessToken(autenticacaoIdentityServerConfig);

            OrganogramaService organogramaService = new OrganogramaService(acessoCidadaoClientAccessToken);

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation, mapper);

            _core = new PlanoClassificacaoCore(repositories, planoClassificacaoValidation, mapper, organogramaService, organizacaoCore);
        }

        #region Guid Organização
        [TestMethod]
        public async Task TestSearchWithGuidOrganizacaoNull()
        {
            try
            {
                await _core.SearchAsync(null);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }
        }

        [TestMethod]
        public async Task TestSearchWithGuidOrganizacaoEmpty()
        {
            try
            {
                await _core.SearchAsync("");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }
        }

        [TestMethod]
        public async Task TestSearchWithGuidOrganizacaoTrimEmpty()
        {
            try
            {
                await _core.SearchAsync(" ");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }
        }

        [TestMethod]
        public async Task TestSearchWithGuidOrganizacaoNotGuid()
        {
            try
            {
                await _core.SearchAsync("ABC");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }
        }

        [TestMethod]
        public async Task TestSearchWithGuidOrganizacaoGuidEmpty()
        {
            try
            {
                await _core.SearchAsync(new Guid().ToString());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }
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
            List<PlanoClassificacaoModel> planosClassificacaoModel = await _core.SearchAsync(Environment.GetEnvironmentVariable("GuidProdest"));
            Assert.IsNotNull(planosClassificacaoModel);
            Assert.IsTrue(planosClassificacaoModel.Count > 0);

            foreach (PlanoClassificacaoModel pcm in planosClassificacaoModel)
            {
                Assert.IsTrue(!string.IsNullOrWhiteSpace(pcm.Codigo));
                Assert.IsTrue(!string.IsNullOrWhiteSpace(pcm.Descricao));
                Assert.IsTrue(!string.IsNullOrWhiteSpace(pcm.GuidOrganizacao));
            }
        }
    }
}
