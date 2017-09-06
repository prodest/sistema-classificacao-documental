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

                Assert.AreEqual(ex.Message, "O Plano de Classificação não pode ser nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com objeto nulo.");
        }

        #region Código
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

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com código nulo.");
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

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com código vazio.");
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

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com código somente com espaço.");
        }
        #endregion

        #region Descrição
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição nula.");
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição vazia.");
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição vazia.");
        }
        #endregion

        #region Guid Organização
        [TestMethod]
        public async Task TestInsertWithGuidOrganizacaoNull()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste" };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com guid da organização nulo.");
        }

        [TestMethod]
        public async Task TestInsertWithGuidOrganizacaoGuidEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.Empty };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com o guid da organização sendo um guid vazio.");
        }

        [TestMethod]
        public async Task TestInsertWithGuidOrganizacaoNonexistentOnOrganograma()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.NewGuid() };

            bool ok = false;

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));

                Assert.IsTrue(ex.Message.Contains("Não foi possível obter os dados do serviço."));
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com o guid da organização não existindo no sistema de organograma.");
        }
        #endregion

        #region Publicação
        [TestMethod]
        public async Task TestInsertWithPublicacaoWithoutAprovacao()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.NewGuid(), Publicacao = now };

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
        public async Task TestInsertWithPublicacaoWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.NewGuid(), Aprovacao = now, Publicacao = now };

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
        public async Task TestInsertWithPublicacaoBeforeAprovacao()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.NewGuid(), Aprovacao = now, Publicacao = now.AddDays(-1), InicioVigencia = now };

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
        public async Task TestInsertWithFimVigenciaWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.NewGuid(), FimVigencia = now };

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
        public async Task TestInsertWithFimVigenciaBeforeInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.NewGuid(), InicioVigencia = now, FimVigencia = now.AddDays(-1) };

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
        public async Task TestInsertWithInvalidInsertId()
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
        public async Task TestInsertWithBasicsFields()
        {
            string codigo = "01";
            string descricao = "Descrição Teste";
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
            string descricao = "Descrição Teste";
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
            string descricao = "Descrição Teste";
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
            string descricao = "Descrição Teste";
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
