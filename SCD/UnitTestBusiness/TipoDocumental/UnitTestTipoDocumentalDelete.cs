using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.TipoDocumental
{
    [TestClass]
    public class UnitTestTipoDocumentalDelete : UnitTestTipoDocumentalCommon
    {
        #region Id
        [TestMethod]
        public async Task TipoDocumentalTestDeleteWithInvalidId()
        {
            bool ok = false;

            try
            {
                await _core.DeleteAsync(default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O id não pode ser nulo ou vazio.");
            }

            if (ok)
                Assert.Fail("Não deveria ter excluído com id zero.");
        }
        #endregion

        [TestMethod]
        public async Task TipoDocumentalTestDeleteWithIdNonexistentOnDataBase()
        {
            bool ok = false;

            try
            {
                await _core.DeleteAsync(-1);

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

        //TODO: Após a implementação dos CRUDs de Itens de Plnao de Classificacação fazer os testes de remoção de tipo documental com itens associados

        [TestMethod]
        public async Task TipoDocumentalTestDelete()
        {
            TipoDocumentalModel tipoDocumentalModel = await InsertAsync();

            await _core.DeleteAsync(tipoDocumentalModel.Id);

            tipoDocumentalModel = await SearchAsync(tipoDocumentalModel.Id);

            if (tipoDocumentalModel != null)
                Assert.Fail("O repositório não deveria conter um Tipo Documental excluído.");
        }
    }
}