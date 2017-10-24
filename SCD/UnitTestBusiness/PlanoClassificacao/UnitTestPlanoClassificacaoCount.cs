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
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.PlanoClassificacao
{
    [TestClass]
    public class UnitTestPlanoClassificacaoCount
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

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(new EFScdRepositories(mapper), organizacaoValidation);

            _core = new PlanoClassificacaoCore(repositories, organogramaService, organizacaoCore);

            string codigo = "01";
            string descricao = "Descrição Teste";
            bool areaFim = true;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = codigo, Descricao = descricao, AreaFim = areaFim };

            await _core.InsertAsync(planoClassificacaoModel);
        }

        #region Guid Organização
        [TestMethod]
        public async Task PlanoClassificacaoTestCountWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                await _core.CountAsync(Guid.Empty);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter contado com o guid da organização sendo um guid vazio.");
        }
        #endregion

        [TestMethod]
        public async Task PlanoClassificacaoTestCountWithGuidOrganizacaoNonexistentOnDataBase()
        {
            int count = await _core.CountAsync(Guid.NewGuid());
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestCountWithGuidOrganizacaoCorrect()
        {
            int count = await _core.CountAsync(_guidProdest);
            Assert.IsTrue(count > 0);
        }
    }
}