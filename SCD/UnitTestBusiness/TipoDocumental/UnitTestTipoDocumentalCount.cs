using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.TipoDocumental
{
    [TestClass]
    public class UnitTestTipoDocumentalCount : UnitTestTipoDocumentalCommon
    {
        #region Guid Organização
        [TestMethod]
        public void TipoDocumentalTestCountWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                _core.Count(Guid.Empty);

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

        [TestMethod]
        public void TipoDocumentalTestCountWithGuidOrganizacaoNonexistentOnDataBase()
        {
            int count = _core.Count(Guid.NewGuid());

            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public async Task TipoDocumentalTestCountWithGuidOrganizacaoCorrect()
        {
            await InsertAsync();

            int count = _core.Count(_guidGees);

            Assert.IsTrue(count > 0);
        }
    }
}