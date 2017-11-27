using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Model;
using System;
using System.Threading.Tasks;
using static Prodest.Scd.Business.Model.TermoClassificacaoInformacaoModel;

namespace Prodest.Scd.UnitTestBusiness.TermoClassificacaoInformacao
{
    [TestClass]
    public class UnitTestTermoClassificacaoInformacao : UnitTestTermoClassificacaoInformacaoCommon
    {
        [TestMethod]
        public async Task TermoClassificacaoInformacaoTestInsert()
        {
            DateTime dataTeste = DateTime.Now;

            string codigo = "01";
            GrauSigiloModel grauSigilo = GrauSigiloModel.Reservado;
            TipoSigiloModel tipoSigilo = TipoSigiloModel.Parcial;
            string conteudoSigilo = "Conteúdo Sigilo";
            string identificadorDocumento = "Identificador Documento";
            DateTime dataProducaoDocumento = DateTime.Now;
            string fundamentoLegal = "Fundamento Legal";
            string justificativa = "Justificativa";
            string cpfIndicacaoAprovador = "11111111111";

            DocumentoModel documento = await InsertDocumentoAsync();
            CriterioRestricaoModel criterioRestricao = await InsertCriterioRestricaoAsync(documento);

            TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel = new TermoClassificacaoInformacaoModel
            {
                Codigo = codigo,
                GrauSigilo = grauSigilo,
                TipoSigilo = tipoSigilo,
                ConteudoSigilo = conteudoSigilo,
                IdentificadorDocumento = identificadorDocumento,
                DataProducaoDocumento = dataProducaoDocumento,
                FundamentoLegal = fundamentoLegal,
                Justificativa = justificativa,
                CpfIndicacaoAprovador = cpfIndicacaoAprovador,

                Documento = documento,
                CriterioRestricao = criterioRestricao
            };

            termoClassificacaoInformacaoModel = await _core.InsertAsync(termoClassificacaoInformacaoModel);
            _idsTermosClassificacaoInformacaoTestados.Add(termoClassificacaoInformacaoModel.Id);

            Assert.IsTrue(termoClassificacaoInformacaoModel.Id > 0);
            Assert.AreEqual(termoClassificacaoInformacaoModel.Codigo, codigo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.GrauSigilo, grauSigilo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.TipoSigilo, tipoSigilo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.ConteudoSigilo, conteudoSigilo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.IdentificadorDocumento, identificadorDocumento);
            Assert.AreEqual(termoClassificacaoInformacaoModel.DataProducaoDocumento, dataProducaoDocumento);
            Assert.AreEqual(termoClassificacaoInformacaoModel.FundamentoLegal, fundamentoLegal);
            Assert.AreEqual(termoClassificacaoInformacaoModel.Justificativa, justificativa);
            Assert.AreEqual(termoClassificacaoInformacaoModel.CpfIndicacaoAprovador, cpfIndicacaoAprovador);

            Assert.IsNotNull(termoClassificacaoInformacaoModel.CpfUsuario);
            Assert.IsTrue(termoClassificacaoInformacaoModel.DataClassificacao >= dataTeste);

            Assert.IsFalse(termoClassificacaoInformacaoModel.Documento == null);
            Assert.AreEqual(termoClassificacaoInformacaoModel.Documento.Id, documento.Id);

            Assert.IsFalse(termoClassificacaoInformacaoModel.CriterioRestricao == null);
            Assert.AreEqual(termoClassificacaoInformacaoModel.CriterioRestricao.Id, criterioRestricao.Id);
        }

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
