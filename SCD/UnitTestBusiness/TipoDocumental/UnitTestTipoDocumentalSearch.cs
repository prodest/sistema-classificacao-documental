using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.TipoDocumental
{
    [TestClass]
    public class UnitTestTipoDocumentalSearch : UnitTestTipoDocumentalCommon
    {
        #region Search by Id
        #region Id
        [TestMethod]
        public async Task TipoDocumentalTestSearchWithInvalidId()
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

                Assert.AreEqual(ex.Message, "O id n�o pode ser nulo ou vazio.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquisado com id zero.");
        }
        #endregion

        [TestMethod]
        public async Task TipoDocumentalTestSearchWithIdNonexistentOnDataBase()
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

                Assert.AreEqual(ex.Message, "Tipo Documental n�o encontrado.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquisado com id inexistente na base de dados.");
        }

        [TestMethod]
        public async Task TipoDocumentalTestSearchWithIdCorrect()
        {
            TipoDocumentalModel tipoDocumentalModel = await InsertAsync();

            TipoDocumentalModel tipoDocumentalModelSearched = await _core.SearchAsync(tipoDocumentalModel.Id);

            Assert.AreEqual(tipoDocumentalModel.Id, tipoDocumentalModelSearched.Id);
            Assert.AreEqual(tipoDocumentalModel.Descricao, tipoDocumentalModelSearched.Descricao);
            Assert.IsTrue(tipoDocumentalModel.Ativo);
            //Assert.AreEqual(tipoDocumental.Organizacao.GuidOrganizacao, tipoDocumentalModelSearched.Organizacao.GuidOrganizacao);
        }
        #endregion
                
        #region Pagination Search by GuidOrganiza��o
        #region Guid Organiza��o
        [TestMethod]
        public async Task TipoDocumentalTestPaginationSearchWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(Guid.Empty, default(int), default(int));

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
        public async Task TipoDocumentalTestPaginationSearchWithInvalidPage()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(_guidGees, default(int), default(int));

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
        public async Task TipoDocumentalTestPaginationSearchWithInvalidCount()
        {
            bool ok = false;

            try
            {
                await _core.SearchAsync(_guidGees, 1, default(int));

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
        public async Task TipoDocumentalTestPaginationSearchWithGuidOrganizacaoNonexistentOnDataBase()
        {
            ICollection<TipoDocumentalModel> tiposDocumentaisModel = await _core.SearchAsync(Guid.NewGuid(), 1, 1);

            Assert.IsNotNull(tiposDocumentaisModel);
            Assert.IsTrue(tiposDocumentaisModel.Count == 0);
        }

        [TestMethod]
        public async Task TipoDocumentalTestPaginationSearch()
        {
            int page = 2;
            int count = 5;

            for (int i = 0; i < (page * count); i++)
            {
                await InsertAsync();
            }

            ICollection<TipoDocumentalModel> tiposDocumentaisModel = await _core.SearchAsync(_guidGees, page, count);
            Assert.IsNotNull(tiposDocumentaisModel);
            Assert.IsTrue(tiposDocumentaisModel.Count == count);

            foreach (TipoDocumentalModel pcm in tiposDocumentaisModel)
            {
                Assert.IsFalse(pcm.Id == default(int));
                Assert.IsFalse(string.IsNullOrWhiteSpace(pcm.Descricao));
                //Assert.IsFalse(Guid.Empty.Equals(pcm.Organizacao.GuidOrganizacao));
            }
        }
        #endregion
    }
}
