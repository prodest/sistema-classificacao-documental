using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Configuration;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Integration;
using Prodest.Scd.Infrastructure.Repository;
using Prodest.Scd.Web.Configuration;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.PlanoClassificacao
{
    [TestClass]
    public class UnitTestPlanoClassificacaoInsert
    {
        private Guid _guidProdest = new Guid(Environment.GetEnvironmentVariable("GuidProdest"));
        private PlanoClassificacaoCore _core;

        [TestInitialize]
        public void Setup()
        {
            ScdRepositories repositories = new ScdRepositories();

            PlanoClassificacaoValidation planoClassificacaoValidation = new PlanoClassificacaoValidation(repositories);

            IOptions<AcessoCidadaoConfiguration> autenticacaoIdentityServerConfig = Options.Create(new AcessoCidadaoConfiguration { Authority = "https://acessocidadao.es.gov.br/is/" });

            AcessoCidadaoClientAccessToken acessoCidadaoClientAccessToken = new AcessoCidadaoClientAccessToken(autenticacaoIdentityServerConfig);

            OrganogramaService organogramaService = new OrganogramaService(acessoCidadaoClientAccessToken);

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BusinessProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation, mapper);

            _core = new PlanoClassificacaoCore(repositories, planoClassificacaoValidation, mapper, organogramaService, organizacaoCore);
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertNull()
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

                Assert.AreEqual(ex.Message, "O Plano de Classifica��o n�o pode ser nulo.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com objeto nulo.");
        }

        #region C�digo
        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithCodigoNull()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel();

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O c�digo n�o pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com c�digo nulo.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithCodigoEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "" };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O c�digo n�o pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com c�digo vazio.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithCodigoTrimEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = " " };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O c�digo n�o pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com c�digo somente com espa�o.");
        }
        #endregion

        #region Descri��o
        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithDescricaoNull()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01" };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descri��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com descri��o nula.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithDescricaoEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "" };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descri��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com descri��o vazia.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithDescricaoTrimEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = " " };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descri��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com descri��o vazia.");
        }
        #endregion

        #region Publica��o
        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithPublicacaoWithoutAprovacao()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste", Publicacao = now };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de aprova��o n�o pode ser vazia ou nula quando existe uma data de publica��o.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com data de publica��o e sem data de aprova��o.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithPublicacaoWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste", Aprovacao = now, Publicacao = now };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de in�cio de vig�ncia n�o pode ser vazia ou nula quando existe uma data de publica��o.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com data de publica��o e sem data de in�cio de vig�ncia.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithPublicacaoBeforeAprovacao()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste", Aprovacao = now, Publicacao = now.AddDays(-1), InicioVigencia = now };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de publica��o deve ser maior ou igual � data de aprova��o.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com data de publica��o anterior � data de aprova��o.");
        }
        #endregion

        #region Fim de Vig�ncia
        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithFimVigenciaWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste", FimVigencia = now };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de in�cio de vig�ncia n�o pode ser vazia ou nula quando existe uma data de fim de vig�ncia.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com data de fim de vig�ncia e sem data de in�cio de vig�ncia.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithFimVigenciaBeforeInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste", InicioVigencia = now, FimVigencia = now.AddDays(-1) };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de fim de vig�ncia deve ser maior ou igual � data de in�cio de vig�ncia.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com data de fim de vig�ncia anterior � data de in�cio de vig�ncia.");
        }
        #endregion

        #region Id
        public async Task PlanoClassificacaoTestInsertWithInvalidInsertId()
        {
            string codigo = "01";
            string descricao = "Descri��o Teste";
            bool areaFim = true;
            Guid guidOrganizacao = _guidProdest;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Id = 1, Codigo = codigo, Descricao = descricao, AreaFim = areaFim, GuidOrganizacao = guidOrganizacao };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O id n�o deve ser preenchido.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com o id inv�lido para inser��o.");
        }
        #endregion

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithBasicsFields()
        {
            string codigo = "01";
            string descricao = "Descri��o Teste";
            bool areaFim = true;
            Guid guidOrganizacao = _guidProdest;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = codigo, Descricao = descricao, AreaFim = areaFim };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            Assert.IsTrue(planoClassificacaoModel.Id > 0);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            //Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithAprovacao()
        {
            DateTime now = DateTime.Now;

            string codigo = "01";
            string descricao = "Descri��o Teste";
            bool areaFim = true;
            DateTime aprovacao = now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = codigo,
                Descricao = descricao,
                AreaFim = areaFim,
                Aprovacao = aprovacao
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            Assert.IsTrue(planoClassificacaoModel.Id > 0);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            //Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithoutFimVigencia()
        {
            DateTime now = DateTime.Now;

            string codigo = "01";
            string descricao = "Descri��o Teste";
            bool areaFim = true;
            DateTime aprovacao = now;
            DateTime publicacao = now;
            DateTime inicioVigecia = now;


            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = codigo,
                Descricao = descricao,
                AreaFim = areaFim,
                Aprovacao = aprovacao,
                Publicacao = publicacao,
                InicioVigencia = inicioVigecia
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            Assert.IsTrue(planoClassificacaoModel.Id > 0);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            //Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
            Assert.AreEqual(planoClassificacaoModel.Publicacao, publicacao);
            Assert.AreEqual(planoClassificacaoModel.InicioVigencia, inicioVigecia);
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithCompleteFields()
        {
            DateTime now = DateTime.Now;

            string codigo = "01";
            string descricao = "Descri��o Teste";
            bool areaFim = true;
            DateTime aprovacao = now;
            DateTime publicacao = now;
            DateTime inicioVigecia = now;
            DateTime fimVigencia = now;


            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = codigo,
                Descricao = descricao,
                AreaFim = areaFim,
                Aprovacao = aprovacao,
                Publicacao = publicacao,
                InicioVigencia = inicioVigecia,
                FimVigencia = fimVigencia
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            Assert.IsTrue(planoClassificacaoModel.Id > 0);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            //Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
            Assert.AreEqual(planoClassificacaoModel.Publicacao, publicacao);
            Assert.AreEqual(planoClassificacaoModel.InicioVigencia, inicioVigecia);
            Assert.AreEqual(planoClassificacaoModel.FimVigencia, fimVigencia);
        }
    }
}
