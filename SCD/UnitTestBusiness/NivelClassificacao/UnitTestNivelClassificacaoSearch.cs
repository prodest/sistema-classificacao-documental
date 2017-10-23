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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.NivelClassificacao
{
    [TestClass]
    public class UnitTestNivelClassificacaoSearch
    {
        private Guid _guidGees = new Guid(Environment.GetEnvironmentVariable("GuidGEES"));
        private NivelClassificacaoCore _core;

        [TestInitialize]
        public async Task Setup()
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

            string descricao = "Descrição Teste";
            Guid guidOrganizacao = _guidGees;

            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel { Descricao = descricao, Organizacao = new OrganizacaoModel { GuidOrganizacao = guidOrganizacao } };

            await _core.InsertAsync(nivelClassificacaoModel);
        }

        #region Search by Id
        #region Id
        [TestMethod]
        public async Task NivelClassificacaoTestSearchWithInvalidId()
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
        public async Task ivelClassificacaoTestSearchWithIdNonexistentOnDataBase()
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

                Assert.AreEqual(ex.Message, "Nivel de Classificação não encontrado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com id inexistente na base de dados.");
        }

        [TestMethod]
        public async Task NivelClassificacaoTestSearchWithIdCorrect()
        {
            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel
            {
                Descricao = "Descrição Teste",
                Organizacao = new OrganizacaoModel { GuidOrganizacao = _guidGees }
            };

            nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);

            NivelClassificacaoModel nivelClassificacaoModelSearched = await _core.SearchAsync(nivelClassificacaoModel.Id);

            Assert.AreEqual(nivelClassificacaoModel.Id, nivelClassificacaoModelSearched.Id);
            Assert.AreEqual(nivelClassificacaoModel.Descricao, nivelClassificacaoModelSearched.Descricao);
            Assert.IsTrue(nivelClassificacaoModel.Ativo);
            Assert.AreEqual(nivelClassificacaoModel.Organizacao.GuidOrganizacao, nivelClassificacaoModelSearched.Organizacao.GuidOrganizacao);
        }
        #endregion
                
        #region Pagination Search by GuidOrganização
        #region Guid Organização
        [TestMethod]
        public async Task NivelClassificacaoTestPaginationSearchWithGuidOrganizacaoGuidEmpty()
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
        public async Task NivelClassificacaoTestPaginationSearchWithInvalidPage()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(_guidGees, default(int), default(int));

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
        public async Task NivelClassificacaoTestPaginationSearchWithInvalidCount()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(_guidGees, 1, default(int));

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
        public async Task NivelClassificacaoTestPaginationSearchWithGuidOrganizacaoNonexistentOnDataBase()
        {
            ICollection<NivelClassificacaoModel> niveisClassificacaoModel = await _core.SearchAsync(Guid.NewGuid(), 1, 1);
            Assert.IsNotNull(niveisClassificacaoModel);
            Assert.IsTrue(niveisClassificacaoModel.Count == 0);
        }

        [TestMethod]
        public async Task NivelClassificacaoTestPaginationSearch()
        {
            int page = 2;
            int count = 5;

            await InsertNiveisClassificacao(page * count);

            ICollection<NivelClassificacaoModel> niveisClassificacaoModel = await _core.SearchAsync(_guidGees, page, count);
            Assert.IsNotNull(niveisClassificacaoModel);
            Assert.IsTrue(niveisClassificacaoModel.Count == count);

            foreach (NivelClassificacaoModel pcm in niveisClassificacaoModel)
            {
                Assert.IsFalse(pcm.Id == default(int));
                Assert.IsFalse(string.IsNullOrWhiteSpace(pcm.Descricao));
                Assert.IsFalse(Guid.Empty.Equals(pcm.Organizacao.GuidOrganizacao));
            }
        }
        #endregion

        private async Task InsertNiveisClassificacao(int count)
        {
            for (int i = 0; i < count; i++)
            {
                string descricao = "Descrição Teste";
                Guid guidOrganizacao = _guidGees;

                NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel
                {
                    Descricao = descricao,
                    Organizacao = new OrganizacaoModel { GuidOrganizacao = guidOrganizacao }
                };

                nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);
            }
        }
    }
}
