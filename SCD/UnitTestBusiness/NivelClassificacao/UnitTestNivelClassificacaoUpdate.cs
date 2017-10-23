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
    public class UnitTestNivelClassificacaoUpdate
    {
        private Guid _guidGees = new Guid(Environment.GetEnvironmentVariable("GuidGEES"));
        private NivelClassificacaoCore _core;
        private NivelClassificacaoModel _nivelClassificacaoModel;

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

            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel { Descricao = "Teste", Organizacao = new OrganizacaoModel { GuidOrganizacao = _guidGees } };

            _nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);
        }

        [TestMethod]
        public async Task NivelClassificacaoTestUpdateNull()
        {
            bool ok = false;

            try
            {
                await _core.UpdateAsync(null);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O Nivel de Classifica��o n�o pode ser nulo.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com objeto nulo.");
        }

        #region Descri��o
        [TestMethod]
        public async Task NivelClassificacaoTestUpdateWithDescricaoNull()
        {
            bool ok = false;
            try
            {
                _nivelClassificacaoModel.Descricao = null;

                await _core.UpdateAsync(_nivelClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descri��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com descri��o nula.");
        }

        [TestMethod]
        public async Task NivelClassificacaoTestUpdateWithDescricaoEmpty()
        {
            bool ok = false;

            try
            {
                _nivelClassificacaoModel.Descricao = "";

                await _core.UpdateAsync(_nivelClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descri��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com descri��o vazia.");
        }

        [TestMethod]
        public async Task NivelClassificacaoTestUpdateWithDescricaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                _nivelClassificacaoModel.Descricao = " ";

                await _core.UpdateAsync(_nivelClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descri��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com descri��o somente com espa�o.");
        }
        #endregion

        #region Organiza��o
        [TestMethod]
        public async Task NivelClassificacaoTestUpdateGuidOrganizacao()
        {
            bool ok = false;

            try
            {
                _nivelClassificacaoModel.Organizacao.GuidOrganizacao = Guid.NewGuid();

                await _core.UpdateAsync(_nivelClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));

                Assert.AreEqual(ex.Message, "N�o � poss�vel atualizar a Organiza��o do N�vel de Classifica��o.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com o guid da organiza��o n�o existindo no sistema.");
        }
        #endregion

        [TestMethod]
        public async Task NivelClassificacaoTestUpdate()
        {
            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel
            {
                Descricao = "Descri��o Teste",
                Organizacao = new OrganizacaoModel { GuidOrganizacao = _guidGees }
            };

            nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);

            int id = nivelClassificacaoModel.Id;
            string descricao = "TestUpdateWithBasicsFieldsDescri��o Teste";
            bool ativo = false;

            nivelClassificacaoModel.Descricao = descricao;
            nivelClassificacaoModel.Ativo = ativo;

            await _core.UpdateAsync(nivelClassificacaoModel);

            nivelClassificacaoModel = await _core.SearchAsync(nivelClassificacaoModel.Id);

            Assert.AreEqual(nivelClassificacaoModel.Id, id);
            Assert.AreEqual(nivelClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(nivelClassificacaoModel.Ativo, ativo);
            Assert.AreEqual(nivelClassificacaoModel.Organizacao.GuidOrganizacao, _guidGees);
        }
    }
}