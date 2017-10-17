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
    public class UnitTestPlanoClassificacaoUpdate
    {
        private Guid _guidProdest = new Guid(Environment.GetEnvironmentVariable("GuidProdest"));
        private Guid _guidSeger = new Guid(Environment.GetEnvironmentVariable("GuidSeger"));
        private PlanoClassificacaoCore _core;
        private PlanoClassificacaoModel _planoClassificacaoModel;
        private PlanoClassificacaoModel _planoClassificacaoPublicadoModel;

        [TestInitialize]
        public async Task Setup()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BusinessProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            ScdRepositories repositories = new ScdRepositories(mapper);

            PlanoClassificacaoValidation planoClassificacaoValidation = new PlanoClassificacaoValidation(repositories);

            IOptions<AcessoCidadaoConfiguration> autenticacaoIdentityServerConfig = Options.Create(new AcessoCidadaoConfiguration { Authority = "https://acessocidadao.es.gov.br/is/" });

            AcessoCidadaoClientAccessToken acessoCidadaoClientAccessToken = new AcessoCidadaoClientAccessToken(autenticacaoIdentityServerConfig);

            OrganogramaService organogramaService = new OrganogramaService(acessoCidadaoClientAccessToken);

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation, mapper);

            _core = new PlanoClassificacaoCore(repositories, planoClassificacaoValidation, mapper, organogramaService, organizacaoCore);

            string codigo = "01";
            string descricao = "Descri��o Teste";
            bool areaFim = true;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = codigo, Descricao = descricao, AreaFim = areaFim };

            _planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);
            _planoClassificacaoModel.Organizacao = null;
            _planoClassificacaoModel.GuidOrganizacao = Guid.Empty;

            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoPublicadoModel = new PlanoClassificacaoModel { Codigo = codigo, Descricao = descricao, AreaFim = areaFim, Aprovacao = now, Publicacao = now, InicioVigencia = now };

            _planoClassificacaoPublicadoModel = await _core.InsertAsync(planoClassificacaoPublicadoModel);
            _planoClassificacaoPublicadoModel.Organizacao = null;
            _planoClassificacaoPublicadoModel.GuidOrganizacao = Guid.Empty;
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateNull()
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

                Assert.AreEqual(ex.Message, "O Plano de Classifica��o n�o pode ser nulo.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com objeto nulo.");
        }

        #region C�digo
        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithCodigoNull()
        {
            bool ok = false;

            try
            {
                _planoClassificacaoModel.Codigo = null;

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O c�digo n�o pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com c�digo nulo.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithCodigoEmpty()
        {
            bool ok = false;

            try
            {
                _planoClassificacaoModel.Codigo = "";

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O c�digo n�o pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com c�digo vazio.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithCodigoTrimEmpty()
        {
            bool ok = false;

            try
            {
                _planoClassificacaoModel.Codigo = " ";

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O c�digo n�o pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com c�digo somente com espa�o.");
        }
        #endregion

        #region Descri��o
        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithDescricaoNull()
        {
            bool ok = false;
            try
            {
                _planoClassificacaoModel.Descricao = null;

                await _core.UpdateAsync(_planoClassificacaoModel);

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
        public async Task PlanoClassificacaoTestUpdateWithDescricaoEmpty()
        {
            bool ok = false;

            try
            {
                _planoClassificacaoModel.Descricao = "";

                await _core.UpdateAsync(_planoClassificacaoModel);

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
        public async Task PlanoClassificacaoTestUpdateWithDescricaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                _planoClassificacaoModel.Descricao = " ";

                await _core.UpdateAsync(_planoClassificacaoModel);
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

        #region Publica��o
        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithPublicacaoWithoutAprovacao()
        {
            DateTime now = DateTime.Now;

            bool ok = false;

            try
            {
                _planoClassificacaoModel.Publicacao = now;

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de aprova��o n�o pode ser vazia ou nula quando existe uma data de publica��o.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com data de publica��o e sem data de aprova��o.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithPublicacaoWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            bool ok = false;

            try
            {
                _planoClassificacaoModel.Aprovacao = now;
                _planoClassificacaoModel.Publicacao = now;

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de in�cio de vig�ncia n�o pode ser vazia ou nula quando existe uma data de publica��o.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com data de publica��o e sem data de in�cio de vig�ncia.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithPublicacaoBeforeAprovacao()
        {
            DateTime now = DateTime.Now;

            bool ok = false;

            try
            {
                _planoClassificacaoModel.Aprovacao = now;
                _planoClassificacaoModel.Publicacao = now.AddDays(-1);
                _planoClassificacaoModel.InicioVigencia = now;

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de publica��o deve ser maior ou igual � data de aprova��o.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com data de publica��o anterior � data de aprova��o.");
        }
        #endregion

        #region Fim de Vig�ncia
        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithFimVigenciaWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            bool ok = false;

            try
            {
                _planoClassificacaoModel.FimVigencia = now;

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de in�cio de vig�ncia n�o pode ser vazia ou nula quando existe uma data de fim de vig�ncia.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atulizado com data de fim de vig�ncia e sem data de in�cio de vig�ncia.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithFimVigenciaBeforeInicioVigencia()
        {
            DateTime now = DateTime.Now;

            bool ok = false;

            try
            {
                _planoClassificacaoModel.InicioVigencia = now;
                _planoClassificacaoModel.FimVigencia = now.AddDays(-1);

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de fim de vig�ncia deve ser maior ou igual � data de in�cio de vig�ncia.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com data de fim de vig�ncia anterior � data de in�cio de vig�ncia.");
        }
        #endregion

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdatePlanoClassificacaoPublicado()
        {
            int id = _planoClassificacaoPublicadoModel.Id;
            string codigo = "TestUpdateWithPublicacao" + "01";
            string descricao = "TestUpdateWithPublicacao" + "Descri��o Teste";
            bool areaFim = false;

            _planoClassificacaoPublicadoModel.Codigo = codigo;
            _planoClassificacaoPublicadoModel.Descricao = descricao;
            _planoClassificacaoPublicadoModel.AreaFim = areaFim;

            bool ok = false;

            try
            {
                await _core.UpdateAsync(_planoClassificacaoPublicadoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O Plano de Classifica��o possui data de publica��o e n�o pode ser atualizado.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com data de publica��o.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithBasicsFields()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descri��o Teste",
                AreaFim = false
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);
            planoClassificacaoModel.Organizacao = null;
            planoClassificacaoModel.GuidOrganizacao = Guid.Empty;

            int id = planoClassificacaoModel.Id;
            string codigo = "TestUpdateWithBasicsFields01";
            string descricao = "TestUpdateWithBasicsFieldsDescri��o Teste";
            bool areaFim = true;

            planoClassificacaoModel.Codigo = codigo;
            planoClassificacaoModel.Descricao = descricao;
            planoClassificacaoModel.AreaFim = areaFim;

            await _core.UpdateAsync(planoClassificacaoModel);

            planoClassificacaoModel = _core.Search(planoClassificacaoModel.Id);

            Assert.IsTrue(planoClassificacaoModel.Id == id);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(planoClassificacaoModel.AreaFim, areaFim);
            //Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.IsFalse(planoClassificacaoModel.Aprovacao.HasValue);
            Assert.IsFalse(planoClassificacaoModel.Publicacao.HasValue);
            Assert.IsFalse(planoClassificacaoModel.InicioVigencia.HasValue);
            Assert.IsFalse(planoClassificacaoModel.FimVigencia.HasValue);
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithAprovacao()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descri��o Teste",
                AreaFim = false
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);
            planoClassificacaoModel.GuidOrganizacao = Guid.Empty;
            planoClassificacaoModel.Organizacao = null;

            DateTime now = DateTime.Now;

            int id = planoClassificacaoModel.Id;
            string codigo = "TestUpdateWithBasicsFields01";
            string descricao = "TestUpdateWithBasicsFieldsDescri��o Teste";
            bool areaFim = true;
            DateTime aprovacao = now;

            planoClassificacaoModel.Codigo = codigo;
            planoClassificacaoModel.Descricao = descricao;
            planoClassificacaoModel.AreaFim = areaFim;
            planoClassificacaoModel.Aprovacao = aprovacao;

            await _core.UpdateAsync(planoClassificacaoModel);

            planoClassificacaoModel = _core.Search(planoClassificacaoModel.Id);

            Assert.IsTrue(planoClassificacaoModel.Id == id);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(planoClassificacaoModel.AreaFim, areaFim);
            //Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
            Assert.IsFalse(planoClassificacaoModel.Publicacao.HasValue);
            Assert.IsFalse(planoClassificacaoModel.InicioVigencia.HasValue);
            Assert.IsFalse(planoClassificacaoModel.FimVigencia.HasValue);
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithoutFimVigencia()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descri��o Teste",
                AreaFim = false
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);
            planoClassificacaoModel.Organizacao = null;
            planoClassificacaoModel.GuidOrganizacao = Guid.Empty;

            DateTime now = DateTime.Now;

            int id = planoClassificacaoModel.Id;
            string codigo = "TestUpdateWithoutFimVigencia01";
            string descricao = "TestUpdateWithoutFimVigenciaDescri��o Teste";
            bool areaFim = true;
            DateTime aprovacao = now;
            DateTime publicacao = now.AddDays(1);
            DateTime inicioVigencia = now.AddDays(2);

            planoClassificacaoModel.Codigo = codigo;
            planoClassificacaoModel.Descricao = descricao;
            planoClassificacaoModel.AreaFim = areaFim;
            planoClassificacaoModel.Aprovacao = aprovacao;
            planoClassificacaoModel.Publicacao = publicacao;
            planoClassificacaoModel.InicioVigencia = inicioVigencia;

            await _core.UpdateAsync(planoClassificacaoModel);

            planoClassificacaoModel = _core.Search(planoClassificacaoModel.Id);

            Assert.IsTrue(planoClassificacaoModel.Id == id);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(planoClassificacaoModel.AreaFim, areaFim);
            //Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
            Assert.AreEqual(planoClassificacaoModel.Publicacao, publicacao);
            Assert.AreEqual(planoClassificacaoModel.InicioVigencia, inicioVigencia);
            Assert.IsFalse(planoClassificacaoModel.FimVigencia.HasValue);
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithCompleteFields()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descri��o Teste",
                AreaFim = false
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);
            planoClassificacaoModel.Organizacao = null;
            planoClassificacaoModel.GuidOrganizacao = Guid.Empty;

            DateTime now = DateTime.Now;

            int id = planoClassificacaoModel.Id;
            string codigo = "TestUpdateWithCompleteFields01";
            string descricao = "TestUpdateWithCompleteFieldsDescri��o Teste";
            bool areaFim = true;
            DateTime aprovacao = now;
            DateTime publicacao = now.AddDays(1);
            DateTime inicioVigencia = now.AddDays(2);
            DateTime fimVigencia = now.AddDays(3);

            planoClassificacaoModel.Codigo = codigo;
            planoClassificacaoModel.Descricao = descricao;
            planoClassificacaoModel.AreaFim = areaFim;
            planoClassificacaoModel.Aprovacao = aprovacao;
            planoClassificacaoModel.Publicacao = publicacao;
            planoClassificacaoModel.InicioVigencia = inicioVigencia;
            planoClassificacaoModel.FimVigencia = fimVigencia;

            await _core.UpdateAsync(planoClassificacaoModel);

            planoClassificacaoModel = _core.Search(planoClassificacaoModel.Id);

            Assert.IsTrue(planoClassificacaoModel.Id == id);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(planoClassificacaoModel.AreaFim, areaFim);
            //Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
            Assert.AreEqual(planoClassificacaoModel.Publicacao, publicacao);
            Assert.AreEqual(planoClassificacaoModel.InicioVigencia, inicioVigencia);
            Assert.AreEqual(planoClassificacaoModel.FimVigencia, fimVigencia);
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateFimVigenciaWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            int id = _planoClassificacaoModel.Id;
            DateTime fimVigencia = now.AddDays(3);

            bool ok = false;

            try
            {
                await _core.UpdateFimVigenciaAsync(id, fimVigencia);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de in�cio de vig�ncia n�o pode ser vazia ou nula quando existe uma data de fim de vig�ncia.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado a data de fim de vig�ncia sem a data de in�cio de vig�ncia.");

            PlanoClassificacaoModel planoClassificacaoModel = _core.Search(id);

            Assert.IsTrue(planoClassificacaoModel.Id == id);
            Assert.AreEqual(planoClassificacaoModel.Codigo, _planoClassificacaoModel.Codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, _planoClassificacaoModel.Descricao);
            Assert.AreEqual(planoClassificacaoModel.AreaFim, _planoClassificacaoModel.AreaFim);
            //Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, _planoClassificacaoModel.GuidOrganizacao);
            Assert.IsFalse(planoClassificacaoModel.Aprovacao.HasValue);
            Assert.IsFalse(planoClassificacaoModel.Publicacao.HasValue);
            Assert.IsFalse(planoClassificacaoModel.InicioVigencia.HasValue);
            Assert.IsFalse(planoClassificacaoModel.FimVigencia.HasValue);
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateFimVigenciaBeforeInicioVigencia()
        {
            DateTime now = DateTime.Now;

            int id = _planoClassificacaoPublicadoModel.Id;
            DateTime fimVigencia = _planoClassificacaoPublicadoModel.InicioVigencia.Value.AddDays(-1);

            bool ok = false;

            try
            {
                await _core.UpdateFimVigenciaAsync(id, fimVigencia);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de fim de vig�ncia deve ser maior ou igual � data de in�cio de vig�ncia.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com data de fim de vig�ncia anterior � data de in�cio de vig�ncia.");

            PlanoClassificacaoModel planoClassificacaoModel = _core.Search(id);

            Assert.IsTrue(planoClassificacaoModel.Id == id);
            Assert.AreEqual(planoClassificacaoModel.Codigo, _planoClassificacaoPublicadoModel.Codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, _planoClassificacaoPublicadoModel.Descricao);
            Assert.AreEqual(planoClassificacaoModel.AreaFim, _planoClassificacaoPublicadoModel.AreaFim);
            //Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, _planoClassificacaoPublicadoModel.GuidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, _planoClassificacaoPublicadoModel.Aprovacao);
            Assert.AreEqual(planoClassificacaoModel.Publicacao, _planoClassificacaoPublicadoModel.Publicacao);
            Assert.AreEqual(planoClassificacaoModel.InicioVigencia, _planoClassificacaoPublicadoModel.InicioVigencia);
            Assert.IsFalse(planoClassificacaoModel.FimVigencia.HasValue);
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateFimVigencia()
        {
            DateTime now = DateTime.Now;

            string codigo = "01";
            string descricao = "Descri��o Teste";
            bool areaFim = true;
            DateTime aprovacao = now;
            DateTime publicacao = now.AddDays(1);
            DateTime inicioVigencia = now.AddDays(2);

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = codigo,
                Descricao = descricao,
                AreaFim = areaFim,
                Aprovacao = aprovacao,
                Publicacao = publicacao,
                InicioVigencia = inicioVigencia
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            int id = planoClassificacaoModel.Id;
            DateTime fimVigencia = now.AddDays(3);

            await _core.UpdateFimVigenciaAsync(id, fimVigencia);

            planoClassificacaoModel = _core.Search(id);

            Assert.IsTrue(planoClassificacaoModel.Id == id);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(planoClassificacaoModel.AreaFim, areaFim);
            //Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
            Assert.AreEqual(planoClassificacaoModel.Publicacao, publicacao);
            Assert.AreEqual(planoClassificacaoModel.InicioVigencia, inicioVigencia);
            Assert.AreEqual(planoClassificacaoModel.FimVigencia, fimVigencia);
        }
    }
}