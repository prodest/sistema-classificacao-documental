using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.Sigilo
{
    [TestClass]
    public class UnitTestSigilo : UnitTestSigiloCommon
    {
        //[TestMethod]
        //public async Task SigiloTestInsertNull()
        //{
        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(null);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "O Item do Plano de Classificação não pode ser nulo.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com objeto nulo.");
        //}

        //[TestMethod]
        //public async Task SigiloTestInsertPlanoClassificacaoNull()
        //{
        //    SigiloModel sigiloModel = new SigiloModel();

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(sigiloModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "O Plano de Classificação não pode ser nulo.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com o Plano de Classificação nulo.");
        //}

        //[TestMethod]
        //public async Task SigiloTestInsertNivelClassificacaoNull()
        //{
        //    SigiloModel sigiloModel = new SigiloModel { PlanoClassificacao = new PlanoClassificacaoModel() };

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(sigiloModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "O Nível de Classificação não pode ser nulo.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com o Nível de Classificação nulo.");
        //}

        //#region Código
        //[TestMethod]
        //public async Task SigiloTestInsertWithCodigoNull()
        //{
        //    SigiloModel sigiloModel = new SigiloModel
        //    {
        //        PlanoClassificacao = new PlanoClassificacaoModel(),
        //        NivelClassificacao = new NivelClassificacaoModel()
        //    };

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(sigiloModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com código nulo.");
        //}

        //[TestMethod]
        //public async Task SigiloTestInsertWithCodigoEmpty()
        //{
        //    SigiloModel sigiloModel = new SigiloModel
        //    {
        //        PlanoClassificacao = new PlanoClassificacaoModel(),
        //        NivelClassificacao = new NivelClassificacaoModel(),
        //        Codigo = ""
        //    };

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(sigiloModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com código vazio.");
        //}

        //[TestMethod]
        //public async Task SigiloTestInsertWithCodigoTrimEmpty()
        //{
        //    SigiloModel sigiloModel = new SigiloModel
        //    {
        //        PlanoClassificacao = new PlanoClassificacaoModel(),
        //        NivelClassificacao = new NivelClassificacaoModel(),
        //        Codigo = " "
        //    };

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(sigiloModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "O código não pode ser vazio ou nulo.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com código somente com espaço.");
        //}
        //#endregion

        //#region Descrição
        //[TestMethod]
        //public async Task SigiloTestInsertWithDescricaoNull()
        //{
        //    SigiloModel sigiloModel = new SigiloModel
        //    {
        //        PlanoClassificacao = new PlanoClassificacaoModel(),
        //        NivelClassificacao = new NivelClassificacaoModel(),
        //        Codigo = "01"
        //    };

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(sigiloModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com descrição nula.");
        //}

        //[TestMethod]
        //public async Task SigiloTestInsertWithDescricaoEmpty()
        //{
        //    SigiloModel sigiloModel = new SigiloModel
        //    {
        //        PlanoClassificacao = new PlanoClassificacaoModel(),
        //        NivelClassificacao = new NivelClassificacaoModel(),
        //        Codigo = "01",
        //        Descricao = ""
        //    };

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(sigiloModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com descrição vazia.");
        //}

        //[TestMethod]
        //public async Task SigiloTestInsertWithDescricaoTrimEmpty()
        //{
        //    SigiloModel sigiloModel = new SigiloModel
        //    {
        //        PlanoClassificacao = new PlanoClassificacaoModel(),
        //        NivelClassificacao = new NivelClassificacaoModel(),
        //        Codigo = "01",
        //        Descricao = " "
        //    };

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(sigiloModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com descrição vazia.");
        //}
        //#endregion

        //[TestMethod]
        //public async Task SigiloTestInsertWithInvalidIdPlanoClassificacao()
        //{
        //    SigiloModel sigiloModel = new SigiloModel
        //    {
        //        PlanoClassificacao = new PlanoClassificacaoModel(),
        //        NivelClassificacao = new NivelClassificacaoModel(),
        //        Codigo = "01",
        //        Descricao = "SigiloTestInsertWithInvalidIdPlanoClassificacao"
        //    };

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(sigiloModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "Identificador do Plano de Classificação inválido.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com descrição vazia.");
        //}

        //[TestMethod]
        //public async Task SigiloTestInsertWithInvalidIdNivelClassificacao()
        //{
        //    SigiloModel sigiloModel = new SigiloModel
        //    {
        //        PlanoClassificacao = new PlanoClassificacaoModel { Id = 1},
        //        NivelClassificacao = new NivelClassificacaoModel(),
        //        Codigo = "01",
        //        Descricao = "SigiloTestInsertWithInvalidIdPlanoClassificacao"
        //    };

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(sigiloModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "Identificador do Nível de Classificação inválido.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com descrição vazia.");
        //}

        [TestMethod]
        public async Task SigiloTestInsert()
        {
            string codigo = "01";
            string descricao = "SigiloTestInsert";
            string fundamentoLegal = "Fundamento Legal Teste";
            string justificativa = "Justificativa Teste";
            int prazoTermino = 5;
            SigiloModel.UnidadePrazoTerminoSigilo unidadePrazoTerminoSigilo = SigiloModel.UnidadePrazoTerminoSigilo.Anos;
            SigiloModel.GrauSigilo grauSigilo = SigiloModel.GrauSigilo.UltraSecreto;
            bool gerarTermo = true;

            DocumentoModel documentoModel = await InsertDocumentoAsync();

            SigiloModel sigiloModel = new SigiloModel
            {
                Codigo = codigo,
                Descricao = descricao,
                FundamentoLegal = fundamentoLegal,
                Justificativa = justificativa,
                PrazoTermino = prazoTermino,
                UnidadePrazoTermino = unidadePrazoTerminoSigilo,
                Grau = grauSigilo,
                GerarTermo = gerarTermo,
                Documento = documentoModel
            };

            sigiloModel = await _core.InsertAsync(sigiloModel);
            _idsSigilosTestados.Add(sigiloModel.Id);

            Assert.IsTrue(sigiloModel.Id > 0);
            Assert.AreEqual(sigiloModel.Codigo, codigo);
            Assert.AreEqual(sigiloModel.Descricao, descricao);
            Assert.AreEqual(sigiloModel.FundamentoLegal, fundamentoLegal);
            Assert.AreEqual(sigiloModel.Justificativa, justificativa);
            Assert.AreEqual(sigiloModel.PrazoTermino, prazoTermino);
            Assert.AreEqual(sigiloModel.UnidadePrazoTermino, unidadePrazoTerminoSigilo);
            Assert.AreEqual(sigiloModel.Grau, grauSigilo);
            Assert.AreEqual(sigiloModel.GerarTermo, gerarTermo);

            Assert.IsFalse(sigiloModel.Documento == null);
            Assert.IsTrue(sigiloModel.Documento.Id == documentoModel.Id);
        }

        [TestMethod]
        public async Task SigiloTestSearch()
        {
            SigiloModel sigiloModel = await InsertSigiloAsync();

            SigiloModel sigiloModelSearched = await _core.SearchAsync(sigiloModel.Id);

            Assert.AreEqual(sigiloModel.Id, sigiloModelSearched.Id);
            Assert.AreEqual(sigiloModel.Codigo, sigiloModelSearched.Codigo);
            Assert.AreEqual(sigiloModel.Descricao, sigiloModelSearched.Descricao);
            Assert.AreEqual(sigiloModel.FundamentoLegal, sigiloModelSearched.FundamentoLegal);
            Assert.AreEqual(sigiloModel.Justificativa, sigiloModelSearched.Justificativa);
            Assert.AreEqual(sigiloModel.PrazoTermino, sigiloModelSearched.PrazoTermino);
            Assert.AreEqual(sigiloModel.UnidadePrazoTermino, sigiloModelSearched.UnidadePrazoTermino);
            Assert.AreEqual(sigiloModel.Grau, sigiloModelSearched.Grau);
            Assert.AreEqual(sigiloModel.GerarTermo, sigiloModelSearched.GerarTermo);
            Assert.AreEqual(sigiloModel.Documento.Id, sigiloModelSearched.Documento.Id);
        }

        [TestMethod]
        public async Task SigiloTestUpdate()
        {
            SigiloModel sigiloModel = await InsertSigiloAsync();

            sigiloModel.Codigo = "02";
            sigiloModel.Descricao = "Descrição updated";
            sigiloModel.FundamentoLegal = "Fundamento updated";
            sigiloModel.GerarTermo = false;
            sigiloModel.Grau = SigiloModel.GrauSigilo.InformacaoPessoal;
            sigiloModel.Justificativa = "justificativa updated";
            sigiloModel.PrazoTermino = 1;
            sigiloModel.UnidadePrazoTermino = SigiloModel.UnidadePrazoTerminoSigilo.Dias;

            //TODO: Testar a mudança e documento

            await _core.UpdateAsync(sigiloModel);

            SigiloModel sigiloModelSearched = await _core.SearchAsync(sigiloModel.Id);

            Assert.AreEqual(sigiloModel.Id, sigiloModelSearched.Id);
            Assert.AreEqual(sigiloModel.Codigo, sigiloModelSearched.Codigo);
            Assert.AreEqual(sigiloModel.Descricao, sigiloModelSearched.Descricao);
            Assert.AreEqual(sigiloModel.FundamentoLegal, sigiloModelSearched.FundamentoLegal);
            Assert.AreEqual(sigiloModel.Justificativa, sigiloModelSearched.Justificativa);
            Assert.AreEqual(sigiloModel.PrazoTermino, sigiloModelSearched.PrazoTermino);
            Assert.AreEqual(sigiloModel.UnidadePrazoTermino, sigiloModelSearched.UnidadePrazoTermino);
            Assert.AreEqual(sigiloModel.Grau, sigiloModelSearched.Grau);
            Assert.AreEqual(sigiloModel.GerarTermo, sigiloModelSearched.GerarTermo);
            Assert.AreEqual(sigiloModel.Documento.Id, sigiloModelSearched.Documento.Id);
        }

        [TestMethod]
        public async Task SigiloTestDelete()
        {
            SigiloModel sigiloModel = await InsertSigiloAsync();

            await _core.DeleteAsync(sigiloModel.Id);

            bool ok = false;

            try
            {
                await _core.SearchAsync(sigiloModel.Id);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Sigilo não encontrado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter encontrado o Siglo excluído.");
        }
    }
}
