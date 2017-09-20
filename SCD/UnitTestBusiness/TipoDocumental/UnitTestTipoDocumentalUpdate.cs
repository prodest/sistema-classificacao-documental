using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.TipoDocumental
{
    [TestClass]
    public class UnitTestTipoDocumentalUpdate : UnitTestTipoDocumentalCommon
    {
        [TestMethod]
        public async Task TipoDocumentalTestUpdateNull()
        {
            bool ok = false;

            try
            {
                await _core.UpdateAsync(null);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O Tipo Documental n�o pode ser nulo.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com objeto nulo.");
        }

        #region Descri��o
        [TestMethod]
        public async Task TipoDocumentalTestUpdateWithDescricaoNull()
        {
            bool ok = false;

            try
            {
                var tipoDocumentalModel = await InsertModelAsync();

                tipoDocumentalModel.Descricao = null;

                await _core.UpdateAsync(tipoDocumentalModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descri��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com descri��o nula.");
        }

        [TestMethod]
        public async Task TipoDocumentalTestUpdateWithDescricaoEmpty()
        {
            bool ok = false;

            try
            {
                var tipoDocumentalModel = await InsertModelAsync();

                tipoDocumentalModel.Descricao = "";

                await _core.UpdateAsync(tipoDocumentalModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descri��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com descri��o vazia.");
        }

        [TestMethod]
        public async Task TipoDocumentalTestUpdateWithDescricaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                var tipoDocumentalModel = await InsertModelAsync();

                tipoDocumentalModel.Descricao = " ";

                await _core.UpdateAsync(tipoDocumentalModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descri��o n�o pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com descri��o somente com espa�o.");
        }
        #endregion

        #region Organiza��o
        [TestMethod]
        public async Task TipoDocumentalTestUpdateGuidOrganizacao()
        {
            bool ok = false;

            try
            {
                var tipoDocumentalModel = await InsertModelAsync();

                tipoDocumentalModel.Organizacao = new OrganizacaoModel { GuidOrganizacao = Guid.NewGuid() };

                await _core.UpdateAsync(tipoDocumentalModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));

                Assert.AreEqual(ex.Message, "N�o � poss�vel atualizar a Organiza��o do Tipo Documental.");
            }

            if (ok)
                Assert.Fail("N�o deveria ter atualizado com o guid da organiza��o n�o existindo no sistema.");
        }
        #endregion

        [TestMethod]
        public async Task TipoDocumentalTestUpdate()
        {
            TipoDocumentalModel tipoDocumentalModel = await InsertModelAsync();

            int id = tipoDocumentalModel.Id;
            string descricao = "TestUpdateWit0hBasicsFieldsDescri��o Teste";
            bool ativo = false;

            tipoDocumentalModel.Descricao = descricao;
            tipoDocumentalModel.Ativo = ativo;

            await _core.UpdateAsync(tipoDocumentalModel);

            tipoDocumentalModel = _core.Search(tipoDocumentalModel.Id);
            //tipoDocumentalModel = SearchModelAsync(tipoDocumentalModel.Id);

            Assert.AreEqual(tipoDocumentalModel.Id, id);
            Assert.AreEqual(tipoDocumentalModel.Descricao, descricao);
            Assert.AreEqual(tipoDocumentalModel.Ativo, ativo);
            //Assert.AreEqual(tipoDocumentalModel.Organizacao.GuidOrganizacao, _guidGees);
        }
    }
}