using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Model;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.ItemPlanoClassificacao
{
    [TestClass]
    public class UnitTestItemPlanoClassificacaoInsert : UnitTestItemPlanoClassificacaoCommon
    {
        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsert()
        {
            string codigo = "01";
            string descricao = "Plano de Classidifação Descrição Teste";

            PlanoClassificacaoModel planoClassificacaoModel = await InsertPlanoClassificacaoModelAsync();

            NivelClassificacaoModel nivelClassificacaoModel = await InsertNivelClassificacaoModelAsync();

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel { Codigo = codigo, Descricao = descricao, PlanoClassificacao = planoClassificacaoModel, NivelClassificacao = nivelClassificacaoModel };
            itemPlanoClassificacaoModel = await _core.InsertAsync(itemPlanoClassificacaoModel);

            Assert.IsTrue(itemPlanoClassificacaoModel.Id > 0);
            Assert.AreEqual(itemPlanoClassificacaoModel.Codigo, codigo);
            Assert.AreEqual(itemPlanoClassificacaoModel.Descricao, descricao);
            Assert.IsFalse(itemPlanoClassificacaoModel.PlanoClassificacao == null);
            Assert.IsTrue(itemPlanoClassificacaoModel.PlanoClassificacao.Id == planoClassificacaoModel.Id);
            Assert.IsFalse(itemPlanoClassificacaoModel.NivelClassificacao == null);
            Assert.IsTrue(itemPlanoClassificacaoModel.NivelClassificacao.Id == nivelClassificacaoModel.Id);
        }
    }
}
