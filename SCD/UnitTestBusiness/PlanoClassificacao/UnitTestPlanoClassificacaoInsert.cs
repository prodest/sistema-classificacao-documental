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
        public async Task TestInsertNull()
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
        public async Task TestInsertWithCodigoNull()
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
        public async Task TestInsertWithCodigoEmpty()
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
        public async Task TestInsertWithCodigoTrimEmpty()
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
        public async Task TestInsertWithDescricaoNull()
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
        public async Task TestInsertWithDescricaoEmpty()
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
        public async Task TestInsertWithDescricaoTrimEmpty()
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

        #region Guid Organiza��o
        [TestMethod]
        public async Task TestInsertWithGuidOrganizacaoNull()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste" };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organiza��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com guid da organiza��o nulo.");
        }

        [TestMethod]
        public async Task TestInsertWithGuidOrganizacaoGuidEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste", GuidOrganizacao = Guid.Empty };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organiza��o inv�lido.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com o guid da organiza��o sendo um guid vazio.");
        }

        [TestMethod]
        public async Task TestInsertWithGuidOrganizacaoNonexistentOnOrganograma()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste", GuidOrganizacao = Guid.NewGuid() };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));

                Assert.IsTrue(ex.Message.Contains("N�o foi poss�vel obter os dados do servi�o."));
            }

            if (ok)
                Assert.Fail("N�o deveria ter inserido com o guid da organiza��o n�o existindo no sistema de organograma.");
        }
        #endregion

        #region Publica��o
        [TestMethod]
        public async Task TestInsertWithPublicacaoWithoutAprovacao()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste", GuidOrganizacao = Guid.NewGuid(), Publicacao = now };

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
        public async Task TestInsertWithPublicacaoWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste", GuidOrganizacao = Guid.NewGuid(), Aprovacao = now, Publicacao = now };

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
        public async Task TestInsertWithPublicacaoBeforeAprovacao()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste", GuidOrganizacao = Guid.NewGuid(), Aprovacao = now, Publicacao = now.AddDays(-1), InicioVigencia = now };

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
        public async Task TestInsertWithFimVigenciaWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste", GuidOrganizacao = Guid.NewGuid(), FimVigencia = now };

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
        public async Task TestInsertWithFimVigenciaBeforeInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descri��o Teste", GuidOrganizacao = Guid.NewGuid(), InicioVigencia = now, FimVigencia = now.AddDays(-1) };

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
        public async Task TestInsertWithInvalidInsertId()
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
        public async Task TestInsertWithBasicsFields()
        {
            string codigo = "01";
            string descricao = "Descri��o Teste";
            bool areaFim = true;
            Guid guidOrganizacao = _guidProdest;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = codigo, Descricao = descricao, AreaFim = areaFim, GuidOrganizacao = guidOrganizacao };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            Assert.IsTrue(planoClassificacaoModel.Id > 0);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
        }

        [TestMethod]
        public async Task TestInsertWithAprovacao()
        {
            DateTime now = DateTime.Now;

            string codigo = "01";
            string descricao = "Descri��o Teste";
            bool areaFim = true;
            Guid guidOrganizacao = _guidProdest;
            DateTime aprovacao = now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = codigo,
                Descricao = descricao,
                AreaFim = areaFim,
                GuidOrganizacao = guidOrganizacao,
                Aprovacao = aprovacao
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            Assert.IsTrue(planoClassificacaoModel.Id > 0);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
        }

        [TestMethod]
        public async Task TestInsertWithoutFimVigencia()
        {
            DateTime now = DateTime.Now;

            string codigo = "01";
            string descricao = "Descri��o Teste";
            bool areaFim = true;
            Guid guidOrganizacao = _guidProdest;
            DateTime aprovacao = now;
            DateTime publicacao = now;
            DateTime inicioVigecia = now;


            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = codigo,
                Descricao = descricao,
                AreaFim = areaFim,
                GuidOrganizacao = guidOrganizacao,
                Aprovacao = aprovacao,
                Publicacao = publicacao,
                InicioVigencia = inicioVigecia
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            Assert.IsTrue(planoClassificacaoModel.Id > 0);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
            Assert.AreEqual(planoClassificacaoModel.Publicacao, publicacao);
            Assert.AreEqual(planoClassificacaoModel.InicioVigencia, inicioVigecia);
        }

        [TestMethod]
        public async Task TestInsertWithCompleteFields()
        {
            DateTime now = DateTime.Now;

            string codigo = "01";
            string descricao = "Descri��o Teste";
            bool areaFim = true;
            Guid guidOrganizacao = _guidProdest;
            DateTime aprovacao = now;
            DateTime publicacao = now;
            DateTime inicioVigecia = now;
            DateTime fimVigencia = now;


            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = codigo,
                Descricao = descricao,
                AreaFim = areaFim,
                GuidOrganizacao = guidOrganizacao,
                Aprovacao = aprovacao,
                Publicacao = publicacao,
                InicioVigencia = inicioVigecia,
                FimVigencia = fimVigencia
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            Assert.IsTrue(planoClassificacaoModel.Id > 0);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
            Assert.AreEqual(planoClassificacaoModel.Publicacao, publicacao);
            Assert.AreEqual(planoClassificacaoModel.InicioVigencia, inicioVigecia);
            Assert.AreEqual(planoClassificacaoModel.FimVigencia, fimVigencia);
        }
    }
}
