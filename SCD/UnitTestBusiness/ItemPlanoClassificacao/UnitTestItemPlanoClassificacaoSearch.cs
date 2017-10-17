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
                await _core.Search(default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O id n�o pode ser nulo ou vazio.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquisado com id zero.");
        }
        #endregion

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestSearchWithIdNonexistentOnDataBase()
        {
            bool ok = false;

            try
            {
                await _core.Search(-1);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Tipo Documental n�o encontrado.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquisado com id inexistente na base de dados.");
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestSearchWithIdCorrect()
        {
            Persistence.Model.ItemPlanoClassificacao itemPlanoClassificacao = await InsertAsync();

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModelSearched = await _core.Search(itemPlanoClassificacao.Id);

            Assert.AreEqual(itemPlanoClassificacao.Id, itemPlanoClassificacaoModelSearched.Id);
            Assert.AreEqual(itemPlanoClassificacao.Codigo, itemPlanoClassificacaoModelSearched.Codigo);
            Assert.AreEqual(itemPlanoClassificacao.Descricao, itemPlanoClassificacaoModelSearched.Descricao);

            //Assert.IsTrue(itemPlanoClassificacao.Ativo);
            //Assert.AreEqual(itemPlanoClassificacao.Organizacao.GuidOrganizacao, itemPlanoClassificacaoModelSearched.Organizacao.GuidOrganizacao);
        }
        #endregion
                
        #region Pagination Search by GuidOrganiza��o
        #region Guid Organiza��o
        [TestMethod]
        public void ItemPlanoClassificacaoTestPaginationSearchWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                //_core.Search(Guid.Empty, default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organiza��o inv�lido.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquido com o guid da organiza��o sendo um guid vazio.");
        }
        #endregion

        #region Page
        [TestMethod]
        public void ItemPlanoClassificacaoTestPaginationSearchWithInvalidPage()
        {
            bool ok = false;

            try
            {
                //_core.Search(_guidGees, default(int), default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "P�gina inv�lida.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquido com a p�gina inv�lida.");
        }
        #endregion

        #region Count
        [TestMethod]
        public void ItemPlanoClassificacaoTestPaginationSearchWithInvalidCount()
        {
            bool ok = false;

            try
            {
                //_core.Search(_guidGees, 1, default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Quantidade de rgistro por p�gina inv�lida.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquido com a quantidade de registro por p�gina inv�lida.");
        }
        #endregion

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestPaginationSearchWithGuidOrganizacaoNonexistentOnDataBase()
        {
            ICollection<ItemPlanoClassificacaoModel> itensPlanoClassificacaoModel = await _core.Search(0, 1, 1);

            Assert.IsNotNull(itensPlanoClassificacaoModel);
            Assert.IsTrue(itensPlanoClassificacaoModel.Count == 0);
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestPaginationSearch()
        {
            int page = 2;
            int count = 5;

            for (int i = 0; i < (page * count); i++)
            {
                await InsertAsync();
            }

            ICollection<ItemPlanoClassificacaoModel> itensPlanoClassificacaoModel = await _core.Search(0, page, count);
            Assert.IsNotNull(itensPlanoClassificacaoModel);
            Assert.IsTrue(itensPlanoClassificacaoModel.Count == count);

            foreach (ItemPlanoClassificacaoModel pcm in itensPlanoClassificacaoModel)
            {
                Assert.IsFalse(pcm.Id == default(int));
                Assert.IsFalse(string.IsNullOrWhiteSpace(pcm.Descricao));
                //Assert.IsFalse(Guid.Empty.Equals(pcm.Organizacao.GuidOrganizacao));
            }
        }
        #endregion
    }
}
