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

                Assert.AreEqual(ex.Message, "Tipo Documental não encontrado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com id inexistente na base de dados.");
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestSearchWithIdCorrect()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await InsertAsync();

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModelSearched = await _core.SearchAsync(itemPlanoClassificacaoModel.Id);

            Assert.AreEqual(itemPlanoClassificacaoModel.Id, itemPlanoClassificacaoModelSearched.Id);
            Assert.AreEqual(itemPlanoClassificacaoModel.Codigo, itemPlanoClassificacaoModelSearched.Codigo);
            Assert.AreEqual(itemPlanoClassificacaoModel.Descricao, itemPlanoClassificacaoModelSearched.Descricao);

            //Assert.IsTrue(itemPlanoClassificacao.Ativo);
            //Assert.AreEqual(itemPlanoClassificacao.Organizacao.GuidOrganizacao, itemPlanoClassificacaoModelSearched.Organizacao.GuidOrganizacao);
        }
        #endregion
                
        #region Pagination Search by GuidOrganização
        #region Guid Organização
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

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com o guid da organização sendo um guid vazio.");
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

                Assert.AreEqual(ex.Message, "Página inválida.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com a página inválida.");
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

                Assert.AreEqual(ex.Message, "Quantidade de rgistro por página inválida.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquido com a quantidade de registro por página inválida.");
        }
        #endregion

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestPaginationSearchWithGuidOrganizacaoNonexistentOnDataBase()
        {
            ICollection<ItemPlanoClassificacaoModel> itensPlanoClassificacaoModel = await _core.SearchAsync(0, 1, 1);

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

            ICollection<ItemPlanoClassificacaoModel> itensPlanoClassificacaoModel = await _core.SearchAsync(0, page, count);
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
