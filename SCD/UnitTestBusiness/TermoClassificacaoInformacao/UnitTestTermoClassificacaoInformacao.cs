using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.TermoClassificacaoInformacao
{
    [TestClass]
    public class UnitTestTermoClassificacaoInformacao : UnitTestTermoClassificacaoInformacaoCommon
    {
        //[TestMethod]
        //public async Task TermoClassificacaoInformacaoTestInsert()
        //{
        //    int Id { get; set; }
        //    string Codigo { get; set; }
        //    Guid GuidOrganizacao { get; set; }
        //    GrauSigiloModel GrauSigilo { get; set; }
        //    TipoSigiloModel TipoSigilo { get; set; }
        //    string ConteudoSigilo { get; set; }
        //    string IdentificadorDocumento { get; set; }
        //    DateTime DataProducaoDocumento { get; set; }
        //    string FundamentoLegal { get; set; }
        //    string Justificativa { get; set; }
        //    DateTime DataClassificacao { get; set; }
        //    string CpfUsuario { get; set; }
        //    string CpfIndicacaoAprovador { get; set; }

        //    DocumentoModel Documento { get; set; }
        //    CriterioRestricaoModel CriterioRestricao { get; set; }
        //    ItemPlanoClassificacaoModel ItemPlanoClassificacao { get; set; }

        //    string codigo = "01";
        //    string descricao = "TermoClassificacaoInformacaoTestInsert";
        //    string justificativa = "Jsutificativa Teste";
        //    string fundamentoLegal = "Fundamento Legal Teste";
        //    bool classificavel = true;
        //    TermoClassificacaoInformacaoModel.GrauSigilo grauSigilo = TermoClassificacaoInformacaoModel.GrauSigilo.Reservado;
        //    int prazoTermino = 5;
        //    UnidadeTempo unidadePrazoTermino = UnidadeTempo.Anos;

        //    PlanoClassificacaoModel planoClassificacaoModel = await InsertPlanoClassificacaoAsync();
        //    DocumentoModel documentoModel = await InsertDocumentoAsync();

        //    TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel = new TermoClassificacaoInformacaoModel
        //    {
        //        Codigo = codigo,
        //        Descricao = descricao,
        //        Justificativa = justificativa,
        //        FundamentoLegal = fundamentoLegal,
        //        Classificavel = classificavel,
        //        Grau = grauSigilo,
        //        PrazoTermino = prazoTermino,
        //        UnidadePrazoTermino = unidadePrazoTermino,

        //        PlanoClassificacao = planoClassificacaoModel,

        //        Documentos = new List<DocumentoModel>() { documentoModel }
        //    };

        //    termoClassificacaoInformacaoModel = await _core.InsertAsync(termoClassificacaoInformacaoModel);
        //    _idsCriteriosRestricaoTestados.Add(termoClassificacaoInformacaoModel.Id);

        //    Assert.IsTrue(termoClassificacaoInformacaoModel.Id > 0);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Codigo, codigo);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Descricao, descricao);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Justificativa, justificativa);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.FundamentoLegal, fundamentoLegal);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Classificavel, classificavel);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Grau, grauSigilo);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.PrazoTermino, prazoTermino);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.UnidadePrazoTermino, unidadePrazoTermino);
        //    Assert.IsNull(termoClassificacaoInformacaoModel.EventoFim);

        //    Assert.IsFalse(termoClassificacaoInformacaoModel.PlanoClassificacao == null);
        //    Assert.IsTrue(termoClassificacaoInformacaoModel.PlanoClassificacao.Id == planoClassificacaoModel.Id);

        //    Assert.IsFalse(termoClassificacaoInformacaoModel.Documentos == null);
        //    Assert.IsTrue(termoClassificacaoInformacaoModel.Documentos.Count == 1);
        //    Assert.IsTrue(termoClassificacaoInformacaoModel.Documentos.Single().Id == documentoModel.Id);
        //}

        //[TestMethod]
        //public async Task TermoClassificacaoInformacaoTestSearch()
        //{
        //    TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel = await InsertTermoClassificacaoInformacaoAsync();

        //    TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModelSearched = await _core.SearchAsync(termoClassificacaoInformacaoModel.Id);

        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Id, termoClassificacaoInformacaoModelSearched.Id);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Codigo, termoClassificacaoInformacaoModelSearched.Codigo);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Descricao, termoClassificacaoInformacaoModelSearched.Descricao);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Justificativa, termoClassificacaoInformacaoModelSearched.Justificativa);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.FundamentoLegal, termoClassificacaoInformacaoModelSearched.FundamentoLegal);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Classificavel, termoClassificacaoInformacaoModelSearched.Classificavel);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Grau, termoClassificacaoInformacaoModelSearched.Grau);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.PrazoTermino, termoClassificacaoInformacaoModelSearched.PrazoTermino);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.UnidadePrazoTermino, termoClassificacaoInformacaoModelSearched.UnidadePrazoTermino);
        //    Assert.IsNull(termoClassificacaoInformacaoModel.EventoFim);

        //    Assert.IsFalse(termoClassificacaoInformacaoModel.PlanoClassificacao == null);
        //    Assert.IsTrue(termoClassificacaoInformacaoModel.PlanoClassificacao.Id == termoClassificacaoInformacaoModelSearched.PlanoClassificacao.Id);

        //    Assert.IsFalse(termoClassificacaoInformacaoModel.Documentos == null);
        //    Assert.IsTrue(termoClassificacaoInformacaoModel.Documentos.Count == 1);
        //    Assert.IsTrue(termoClassificacaoInformacaoModel.Documentos.Single().Id == termoClassificacaoInformacaoModelSearched.Documentos.Single().Id);
        //}

        //[TestMethod]
        //public async Task TermoClassificacaoInformacaoTestUpdate()
        //{
        //    TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel = await InsertTermoClassificacaoInformacaoAsync();

        //    termoClassificacaoInformacaoModel.Codigo = "02";
        //    termoClassificacaoInformacaoModel.Descricao = "Descrição updated";
        //    termoClassificacaoInformacaoModel.Justificativa = "Jutificativa Teste update";
        //    termoClassificacaoInformacaoModel.FundamentoLegal= "Fundamento Legal Teste update";
        //    termoClassificacaoInformacaoModel.Classificavel = false;
        //    termoClassificacaoInformacaoModel.Grau = TermoClassificacaoInformacaoModel.GrauSigilo.Secreto;
        //    termoClassificacaoInformacaoModel.PrazoTermino = 1;
        //    termoClassificacaoInformacaoModel.UnidadePrazoTermino = UnidadeTempo.Dias;

        //    await _core.UpdateAsync(termoClassificacaoInformacaoModel);

        //    TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModelSearched = await _core.SearchAsync(termoClassificacaoInformacaoModel.Id);

        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Id, termoClassificacaoInformacaoModelSearched.Id);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Codigo, termoClassificacaoInformacaoModelSearched.Codigo);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Descricao, termoClassificacaoInformacaoModelSearched.Descricao);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Justificativa, termoClassificacaoInformacaoModelSearched.Justificativa);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.FundamentoLegal, termoClassificacaoInformacaoModelSearched.FundamentoLegal);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Classificavel, termoClassificacaoInformacaoModelSearched.Classificavel);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.Grau, termoClassificacaoInformacaoModelSearched.Grau);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.PrazoTermino, termoClassificacaoInformacaoModelSearched.PrazoTermino);
        //    Assert.AreEqual(termoClassificacaoInformacaoModel.UnidadePrazoTermino, termoClassificacaoInformacaoModelSearched.UnidadePrazoTermino);
        //    Assert.IsNull(termoClassificacaoInformacaoModel.EventoFim);

        //    Assert.IsFalse(termoClassificacaoInformacaoModel.PlanoClassificacao == null);
        //    Assert.IsTrue(termoClassificacaoInformacaoModel.PlanoClassificacao.Id == termoClassificacaoInformacaoModelSearched.PlanoClassificacao.Id);

        //    Assert.IsFalse(termoClassificacaoInformacaoModel.Documentos == null);
        //    Assert.IsTrue(termoClassificacaoInformacaoModel.Documentos.Count == 1);
        //    Assert.IsTrue(termoClassificacaoInformacaoModel.Documentos.Single().Id == termoClassificacaoInformacaoModelSearched.Documentos.Single().Id);
        //}

        //[TestMethod]
        //public async Task TermoClassificacaoInformacaoTestDelete()
        //{
        //    TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel = await InsertTermoClassificacaoInformacaoAsync();

        //    await _core.DeleteAsync(termoClassificacaoInformacaoModel.Id);

        //    bool ok = false;

        //    try
        //    {
        //        await _core.SearchAsync(termoClassificacaoInformacaoModel.Id);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "Critério de Restrição não encontrado.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter encontrado o Critério de Restrição excluído.");
        //}
    }
}
