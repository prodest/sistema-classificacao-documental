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

                Assert.AreEqual(ex.Message, "O Plano de Classificação não pode ser nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com objeto nulo.");
        }

        #region Código
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

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com código nulo.");
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

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com código vazio.");
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

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com código somente com espaço.");
        }
        #endregion

        #region Descrição
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição nula.");
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição vazia.");
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição vazia.");
        }
        #endregion

        #region Publicação
        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithPublicacaoWithoutAprovacao()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", Publicacao = now };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de aprovação não pode ser vazia ou nula quando existe uma data de publicação.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com data de publicação e sem data de aprovação.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithPublicacaoWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", Aprovacao = now, Publicacao = now };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de início de vigência não pode ser vazia ou nula quando existe uma data de publicação.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com data de publicação e sem data de início de vigência.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithPublicacaoBeforeAprovacao()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", Aprovacao = now, Publicacao = now.AddDays(-1), InicioVigencia = now };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de publicação deve ser maior ou igual à data de aprovação.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com data de publicação anterior à data de aprovação.");
        }
        #endregion

        #region Fim de Vigência
        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithFimVigenciaWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", FimVigencia = now };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de início de vigência não pode ser vazia ou nula quando existe uma data de fim de vigência.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com data de fim de vigência e sem data de início de vigência.");
        }

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithFimVigenciaBeforeInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", InicioVigencia = now, FimVigencia = now.AddDays(-1) };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A data de fim de vigência deve ser maior ou igual à data de início de vigência.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com data de fim de vigência anterior à data de início de vigência.");
        }
        #endregion

        #region Id
        public async Task PlanoClassificacaoTestInsertWithInvalidInsertId()
        {
            string codigo = "01";
            string descricao = "Descrição Teste";
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

                Assert.AreEqual(ex.Message, "O id não deve ser preenchido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com o id inválido para inserção.");
        }
        #endregion

        [TestMethod]
        public async Task PlanoClassificacaoTestInsertWithBasicsFields()
        {
            string codigo = "01";
            string descricao = "Descrição Teste";
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
            string descricao = "Descrição Teste";
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
            string descricao = "Descrição Teste";
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
            string descricao = "Descrição Teste";
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
