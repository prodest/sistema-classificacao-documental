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

                Assert.AreEqual(ex.Message, "O Nivel de Classificação não pode ser nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com objeto nulo.");
        }

        #region Descrição
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com descrição nula.");
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com descrição vazia.");
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com descrição somente com espaço.");
        }
        #endregion

        #region Organização
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

                Assert.AreEqual(ex.Message, "Não é possível atualizar a Organização do Nível de Classificação.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com o guid da organização não existindo no sistema.");
        }
        #endregion

        [TestMethod]
        public async Task NivelClassificacaoTestUpdate()
        {
            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel
            {
                Descricao = "Descrição Teste",
                Organizacao = new OrganizacaoModel { GuidOrganizacao = _guidGees }
            };

            nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);

            int id = nivelClassificacaoModel.Id;
            string descricao = "TestUpdateWithBasicsFieldsDescrição Teste";
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