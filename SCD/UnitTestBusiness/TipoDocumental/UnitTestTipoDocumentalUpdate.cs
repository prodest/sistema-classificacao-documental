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

                Assert.AreEqual(ex.Message, "O Tipo Documental não pode ser nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com objeto nulo.");
        }

        #region Descrição
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com descrição nula.");
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com descrição vazia.");
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

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com descrição somente com espaço.");
        }
        #endregion

        #region Organização
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

                Assert.AreEqual(ex.Message, "Não é possível atualizar a Organização do Tipo Documental.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com o guid da organização não existindo no sistema.");
        }
        #endregion

        [TestMethod]
        public async Task TipoDocumentalTestUpdate()
        {
            TipoDocumentalModel tipoDocumentalModel = await InsertModelAsync();

            int id = tipoDocumentalModel.Id;
            string descricao = "TestUpdateWit0hBasicsFieldsDescrição Teste";
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