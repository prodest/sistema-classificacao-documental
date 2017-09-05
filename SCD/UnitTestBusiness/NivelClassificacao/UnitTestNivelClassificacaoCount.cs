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

namespace Prodest.Scd.NivelClassificacao.UnitTestBusiness
{
    [TestClass]
    public class UnitTestNivelClassificacaoCount
    {
        private string _guidGees = Environment.GetEnvironmentVariable("guidGEES");
        private NivelClassificacaoCore _core;

        [TestInitialize]
        public async Task Setup()
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

            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel { Descricao = "Descrição Teste", Organizacao = new OrganizacaoModel { GuidOrganizacao = new Guid(_guidGees) } };

            await _core.InsertAsync(nivelClassificacaoModel);
        }

        #region Guid Organização
        [TestMethod]
        public void TestCountWithGuidOrganizacaoNull()
        {
            bool ok = false;

            try
            {
                _core.Count(null);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter contado com guid da organização nulo.");
        }

        [TestMethod]
        public void TestCountWithGuidOrganizacaoEmpty()
        {
            bool ok = false;

            try
            {
                _core.Count("");

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter contado com guid da organização vazio.");
        }

        [TestMethod]
        public void TestCountWithGuidOrganizacaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                _core.Count(" ");

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter contado com o guid da organização sendo um guid vazio.");
        }

        [TestMethod]
        public void TestCountWithGuidOrganizacaoNotGuid()
        {
            bool ok = false;

            try
            {
                _core.Count("ABC");

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter contado com o guid da organização não válido.");
        }

        [TestMethod]
        public void TestCountWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                _core.Count(new Guid().ToString());

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
        public void TestCountWithGuidOrganizacaoNonexistentOnDataBase()
        {
            int count = _core.Count(Guid.NewGuid().ToString());
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void TestCountWithGuidOrganizacaoCorrect()
        {
            int count = _core.Count(_guidGees);
            Assert.IsTrue(count > 0);
        }
    }
}