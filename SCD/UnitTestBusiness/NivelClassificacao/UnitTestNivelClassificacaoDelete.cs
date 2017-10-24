using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Configuration;
using Prodest.Scd.Infrastructure.Repository;
using Prodest.Scd.Infrastructure.Repository.Specific;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.NivelClassificacao
{
    [TestClass]
    public class UnitTestNivelClassificacaoDelete
    {
        private string _guidGees = Environment.GetEnvironmentVariable("GuidGEES");
        private NivelClassificacaoCore _core;

        [TestInitialize]
        public void Setup()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<InfrastructureProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            EFScdRepositories repositories = new EFScdRepositories(mapper);

            NivelClassificacaoValidation nivelClassificacaoValidation = new NivelClassificacaoValidation();

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation);

            _core = new NivelClassificacaoCore(repositories, nivelClassificacaoValidation, organizacaoCore);
        }

        #region Id
        [TestMethod]
        public async Task NivelClassificacaoTestDeleteWithInvalidId()
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
        public async Task NivelClassificacaoTestDeleteWithIdNonexistentOnDataBase()
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

                Assert.AreEqual(ex.Message, "Nivel de Classifica��o n�o encontrado.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquisado com id inexistente na base de dados.");
        }

        //TODO: Ap�s a implementa��o dos CRUDs de Itens de Plnao de Classificaca��o fazer os testes de remo��o de nivel de classifica��o com itens associados

        [TestMethod]
        public async Task NivelClassificacaoTestDeleteWithIdCorrect()
        {
            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel
            {
                Descricao = "Descri��o Teste",
                Organizacao = new OrganizacaoModel { GuidOrganizacao = new Guid(_guidGees) }
            };

            nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);

            await _core.DeleteAsync(nivelClassificacaoModel.Id);

            bool ok = false;

            try
            {
                await _core.SearchAsync(nivelClassificacaoModel.Id);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Nivel de Classifica��o n�o encontrado.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter encontrado um Nivel de Classifica��o exclu�do.");
        }
    }
}
