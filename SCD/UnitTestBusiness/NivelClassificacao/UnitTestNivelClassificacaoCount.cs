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
    public class UnitTestNivelClassificacaoCount
    {
        private Guid _guidGees = new Guid(Environment.GetEnvironmentVariable("guidGEES"));
        private NivelClassificacaoCore _core;

        [TestInitialize]
        public async Task Setup()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BusinessProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            ScdRepositories repositories = new ScdRepositories(mapper);

            NivelClassificacaoValidation nivelClassificacaoValidation = new NivelClassificacaoValidation(repositories);

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation, mapper);

            _core = new NivelClassificacaoCore(repositories, nivelClassificacaoValidation, mapper, organizacaoCore);

            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel { Descricao = "Descrição Teste", Organizacao = new OrganizacaoModel { GuidOrganizacao = _guidGees } };

            await _core.InsertAsync(nivelClassificacaoModel);
        }

        #region Guid Organização
        [TestMethod]
        public void NivelClassificacaoTestCountWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                _core.Count(Guid.Empty);

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
        public void NivelClassificacaoTestCountWithGuidOrganizacaoNonexistentOnDataBase()
        {
            int count = _core.Count(Guid.NewGuid());
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void NivelClassificacaoTestCountWithGuidOrganizacaoCorrect()
        {
            int count = _core.Count(_guidGees);
            Assert.IsTrue(count > 0);
        }
    }
}