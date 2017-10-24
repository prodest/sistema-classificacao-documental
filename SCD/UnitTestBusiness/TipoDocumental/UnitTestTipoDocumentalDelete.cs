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

                Assert.AreEqual(ex.Message, "O id n�o pode ser nulo ou vazio.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter exclu�do com id zero.");
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

                Assert.AreEqual(ex.Message, "Tipo Documental n�o encontrado.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter pesquisado com id inexistente na base de dados.");
        }

        //TODO: Ap�s a implementa��o dos CRUDs de Itens de Plnao de Classificaca��o fazer os testes de remo��o de tipo documental com itens associados

        [TestMethod]
        public async Task TipoDocumentalTestDelete()
        {
            TipoDocumentalModel tipoDocumentalModel = await InsertAsync();

            await _core.DeleteAsync(tipoDocumentalModel.Id);

            tipoDocumentalModel = await SearchAsync(tipoDocumentalModel.Id);

            if (tipoDocumentalModel != null)
                Assert.Fail("O reposit�rio n�o deveria conter um Tipo Documental exclu�do.");
        }
    }
}