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

            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel { Descricao = "Descri��o Teste", Organizacao = new OrganizacaoModel { GuidOrganizacao = new Guid(_guidGees) } };

            await _core.InsertAsync(nivelClassificacaoModel);
        }

        #region Guid Organiza��o
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

                Assert.AreEqual(ex.Message, "A organiza��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter contado com guid da organiza��o nulo.");
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

                Assert.AreEqual(ex.Message, "A organiza��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter contado com guid da organiza��o vazio.");
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

                Assert.AreEqual(ex.Message, "A organiza��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter contado com o guid da organiza��o sendo um guid vazio.");
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

                Assert.AreEqual(ex.Message, "Guid da organiza��o inv�lido.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter contado com o guid da organiza��o n�o v�lido.");
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

                Assert.AreEqual(ex.Message, "Guid da organiza��o inv�lido.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquido com o guid da organiza��o sendo um guid vazio.");
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