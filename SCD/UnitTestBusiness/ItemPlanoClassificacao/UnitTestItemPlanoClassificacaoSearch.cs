using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.ItemPlanoClassificacao
{
    [TestClass]
    public class UnitTestItemPlanoClassificacaoSearch : UnitTestItemPlanoClassificacaoCommon
    {
        #region Search by Id
        #region Id
        [TestMethod]
        public async Task ItemPlanoClassificacaoTestSearchWithInvalidId()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O id não pode ser nulo ou vazio.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com id zero.");
        }
        #endregion

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestSearchWithIdNonexistentOnDataBase()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(-1);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Item do Plano de Classificação não encontrado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com id inexistente na base de dados.");
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestSearchWithIdCorrect()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await InsertItemPlanoClassificacaoAsync();

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModelSearched = await _core.SearchAsync(itemPlanoClassificacaoModel.Id);

            Assert.AreEqual(itemPlanoClassificacaoModel.Id, itemPlanoClassificacaoModelSearched.Id);
            Assert.AreEqual(itemPlanoClassificacaoModel.Codigo, itemPlanoClassificacaoModelSearched.Codigo);
            Assert.AreEqual(itemPlanoClassificacaoModel.Descricao, itemPlanoClassificacaoModelSearched.Descricao);
            Assert.AreEqual(itemPlanoClassificacaoModel.NivelClassificacao.Id, itemPlanoClassificacaoModelSearched.NivelClassificacao.Id);
            Assert.AreEqual(itemPlanoClassificacaoModel.PlanoClassificacao.Id, itemPlanoClassificacaoModelSearched.PlanoClassificacao.Id);
        }
        #endregion
                
        #region Pagination Search by Plano de Classificação
        #region Guid Organização
        [TestMethod]
        public async Task ItemPlanoClassificacaoTestPaginationSearchWithInvalidIdPlanoClassificacao()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(default(int), default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O id não pode ser nulo ou vazio.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com o id do Plano de Classificação inválido.");
        }
        #endregion

        #region Page
        [TestMethod]
        public async Task ItemPlanoClassificacaoTestPaginationSearchWithInvalidPage()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(1, default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Página inválida.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com a página inválida.");
        }
        #endregion

        #region Count
        [TestMethod]
        public async Task ItemPlanoClassificacaoTestPaginationSearchWithInvalidCount()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(1, 1, default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Quantidade de rgistro por página inválida.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com a quantidade de registro por página inválida.");
        }
        #endregion

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestPaginationSearchWithGuidOrganizacaoNonexistentOnDataBase()
        {
            ICollection<ItemPlanoClassificacaoModel> itensPlanoClassificacaoModel = await _core.SearchAsync(-1, 1, 1);

            Assert.IsNotNull(itensPlanoClassificacaoModel);
            Assert.IsTrue(itensPlanoClassificacaoModel.Count == 0);
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestPaginationSearch()
        {
            PlanoClassificacaoModel planoClassificacaoModel = await InsertPlanoClassificacaoAsync();

            int page = 2;
            int count = 5;

            for (int i = 0; i < (page * count); i++)
            {
                await InsertItemPlanoClassificacaoAsync(planoClassificacaoModel);
            }

            ICollection<ItemPlanoClassificacaoModel> itensPlanoClassificacaoModel = await _core.SearchAsync(planoClassificacaoModel.Id, page, count);

            Assert.IsNotNull(itensPlanoClassificacaoModel);
            Assert.IsTrue(itensPlanoClassificacaoModel.Count == count);

            foreach (ItemPlanoClassificacaoModel ipc in itensPlanoClassificacaoModel)
            {
                Assert.IsTrue(ipc.Id > default(int));
                Assert.IsFalse(string.IsNullOrWhiteSpace(ipc.Descricao));
                Assert.AreEqual(planoClassificacaoModel.Id, ipc.PlanoClassificacao.Id);
            }
        }
        #endregion
    }
}
