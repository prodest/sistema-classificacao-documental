//using AutoMapper;
//using Microsoft.Extensions.Options;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Prodest.Scd.Business;
//using Prodest.Scd.Business.Common.Exceptions;
//using Prodest.Scd.Business.Configuration;
//using Prodest.Scd.Business.Model;
//using Prodest.Scd.Business.Validation;
//using Prodest.Scd.Infrastructure.Integration;
//using Prodest.Scd.Infrastructure.Repository;
//using Prodest.Scd.Integration.Organograma;
//using Prodest.Scd.Web.Configuration;
//using System;
//using System.Threading.Tasks;

//namespace Prodest.Scd.NivelClassificacao.UnitTestBusiness
//{
//    [TestClass]
//    public class UnitTestNivelClassificacaoDelete
//    {
//        private string _guidProdest = Environment.GetEnvironmentVariable("GuidGEES");
//        private NivelClassificacaoCore _core;

//        [TestInitialize]
//        public void Setup()
//        {
//            ScdRepositories repositories = new ScdRepositories();

//            NivelClassificacaoValidation nivelClassificacaoValidation = new NivelClassificacaoValidation(repositories);

//            Mapper.Initialize(cfg =>
//            {
//                cfg.AddProfile<BusinessProfileAutoMapper>();
//            });

//            IMapper mapper = Mapper.Instance;

//            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

//            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation, mapper);

//            _core = new NivelClassificacaoCore(repositories, nivelClassificacaoValidation, mapper, organizacaoCore);
//        }

//        #region Id
//        [TestMethod]
//        public async Task TestDeleteWithInvalidId()
//        {
//            bool ok = false;

//            try
//            {
//                await _core.DeleteAsync(default(int));

//                ok = true;
//            }
//            catch (Exception ex)
//            {
//                Assert.IsInstanceOfType(ex, typeof(ScdException));

//                Assert.AreEqual(ex.Message, "O id não pode ser nulo ou vazio.");
//            }

//            if (ok)
//                Assert.Fail("Não deveria ter excluído com id zero.");
//        }
//        #endregion

//        [TestMethod]
//        public async Task TestDeleteWithIdNonexistentOnDataBase()
//        {
//            bool ok = false;

//            try
//            {
//                await _core.DeleteAsync(-1);

//                ok = true;
//            }
//            catch (Exception ex)
//            {
//                Assert.IsInstanceOfType(ex, typeof(ScdException));

//                Assert.AreEqual(ex.Message, "Nivel de Classificação não encontrado.");
//            }

//            if (ok)
//                Assert.Fail("Não deveria ter pesquisado com id inexistente na base de dados.");
//        }


//        [TestMethod]
//        public async Task TestDeleteNivelClassificacaoPublicado()
//        {
//            DateTime now = DateTime.Now;
//            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel
//            {
//                Codigo = "01",
//                Descricao = "Descrição Teste",
//                AreaFim = true,
//                GuidOrganizacao = _guidProdest,
//                Aprovacao = now,
//                Publicacao = now,
//                InicioVigencia = now
//            };

//            nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);

//            bool ok = false;

//            try
//            {
//                await _core.DeleteAsync(nivelClassificacaoModel.Id);

//                ok = true;
//            }
//            catch (Exception ex)
//            {
//                Assert.IsInstanceOfType(ex, typeof(ScdException));

//                Assert.AreEqual(ex.Message, "O Nivel de Classificação possui data de publicação e não pode ser excluído.");
//            }

//            if (ok)
//                Assert.Fail("Não deveria ter excluiído com data de publicação.");
//        }

//        //TODO: Após a implementação dos CRUDs de Itens de Plnao de Classificacação fazer os testes de remoção de nivel de classificação com itens associados

//        [TestMethod]
//        public async Task TestDeletehWithIdCorrect()
//        {
//            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel
//            {
//                Codigo = "01",
//                Descricao = "Descrição Teste",
//                AreaFim = true,
//                GuidOrganizacao = _guidProdest
//            };

//            nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);

//            await _core.DeleteAsync(nivelClassificacaoModel.Id);

//            bool ok = false;

//            try
//            {
//                _core.Search(nivelClassificacaoModel.Id);

//                ok = true;
//            }
//            catch (Exception ex)
//            {
//                Assert.IsInstanceOfType(ex, typeof(ScdException));

//                Assert.AreEqual(ex.Message, "Nivel de Classificação não encontrado.");
//            }

//            if (ok)
//                Assert.Fail("Não deveria ter encontrado um Nivel de Classificação excluído.");
//        }
//    }
//}
