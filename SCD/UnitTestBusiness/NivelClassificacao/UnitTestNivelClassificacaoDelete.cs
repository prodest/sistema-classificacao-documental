using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Configuration;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Repository;
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
            ScdRepositories repositories = new ScdRepositories();

            NivelClassificacaoValidation nivelClassificacaoValidation = new NivelClassificacaoValidation(repositories);

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BusinessProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation, mapper);

            _core = new NivelClassificacaoCore(repositories, nivelClassificacaoValidation, mapper, organizacaoCore);
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

                Assert.AreEqual(ex.Message, "O id não pode ser nulo ou vazio.");
            }

            if (ok)
                Assert.Fail("Não deveria ter excluído com id zero.");
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

                Assert.AreEqual(ex.Message, "Nivel de Classificação não encontrado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com id inexistente na base de dados.");
        }

        //TODO: Após a implementação dos CRUDs de Itens de Plnao de Classificacação fazer os testes de remoção de nivel de classificação com itens associados

        [TestMethod]
        public async Task NivelClassificacaoTestDeletehWithIdCorrect()
        {
            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel
            {
                Descricao = "Descrição Teste",
                Organizacao = new OrganizacaoModel { GuidOrganizacao = new Guid(_guidGees) }
            };

            nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);

            await _core.DeleteAsync(nivelClassificacaoModel.Id);

            bool ok = false;

            try
            {
                _core.Search(nivelClassificacaoModel.Id);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Nivel de Classificação não encontrado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter encontrado um Nivel de Classificação excluído.");
        }
    }
}
