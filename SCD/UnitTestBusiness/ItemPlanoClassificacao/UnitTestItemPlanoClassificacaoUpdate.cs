using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.ItemPlanoClassificacao
{
    [TestClass]
    public class UnitTestItemPlanoClassificacaoUpdate : UnitTestItemPlanoClassificacaoCommon
    {
        [TestMethod]
        public async Task ItemPlanoClassificacaoTestUpdateNull()
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

                Assert.AreEqual(ex.Message, "O Item do Plano de Classificação não pode ser nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com objeto nulo.");
        }

        #region Descrição
        [TestMethod]
        public async Task ItemPlanoClassificacaoTestUpdateWithDescricaoNull()
        {
            bool ok = false;

            try
            {
                ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await InsertItemPlanoClassificacaoAsync();

                itemPlanoClassificacaoModel.Descricao = null;

                await _core.UpdateAsync(itemPlanoClassificacaoModel);

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
        public async Task ItemPlanoClassificacaoTestUpdateWithDescricaoEmpty()
        {
            bool ok = false;

            try
            {
                ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await InsertItemPlanoClassificacaoAsync();

                itemPlanoClassificacaoModel.Descricao = "";

                await _core.UpdateAsync(itemPlanoClassificacaoModel);

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
        public async Task ItemPlanoClassificacaoTestUpdateWithDescricaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await InsertItemPlanoClassificacaoAsync();

                itemPlanoClassificacaoModel.Descricao = " ";

                await _core.UpdateAsync(itemPlanoClassificacaoModel);
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

        #region Plano Classificação
        //[TestMethod]
        //public async Task ItemPlanoClassificacaoTestUpdateGuidOrganizacao()
        //{
        //    bool ok = false;

        //    try
        //    {
        //        var itemPlanoClassificacaoModel = await InsertItemPlanoClassificacaoAsync();

        //        itemPlanoClassificacaoModel.Organizacao = new OrganizacaoModel { GuidOrganizacao = Guid.NewGuid() };

        //        await _core.UpdateAsync(itemPlanoClassificacaoModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(Exception));

        //        Assert.AreEqual(ex.Message, "Não é possível atualizar a Organização do Tipo Documental.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter atualizado com o guid da organização não existindo no sistema.");
        //}
        #endregion

        //[TestMethod]
        //public async Task ItemPlanoClassificacaoTestUpdate()
        //{
        //    ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = await InsertModelAsync();

        //    int id = itemPlanoClassificacaoModel.Id;
        //    string descricao = "TestUpdateWit0hBasicsFieldsDescrição Teste";
        //    bool ativo = false;

        //    itemPlanoClassificacaoModel.Descricao = descricao;
        //    itemPlanoClassificacaoModel.Ativo = ativo;

        //    await _core.UpdateAsync(itemPlanoClassificacaoModel);

        //    itemPlanoClassificacaoModel = await _core.SearchAsync(itemPlanoClassificacaoModel.Id);
        //    //itemPlanoClassificacaoModel = SearchModelAsync(itemPlanoClassificacaoModel.Id);

        //    Assert.AreEqual(itemPlanoClassificacaoModel.Id, id);
        //    Assert.AreEqual(itemPlanoClassificacaoModel.Descricao, descricao);
        //    Assert.AreEqual(itemPlanoClassificacaoModel.Ativo, ativo);
        //    //Assert.AreEqual(itemPlanoClassificacaoModel.Organizacao.GuidOrganizacao, _guidGees);
        //}
    }
}