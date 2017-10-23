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
        public async Task TipoDocumentalTestCountWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                await _core.CountAsync(Guid.Empty);

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
        public async Task TipoDocumentalTestCountWithGuidOrganizacaoNonexistentOnDataBase()
        {
            int count = await _core.CountAsync(Guid.NewGuid());

            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public async Task TipoDocumentalTestCountWithGuidOrganizacaoCorrect()
        {
            await InsertAsync();

            int count = await _core.CountAsync(_guidGees);

            Assert.IsTrue(count > 0);
        }
    }
}