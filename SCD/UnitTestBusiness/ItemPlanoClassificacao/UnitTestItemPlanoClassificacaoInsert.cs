using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.ItemPlanoClassificacao
{
    [TestClass]
    public class UnitTestItemPlanoClassificacaoInsert : UnitTestItemPlanoClassificacaoCommon
    {
        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsertNull()
        {
            bool ok = false;

            try
            {
                await _core.InsertAsync(null);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O Item do Plano de Classificação não pode ser nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com objeto nulo.");
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsertPlanoClassificacaoNull()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel();

            bool ok = false;

            try
            {
                await _core.InsertAsync(itemPlanoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O Plano de Classificação não pode ser nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com o Plano de Classificação nulo.");
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsertNivelClassificacaoNull()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel { PlanoClassificacao = new PlanoClassificacaoModel() };

            bool ok = false;

            try
            {
                await _core.InsertAsync(itemPlanoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O Nível de Classificação não pode ser nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com o Nível de Classificação nulo.");
        }

        #region Código
        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsertWithCodigoNull()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel
            {
                PlanoClassificacao = new PlanoClassificacaoModel(),
                NivelClassificacao = new NivelClassificacaoModel()
            };

            bool ok = false;

            try
            {
                await _core.InsertAsync(itemPlanoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com código nulo.");
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsertWithCodigoEmpty()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel
            {
                PlanoClassificacao = new PlanoClassificacaoModel(),
                NivelClassificacao = new NivelClassificacaoModel(),
                Codigo = ""
            };

            bool ok = false;

            try
            {
                await _core.InsertAsync(itemPlanoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com código vazio.");
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsertWithCodigoTrimEmpty()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel
            {
                PlanoClassificacao = new PlanoClassificacaoModel(),
                NivelClassificacao = new NivelClassificacaoModel(),
                Codigo = " "
            };

            bool ok = false;

            try
            {
                await _core.InsertAsync(itemPlanoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com código somente com espaço.");
        }
        #endregion

        #region Descrição
        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsertWithDescricaoNull()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel
            {
                PlanoClassificacao = new PlanoClassificacaoModel(),
                NivelClassificacao = new NivelClassificacaoModel(),
                Codigo = "01"
            };

            bool ok = false;

            try
            {
                await _core.InsertAsync(itemPlanoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição nula.");
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsertWithDescricaoEmpty()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel
            {
                PlanoClassificacao = new PlanoClassificacaoModel(),
                NivelClassificacao = new NivelClassificacaoModel(),
                Codigo = "01",
                Descricao = ""
            };

            bool ok = false;

            try
            {
                await _core.InsertAsync(itemPlanoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição vazia.");
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsertWithDescricaoTrimEmpty()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel
            {
                PlanoClassificacao = new PlanoClassificacaoModel(),
                NivelClassificacao = new NivelClassificacaoModel(),
                Codigo = "01",
                Descricao = " "
            };

            bool ok = false;

            try
            {
                await _core.InsertAsync(itemPlanoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição vazia.");
        }
        #endregion

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsertWithInvalidIdPlanoClassificacao()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel
            {
                PlanoClassificacao = new PlanoClassificacaoModel(),
                NivelClassificacao = new NivelClassificacaoModel(),
                Codigo = "01",
                Descricao = "ItemPlanoClassificacaoTestInsertWithInvalidIdPlanoClassificacao"
            };

            bool ok = false;

            try
            {
                await _core.InsertAsync(itemPlanoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Identificador do Plano de Classificação inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição vazia.");
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsertWithInvalidIdNivelClassificacao()
        {
            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel
            {
                PlanoClassificacao = new PlanoClassificacaoModel { Id = 1},
                NivelClassificacao = new NivelClassificacaoModel(),
                Codigo = "01",
                Descricao = "ItemPlanoClassificacaoTestInsertWithInvalidIdPlanoClassificacao"
            };

            bool ok = false;

            try
            {
                await _core.InsertAsync(itemPlanoClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Identificador do Nível de Classificação inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter inserido com descrição vazia.");
        }

        [TestMethod]
        public async Task ItemPlanoClassificacaoTestInsert()
        {
            string codigo = "01";
            string descricao = "ItemPlanoClassificacaoTestInsert";

            PlanoClassificacaoModel planoClassificacaoModel = await InsertPlanoClassificacaoModelAsync();

            NivelClassificacaoModel nivelClassificacaoModel = await InsertNivelClassificacaoModelAsync();

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = new ItemPlanoClassificacaoModel
            {
                PlanoClassificacao = planoClassificacaoModel,
                NivelClassificacao = nivelClassificacaoModel,
                Codigo = codigo,
                Descricao = descricao
            };
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
