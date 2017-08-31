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
using Prodest.Scd.Integration.Organograma;
using Prodest.Scd.Web.Configuration;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.PlanoClassificacao.UnitTestBusiness
{
    [TestClass]
    public class UnitTestPlanoClassificacaoUpdate
    {
        private string _guidProdest = Environment.GetEnvironmentVariable("GuidProdest");
        private string _guidSeger = Environment.GetEnvironmentVariable("GuidSeger");
        private PlanoClassificacaoCore _core;
        private PlanoClassificacaoModel _planoClassificacaoModel;
        private PlanoClassificacaoModel _planoClassificacaoPublicadoModel;

        [TestInitialize]
        public async Task Setup()
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

            string codigo = "01";
            string descricao = "Descrição Teste";
            bool areaFim = true;
            string guidOrganizacao = _guidProdest;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = codigo, Descricao = descricao, AreaFim = areaFim, GuidOrganizacao = guidOrganizacao };

            _planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoPublicadoModel = new PlanoClassificacaoModel { Codigo = codigo, Descricao = descricao, AreaFim = areaFim, GuidOrganizacao = guidOrganizacao, Aprovacao = now, Publicacao = now, InicioVigencia = now };

            _planoClassificacaoPublicadoModel = await _core.InsertAsync(planoClassificacaoPublicadoModel);
        }

        [TestMethod]
        public async Task TestUpdateNull()
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
        public async Task TestUpdateWithCodigoNull()
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
        public async Task TestUpdateWithCodigoEmpty()
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
        public async Task TestUpdateWithCodigoTrimEmpty()
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
        public async Task TestUpdateWithDescricaoNull()
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
        public async Task TestUpdateWithDescricaoEmpty()
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
        public async Task TestUpdateWithDescricaoTrimEmpty()
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

        #region Guid Organização
        [TestMethod]
        public async Task TestUpdateWithGuidOrganizacaoNull()
        {
            bool ok = false;
            try
            {
                _planoClassificacaoModel.GuidOrganizacao = null;

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com guid da organização nulo.");
        }

        [TestMethod]
        public async Task TestUpdateWithGuidOrganizacaoEmpty()
        {
            bool ok = false;

            try
            {
                _planoClassificacaoModel.GuidOrganizacao = "";

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com guid da organização vazio.");
        }

        [TestMethod]
        public async Task TestUpdateWithGuidOrganizacaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                _planoClassificacaoModel.GuidOrganizacao = " ";

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com guid da organização somente com espaço.");
        }

        [TestMethod]
        public async Task TestUpdateWithGuidOrganizacaoNotGuid()
        {
            bool ok = false;

            try
            {
                _planoClassificacaoModel.GuidOrganizacao = "ABC";

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com guid da organização inválido.");
        }

        [TestMethod]
        public async Task TestUpdateWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                _planoClassificacaoModel.GuidOrganizacao = new Guid().ToString();

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com guid da organização sendo um guid vazio.");
        }

        [TestMethod]
        public async Task TestUpdateWithGuidOrganizacaoNonexistentOnOrganograma()
        {
            bool ok = false;

            try
            {
                _planoClassificacaoModel.GuidOrganizacao = Guid.NewGuid().ToString();

                await _core.UpdateAsync(_planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));

                Assert.IsTrue(ex.Message.Contains("Não foi possível obter os dados do serviço."));
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com o guid da organização não existindo no sistema de organograma.");
        }
        #endregion

        #region Publicação
        [TestMethod]
        public async Task TestUpdateWithPublicacaoWithoutAprovacao()
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
        public async Task TestUpdateWithPublicacaoWithoutInicioVigencia()
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
        public async Task TestUpdateWithPublicacaoBeforeAprovacao()
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
        public async Task TestUpdateWithFimVigenciaWithoutInicioVigencia()
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
        public async Task TestUpdateWithFimVigenciaBeforeInicioVigencia()
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
        public async Task TestUpdatePlanoClassificacaoPublicado()
        {
            int id = _planoClassificacaoPublicadoModel.Id;
            string codigo = "TestUpdateWithPublicacao" + "01";
            string descricao = "TestUpdateWithPublicacao" + "Descrição Teste";
            bool areaFim = false;
            string guidOrganizacao = null;
            if (_planoClassificacaoModel.GuidOrganizacao.Equals(_guidProdest))
                guidOrganizacao = _guidSeger;
            else
                guidOrganizacao = _guidProdest;

            _planoClassificacaoPublicadoModel.Codigo = codigo;
            _planoClassificacaoPublicadoModel.Descricao = descricao;
            _planoClassificacaoPublicadoModel.AreaFim = areaFim;
            _planoClassificacaoPublicadoModel.GuidOrganizacao = guidOrganizacao;

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
        public async Task TestUpdateWithBasicsFields()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descrição Teste",
                AreaFim = false,
                GuidOrganizacao = _guidProdest
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            int id = planoClassificacaoModel.Id;
            string codigo = "TestUpdateWithBasicsFields01";
            string descricao = "TestUpdateWithBasicsFieldsDescrição Teste";
            bool areaFim = true;
            string guidOrganizacao = _guidSeger;

            planoClassificacaoModel.Codigo = codigo;
            planoClassificacaoModel.Descricao = descricao;
            planoClassificacaoModel.AreaFim = areaFim;
            planoClassificacaoModel.GuidOrganizacao = guidOrganizacao;

            await _core.UpdateAsync(planoClassificacaoModel);

            planoClassificacaoModel = _core.Search(planoClassificacaoModel.Id);

            Assert.IsTrue(planoClassificacaoModel.Id == id);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(planoClassificacaoModel.AreaFim, areaFim);
            Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.IsFalse(planoClassificacaoModel.Aprovacao.HasValue);
            Assert.IsFalse(planoClassificacaoModel.Publicacao.HasValue);
            Assert.IsFalse(planoClassificacaoModel.InicioVigencia.HasValue);
            Assert.IsFalse(planoClassificacaoModel.FimVigencia.HasValue);
        }

        [TestMethod]
        public async Task TestUpdateWithAprovacao()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descrição Teste",
                AreaFim = false,
                GuidOrganizacao = _guidProdest
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            DateTime now = DateTime.Now;

            int id = planoClassificacaoModel.Id;
            string codigo = "TestUpdateWithBasicsFields01";
            string descricao = "TestUpdateWithBasicsFieldsDescrição Teste";
            bool areaFim = true;
            string guidOrganizacao = _guidSeger;
            DateTime aprovacao = now;

            planoClassificacaoModel.Codigo = codigo;
            planoClassificacaoModel.Descricao = descricao;
            planoClassificacaoModel.AreaFim = areaFim;
            planoClassificacaoModel.GuidOrganizacao = guidOrganizacao;
            planoClassificacaoModel.Aprovacao = aprovacao;

            await _core.UpdateAsync(planoClassificacaoModel);

            planoClassificacaoModel = _core.Search(planoClassificacaoModel.Id);

            Assert.IsTrue(planoClassificacaoModel.Id == id);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(planoClassificacaoModel.AreaFim, areaFim);
            Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
            Assert.IsFalse(planoClassificacaoModel.Publicacao.HasValue);
            Assert.IsFalse(planoClassificacaoModel.InicioVigencia.HasValue);
            Assert.IsFalse(planoClassificacaoModel.FimVigencia.HasValue);
        }

        [TestMethod]
        public async Task TestUpdateWithoutFimVigencia()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descrição Teste",
                AreaFim = false,
                GuidOrganizacao = _guidProdest
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            DateTime now = DateTime.Now;

            int id = planoClassificacaoModel.Id;
            string codigo = "TestUpdateWithoutFimVigencia01";
            string descricao = "TestUpdateWithoutFimVigenciaDescrição Teste";
            bool areaFim = true;
            string guidOrganizacao = _guidSeger;
            DateTime aprovacao = now;
            DateTime publicacao = now.AddDays(1);
            DateTime inicioVigencia = now.AddDays(2);

            planoClassificacaoModel.Codigo = codigo;
            planoClassificacaoModel.Descricao = descricao;
            planoClassificacaoModel.AreaFim = areaFim;
            planoClassificacaoModel.GuidOrganizacao = guidOrganizacao;
            planoClassificacaoModel.Aprovacao = aprovacao;
            planoClassificacaoModel.Publicacao = publicacao;
            planoClassificacaoModel.InicioVigencia = inicioVigencia;

            await _core.UpdateAsync(planoClassificacaoModel);

            planoClassificacaoModel = _core.Search(planoClassificacaoModel.Id);

            Assert.IsTrue(planoClassificacaoModel.Id == id);
            Assert.AreEqual(planoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(planoClassificacaoModel.Descricao, descricao);
            Assert.AreEqual(planoClassificacaoModel.AreaFim, areaFim);
            Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
            Assert.AreEqual(planoClassificacaoModel.Publicacao, publicacao);
            Assert.AreEqual(planoClassificacaoModel.InicioVigencia, inicioVigencia);
            Assert.IsFalse(planoClassificacaoModel.FimVigencia.HasValue);
        }

        [TestMethod]
        public async Task TestUpdateWithCompleteFields()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = "01",
                Descricao = "Descrição Teste",
                AreaFim = false,
                GuidOrganizacao = _guidProdest
            };

            planoClassificacaoModel = await _core.InsertAsync(planoClassificacaoModel);

            DateTime now = DateTime.Now;

            int id = planoClassificacaoModel.Id;
            string codigo = "TestUpdateWithCompleteFields01";
            string descricao = "TestUpdateWithCompleteFieldsDescrição Teste";
            bool areaFim = true;
            string guidOrganizacao = _guidSeger;
            DateTime aprovacao = now;
            DateTime publicacao = now.AddDays(1);
            DateTime inicioVigencia = now.AddDays(2);
            DateTime fimVigencia = now.AddDays(3);

            planoClassificacaoModel.Codigo = codigo;
            planoClassificacaoModel.Descricao = descricao;
            planoClassificacaoModel.AreaFim = areaFim;
            planoClassificacaoModel.GuidOrganizacao = guidOrganizacao;
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
            Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
            Assert.AreEqual(planoClassificacaoModel.Publicacao, publicacao);
            Assert.AreEqual(planoClassificacaoModel.InicioVigencia, inicioVigencia);
            Assert.AreEqual(planoClassificacaoModel.FimVigencia, fimVigencia);
        }

        [TestMethod]
        public async Task TesteUpdateFimVigenciaWithoutInicioVigencia()
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
            Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, _planoClassificacaoModel.GuidOrganizacao);
            Assert.IsFalse(planoClassificacaoModel.Aprovacao.HasValue);
            Assert.IsFalse(planoClassificacaoModel.Publicacao.HasValue);
            Assert.IsFalse(planoClassificacaoModel.InicioVigencia.HasValue);
            Assert.IsFalse(planoClassificacaoModel.FimVigencia.HasValue);
        }

        [TestMethod]
        public async Task TesteUpdateFimVigenciaBeforeInicioVigencia()
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
            Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, _planoClassificacaoPublicadoModel.GuidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, _planoClassificacaoPublicadoModel.Aprovacao);
            Assert.AreEqual(planoClassificacaoModel.Publicacao, _planoClassificacaoPublicadoModel.Publicacao);
            Assert.AreEqual(planoClassificacaoModel.InicioVigencia, _planoClassificacaoPublicadoModel.InicioVigencia);
            Assert.IsFalse(planoClassificacaoModel.FimVigencia.HasValue);
        }

        [TestMethod]
        public async Task TesteUpdateFimVigencia()
        {
            DateTime now = DateTime.Now;

            string codigo = "01";
            string descricao = "Descrição Teste";
            bool areaFim = true;
            string guidOrganizacao = _guidProdest;
            DateTime aprovacao = now;
            DateTime publicacao = now.AddDays(1);
            DateTime inicioVigencia = now.AddDays(2);

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel
            {
                Codigo = codigo,
                Descricao = descricao,
                AreaFim = areaFim,
                GuidOrganizacao = guidOrganizacao,
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
            Assert.AreEqual(planoClassificacaoModel.GuidOrganizacao, guidOrganizacao);
            Assert.AreEqual(planoClassificacaoModel.Aprovacao, aprovacao);
            Assert.AreEqual(planoClassificacaoModel.Publicacao, publicacao);
            Assert.AreEqual(planoClassificacaoModel.InicioVigencia, inicioVigencia);
            Assert.AreEqual(planoClassificacaoModel.FimVigencia, fimVigencia);
        }
    }
}