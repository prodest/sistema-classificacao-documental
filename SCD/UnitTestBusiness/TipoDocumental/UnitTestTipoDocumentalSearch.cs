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
        public void TipoDocumentalTestSearchWithInvalidId()
        {
            bool ok = false;

            try
            {
                _core.Search(default(int));

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
        public void TipoDocumentalTestSearchWithIdNonexistentOnDataBase()
        {
            bool ok = false;

            try
            {
                _core.Search(-1);

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
        public async Task TipoDocumentalTestSearchWithIdCorrect()
        {
            Persistence.Model.TipoDocumental tipoDocumental = await InsertAsync();

            TipoDocumentalModel tipoDocumentalModelSearched = _core.Search(tipoDocumental.Id);

            Assert.AreEqual(tipoDocumental.Id, tipoDocumentalModelSearched.Id);
            Assert.AreEqual(tipoDocumental.Descricao, tipoDocumentalModelSearched.Descricao);
            Assert.IsTrue(tipoDocumental.Ativo);
            //Assert.AreEqual(tipoDocumental.Organizacao.GuidOrganizacao, tipoDocumentalModelSearched.Organizacao.GuidOrganizacao);
        }
        #endregion
                
        #region Pagination Search by GuidOrganização
        #region Guid Organização
        [TestMethod]
        public void TipoDocumentalTestPaginationSearchWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                _core.Search(Guid.Empty, default(int), default(int));

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
        public void TipoDocumentalTestPaginationSearchWithInvalidPage()
        {
            bool ok = false;

            try
            {
                _core.Search(_guidGees, default(int), default(int));

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
        public void TipoDocumentalTestPaginationSearchWithInvalidCount()
        {
            bool ok = false;

            try
            {
                _core.Search(_guidGees, 1, default(int));

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
        public void TipoDocumentalTestPaginationSearchWithGuidOrganizacaoNonexistentOnDataBase()
        {
            List<TipoDocumentalModel> tiposDocumentaisModel = _core.Search(Guid.NewGuid(), 1, 1);

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

            List<TipoDocumentalModel> tiposDocumentaisModel = _core.Search(_guidGees, page, count);
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
