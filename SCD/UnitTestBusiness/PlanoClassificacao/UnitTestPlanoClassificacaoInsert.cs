using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Configuration;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Common.Exceptions;
using Prodest.Scd.Infrastructure.Repository;
using Prodest.Scd.Integration.Organograma;
using Prodest.Scd.Web.Configuration;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.PlanoClassificacao.UnitTestBusiness
{
    [TestClass]
    public class UnitTestPlanoClassificacaoInsert
    {
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
                cfg.AddProfile<ProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation, mapper);

            _core = new PlanoClassificacaoCore(repositories, planoClassificacaoValidation, mapper, organogramaService, organizacaoCore);
        }

        [TestMethod]
        public async Task TestInsertNull()
        {
            try
            {
                await _core.InsertAsync(null);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "O Plano de Classificação não pode ser nulo.");
            }
        }

        #region Código
        [TestMethod]
        public async Task TestInsertWithCodigoNull()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel();
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }
        }

        [TestMethod]
        public async Task TestInsertWithCodigoEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "" };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }
        }

        [TestMethod]
        public async Task TestInsertWithCodigoTrimEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = " " };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }
        }
        #endregion

        #region Descrição
        [TestMethod]
        public async Task TestInsertWithDescricaoNull()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01" };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }
        }

        [TestMethod]
        public async Task TestInsertWithDescricaoEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "" };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }
        }

        [TestMethod]
        public async Task TestInsertWithDescricaoTrimEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = " " };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }
        }
        #endregion

        #region Guid Organização
        [TestMethod]
        public async Task TestInsertWithGuidOrganizacaoNull()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste" };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }
        }

        [TestMethod]
        public async Task TestInsertWithGuidOrganizacaoEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = "" };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }
        }

        [TestMethod]
        public async Task TestInsertWithGuidOrganizacaoTrimEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = " " };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A organização não pode ser vazia ou nula.");
            }
        }

        [TestMethod]
        public async Task TestInsertWithGuidOrganizacaoNotGuid()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = "ABC" };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }
        }

        [TestMethod]
        public async Task TestInsertWithGuidOrganizacaoGuidEmpty()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = new Guid().ToString() };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }
        }

        [TestMethod]
        public async Task TestInsertWithGuidOrganizacaoNonexistentOnOrganograma()
        {
            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.NewGuid().ToString() };

            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.IsTrue(ex.Message.Contains("Não foi possível obter os dados do serviço."));
            }
        }
        #endregion

        #region Publicação
        [TestMethod]
        public async Task TestInsertWithPublicacaoWithoutAprovacao()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.NewGuid().ToString(), Publicacao = now };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A data de aprovação não pode ser vazia ou nula quando existe uma data de publicação.");
            }
        }

        [TestMethod]
        public async Task TestInsertWithPublicacaoWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.NewGuid().ToString(), Aprovacao = now, Publicacao = now };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A data de início de vigência não pode ser vazia ou nula quando existe uma data de publicação.");
            }
        }

        [TestMethod]
        public async Task TestInsertWithPublicacaoBeforeAprovacao()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.NewGuid().ToString(), Aprovacao = now, Publicacao = now.AddDays(-1), InicioVigencia = now };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A data de publicação deve ser maior ou igual à data de aprovação.");
            }
        }
        #endregion

        #region Fim de Vigência
        [TestMethod]
        public async Task TestInsertWithFimVigenciaWithoutInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.NewGuid().ToString(), FimVigencia = now };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A data de início de vigência não pode ser vazia ou nula quando existe uma data de fim de vigência.");
            }
        }

        [TestMethod]
        public async Task TestInsertWithFimVigenciaBeforeInicioVigencia()
        {
            DateTime now = DateTime.Now;

            PlanoClassificacaoModel planoClassificacaoModel = new PlanoClassificacaoModel { Codigo = "01", Descricao = "Descrição Teste", GuidOrganizacao = Guid.NewGuid().ToString(), InicioVigencia = now, FimVigencia = now.AddDays(-1) };
            try
            {
                await _core.InsertAsync(planoClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdExpection));

                Assert.AreEqual(ex.Message, "A data de fim de vigência deve ser maior ou igual à data de início de vigência.");
            }
        }
        #endregion

        [TestMethod]
        public async Task TestInsertWithBasicsFields()
        {
            string codigo = "01";
            string descricao = "Descrição Teste";
            bool areaFim = true;
            string guidOrganizacao = Environment.GetEnvironmentVariable("GuidProdest");

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
            string guidOrganizacao = Environment.GetEnvironmentVariable("GuidProdest");
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
            string guidOrganizacao = Environment.GetEnvironmentVariable("GuidProdest");
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
            string guidOrganizacao = Environment.GetEnvironmentVariable("GuidProdest");
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
