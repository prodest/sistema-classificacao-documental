using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.TipoDocumental
{
    [TestClass]
    public class UnitTestTipoDocumentalInsert : UnitTestTipoDocumentalCommon
    {
        [TestMethod]
        public async Task TipoDocumentalTestInsertNull()
        {
            bool ok = false;
            TipoDocumentalModel tipoDocumentalModel = null;

            try
            {
                tipoDocumentalModel = await _core.InsertAsync(null);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O Tipo Documental n�o pode ser nulo.");
            }

            if (ok)
            {
                _idsTiposDocumentaisTestados.Add(tipoDocumentalModel.Id);

                Assert.Fail("N�o deveria ter inserido com objeto nulo.");
            }
        }

        #region Descri��o
        [TestMethod]
        public async Task TipoDocumentalTestInsertWithDescricaoNull()
        {
            TipoDocumentalModel tipoDocumentalModel = new TipoDocumentalModel();

            bool ok = false;

            try
            {
                tipoDocumentalModel = await _core.InsertAsync(tipoDocumentalModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descri��o n�o pode ser vazia ou nula.");
            }

            if (ok)
            {
                _idsTiposDocumentaisTestados.Add(tipoDocumentalModel.Id);

                Assert.Fail("N�o deveria ter inserido com descri��o nula.");
            }
        }

        [TestMethod]
        public async Task TipoDocumentalTestInsertWithDescricaoEmpty()
        {
            TipoDocumentalModel tipoDocumentalModel = new TipoDocumentalModel { Descricao = "" };

            bool ok = false;

            try
            {
                tipoDocumentalModel = await _core.InsertAsync(tipoDocumentalModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descri��o n�o pode ser vazia ou nula.");
            }

            if (ok)
            {
                _idsTiposDocumentaisTestados.Add(tipoDocumentalModel.Id);

                Assert.Fail("N�o deveria ter inserido com descri��o vazia.");
            }
        }

        [TestMethod]
        public async Task TipoDocumentalTestInsertWithDescricaoTrimEmpty()
        {
            TipoDocumentalModel tipoDocumentalModel = new TipoDocumentalModel { Descricao = " " };

            bool ok = false;

            try
            {
                tipoDocumentalModel = await _core.InsertAsync(tipoDocumentalModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descri��o n�o pode ser vazia ou nula.");
            }

            if (ok)
            {
                _idsTiposDocumentaisTestados.Add(tipoDocumentalModel.Id);

                Assert.Fail("N�o deveria ter inserido com descri��o somente com espa�o.");
            }
        }
        #endregion

        #region Id
        [TestMethod]
        public async Task TipoDocumentalTestInsertWithInvalidInsertId()
        {
            TipoDocumentalModel tipoDocumentalModel = new TipoDocumentalModel { Id = 1, Descricao = "Teste" };

            bool ok = false;

            try
            {
                tipoDocumentalModel = await _core.InsertAsync(tipoDocumentalModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O id n�o deve ser preenchido.");
            }

            if (ok)
            {
                _idsTiposDocumentaisTestados.Add(tipoDocumentalModel.Id);

                Assert.Fail("N�o deveria ter inserido com um id inv�lido para inser��o.");
            }
        }
        #endregion

        [TestMethod]
        public async Task TipoDocumentalTestInsert()
        {
            string descricao = "Descri��o Teste";

            TipoDocumentalModel tipoDocumentalModel = new TipoDocumentalModel { Descricao = descricao };

            tipoDocumentalModel = await _core.InsertAsync(tipoDocumentalModel);

            _idsTiposDocumentaisTestados.Add(tipoDocumentalModel.Id);

            Assert.IsTrue(tipoDocumentalModel.Id > 0);
            Assert.AreEqual(tipoDocumentalModel.Descricao, descricao);
            Assert.AreEqual(tipoDocumentalModel.Ativo, true);
            Assert.IsFalse(tipoDocumentalModel.Organizacao == null);
            Assert.IsFalse(tipoDocumentalModel.Organizacao.GuidOrganizacao.Equals(Guid.Empty));
        }
    }
}