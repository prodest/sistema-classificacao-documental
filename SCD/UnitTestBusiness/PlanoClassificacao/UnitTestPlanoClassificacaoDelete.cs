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
    public class UnitTestPlanoClassificacaoDelete
    {
        private Guid _guidProdest = new Guid(Environment.GetEnvironmentVariable("GuidProdest"));
        private PlanoClassificacaoCore _core;

        [TestInitialize]
        public void Setup()
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

            EFScdRepositories scdRepositories = new EFScdRepositories(mapper);

            _core = new PlanoClassificacaoCore(scdRepositories, organogramaService, organizacaoCore);
        }

        #region Id
        [TestMethod]
        public async Task PlanoClassificacaoTestDeleteWithInvalidId()
        {
            bool ok = false;

            try
            {
                await _core.DeleteAsync(default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O id n�o pode ser nulo ou vazio.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter exclu�do com id zero.");
        }
        #endregion

        [TestMethod]
        public async Task PlanoClassificacaoTestDeleteWithIdNonexistentOnDataBase()
        {
            bool ok = false;

            try
            {
                await _core.DeleteAsync(-1);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Plano de Classifica��o n�o encontrado.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquisado com id inexistente na base de dados.");
        }


        [TestMethod]
        public async Task PlanoClassificacaoTestDeletePlanoClassificacaoPublicado()
        {
            DateTime now = DateTime.Now;
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descri��o Teste",
                AreaFim = true,
                Aprovacao = now,
                Publicacao = now,
                InicioVigencia = now
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            bool ok = false;

            try
            {
                await _core.DeleteAsync(planoClassificacaoModel.Id);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O Plano de Classifica��o possui data de publica��o e n�o pode ser exclu�do.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter exclui�do com data de publica��o.");
        }

        //TODO: Ap�s a implementa��o dos CRUDs de Itens de Plnao de Classificaca��o fazer os testes de remo��o de plano de classifica��o com itens associados

        [TestMethod]
        public async Task PlanoClassificacaoTestDeletehWithIdCorrect()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descri��o Teste",
                AreaFim = true
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            await _core.DeleteAsync(planoClassificacaoModel.Id);

            bool ok = false;

            try
            {
                await _core.SearchAsync(planoClassificacaoModel.Id);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Plano de Classifica��o n�o encontrado.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter encontrado um Plano de Classifica��o exclu�do.");
        }
    }
}
