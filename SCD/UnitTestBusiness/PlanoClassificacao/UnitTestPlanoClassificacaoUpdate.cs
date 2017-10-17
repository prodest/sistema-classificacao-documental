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
            string descricao = "Descrição Teste";
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

                Assert.AreEqual(ex.Message, "O Plano de Classificação não pode ser nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com objeto nulo.");
        }

        #region Código
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

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com código nulo.");
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

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com código vazio.");
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

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com código somente com espaço.");
        }
        #endregion

        #region Descrição
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com descrição nula.");
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com descrição vazia.");
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com descrição somente com espaço.");
        }
        #endregion

        #region Publicação
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

                Assert.AreEqual(ex.Message, "A data de aprovação não pode ser vazia ou nula quando existe uma data de publicação.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com data de publicação e sem data de aprovação.");
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

                Assert.AreEqual(ex.Message, "A data de início de vigência não pode ser vazia ou nula quando existe uma data de publicação.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com data de publicação e sem data de início de vigência.");
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

                Assert.AreEqual(ex.Message, "A data de publicação deve ser maior ou igual à data de aprovação.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com data de publicação anterior à data de aprovação.");
        }
        #endregion

        #region Fim de Vigência
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

                Assert.AreEqual(ex.Message, "A data de início de vigência não pode ser vazia ou nula quando existe uma data de fim de vigência.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atulizado com data de fim de vigência e sem data de início de vigência.");
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

                Assert.AreEqual(ex.Message, "A data de fim de vigência deve ser maior ou igual à data de início de vigência.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com data de fim de vigência anterior à data de início de vigência.");
        }
        #endregion

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdatePlanoClassificacaoPublicado()
        {
            int id = _planoClassificacaoPublicadoModel.Id;
            string codigo = "TestUpdateWithPublicacao" + "01";
            string descricao = "TestUpdateWithPublicacao" + "Descrição Teste";
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

                Assert.AreEqual(ex.Message, "O Plano de Classificação possui data de publicação e não pode ser atualizado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com data de publicação.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestUpdateWithBasicsFields()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descrição Teste",
                AreaFim = false
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);
            planoClassificacaoModel.Organizacao = null;
            planoClassificacaoModel.GuidOrganizacao = Guid.Empty;

            int id = planoClassificacaoModel.Id;
            string codigo = "TestUpdateWithBasicsFields01";
            string descricao = "TestUpdateWithBasicsFieldsDescrição Teste";
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
                Descricao = "Descrição Teste",
                AreaFim = false
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);
            planoClassificacaoModel.GuidOrganizacao = Guid.Empty;
            planoClassificacaoModel.Organizacao = null;

            DateTime now = DateTime.Now;

            int id = planoClassificacaoModel.Id;
            string codigo = "TestUpdateWithBasicsFields01";
            string descricao = "TestUpdateWithBasicsFieldsDescrição Teste";
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
                Descricao = "Descrição Teste",
                AreaFim = false
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);
            planoClassificacaoModel.Organizacao = null;
            planoClassificacaoModel.GuidOrganizacao = Guid.Empty;

            DateTime now = DateTime.Now;

            int id = planoClassificacaoModel.Id;
            string codigo = "TestUpdateWithoutFimVigencia01";
            string descricao = "TestUpdateWithoutFimVigenciaDescrição Teste";
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
                Descricao = "Descrição Teste",
                AreaFim = false
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);
            planoClassificacaoModel.Organizacao = null;
            planoClassificacaoModel.GuidOrganizacao = Guid.Empty;

            DateTime now = DateTime.Now;

            int id = planoClassificacaoModel.Id;
            string codigo = "TestUpdateWithCompleteFields01";
            string descricao = "TestUpdateWithCompleteFieldsDescrição Teste";
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

                Assert.AreEqual(ex.Message, "A data de início de vigência não pode ser vazia ou nula quando existe uma data de fim de vigência.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado a data de fim de vigência sem a data de início de vigência.");

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

                Assert.AreEqual(ex.Message, "A data de fim de vigência deve ser maior ou igual à data de início de vigência.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com data de fim de vigência anterior à data de início de vigência.");

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
            string descricao = "Descrição Teste";
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