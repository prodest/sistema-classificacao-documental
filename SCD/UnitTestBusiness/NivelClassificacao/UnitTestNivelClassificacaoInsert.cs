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
    public class UnitTestNivelClassificacaoInsert
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

        [TestMethod]
        public async Task NivelClassificacaoTestInsertNull()
        {
            bool ok = false;

            try
            {
                await _core.InsertAsync(null);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O Nivel de Classificação não pode ser nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com objeto nulo.");
        }

        #region Descrição
        [TestMethod]
        public async Task NivelClassificacaoTestInsertWithDescricaoNull()
        {
            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel();

            bool ok = false;

            try
            {
                await _core.InsertAsync(nivelClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição nula.");
        }

        [TestMethod]
        public async Task NivelClassificacaoTestInsertWithDescricaoEmpty()
        {
            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel { Descricao = "" };

            bool ok = false;

            try
            {
                await _core.InsertAsync(nivelClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição vazia.");
        }

        [TestMethod]
        public async Task NivelClassificacaoTestInsertWithDescricaoTrimEmpty()
        {
            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel { Descricao = " " };

            bool ok = false;

            try
            {
                await _core.InsertAsync(nivelClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição vazia.");
        }
        #endregion

        #region Id
        [TestMethod]
        public async Task NivelClassificacaoTestInsertWithInvalidInsertId()
        {
            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel { Id = 1, Descricao = "Teste", Organizacao = new OrganizacaoModel { GuidOrganizacao = new Guid(_guidGees) } };

            bool ok = false;

            try
            {
                await _core.InsertAsync(nivelClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O id não deve ser preenchido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com o id inválido para inserção.");
        }
        #endregion

        [TestMethod]
        public async Task NivelClassificacaoTestInsert()
        {
            string descricao = "Descrição Teste";
            Guid guidOrganizacao = new Guid(_guidGees);

            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel { Descricao = descricao, Organizacao = new OrganizacaoModel { GuidOrganizacao = guidOrganizacao } };

            nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);

            Assert.IsTrue(nivelClassificacaoModel.Id > 0);
            Assert.AreEqual(nivelClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(nivelClassificacaoModel.Ativo, true);
            Assert.IsFalse(nivelClassificacaoModel.Organizacao == null);
            Assert.AreEqual(nivelClassificacaoModel.Organizacao.GuidOrganizacao, guidOrganizacao);
        }
    }
}