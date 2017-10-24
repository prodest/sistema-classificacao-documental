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
    public class UnitTestNivelClassificacaoCount
    {
        private Guid _guidGees = new Guid(Environment.GetEnvironmentVariable("guidGEES"));
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

            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel { Descricao = "Descrição Teste", Organizacao = new OrganizacaoModel { GuidOrganizacao = _guidGees } };

            await _core.InsertAsync(nivelClassificacaoModel);
        }

        #region Guid Organização
        [TestMethod]
        public async Task NivelClassificacaoTestCountWithGuidOrganizacaoGuidEmpty()
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
                Assert.Fail("Não deveria ter pesquido com o guid da organização sendo um guid vazio.");
        }
        #endregion

        [TestMethod]
        public async Task NivelClassificacaoTestCountWithGuidOrganizacaoNonexistentOnDataBase()
        {
            int count = await _core.CountAsync(Guid.NewGuid());
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public async Task NivelClassificacaoTestCountWithGuidOrganizacaoCorrect()
        {
            int count = await _core.CountAsync(_guidGees);
            Assert.IsTrue(count > 0);
        }
    }
}