using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.CriterioRestricao
{
    [TestClass]
    public class UnitTestCriterioRestricao : UnitTestCriterioRestricaoCommon
    {
        [TestMethod]
        public async Task CriterioRestricaoTestInsert()
        {
            string codigo = "01";
            string descricao = "CriterioRestricaoTestInsert";
            string justificativa = "Jsutificativa Teste";
            string fundamentoLegal = "Fundamento Legal Teste";
            bool classificavel = true;
            CriterioRestricaoModel.GrauSigilo grauSigilo = CriterioRestricaoModel.GrauSigilo.Reservado;
            int prazoTermino = 5;
            UnidadeTempo unidadePrazoTermino = UnidadeTempo.Anos;

            PlanoClassificacaoModel planoClassificacaoModel = await InsertPlanoClassificacaoAsync();
            DocumentoModel documentoModel = await InsertDocumentoAsync();

            CriterioRestricaoModel criterioRestricaoModel = new CriterioRestricaoModel
            {
                Codigo = codigo,
                Descricao = descricao,
                Justificativa = justificativa,
                FundamentoLegal = fundamentoLegal,
                Classificavel = classificavel,
                Grau = grauSigilo,
                PrazoTermino = prazoTermino,
                UnidadePrazoTermino = unidadePrazoTermino,

                PlanoClassificacao = planoClassificacaoModel,

                Documentos = new List<DocumentoModel>() { documentoModel }
            };

            criterioRestricaoModel = await _core.InsertAsync(criterioRestricaoModel);
            _idsCriteriosRestricaoTestados.Add(criterioRestricaoModel.Id);

            Assert.IsTrue(criterioRestricaoModel.Id > 0);
            Assert.AreEqual(criterioRestricaoModel.Codigo, codigo);
            Assert.AreEqual(criterioRestricaoModel.Descricao, descricao);
            Assert.AreEqual(criterioRestricaoModel.Justificativa, justificativa);
            Assert.AreEqual(criterioRestricaoModel.FundamentoLegal, fundamentoLegal);
            Assert.AreEqual(criterioRestricaoModel.Classificavel, classificavel);
            Assert.AreEqual(criterioRestricaoModel.Grau, grauSigilo);
            Assert.AreEqual(criterioRestricaoModel.PrazoTermino, prazoTermino);
            Assert.AreEqual(criterioRestricaoModel.UnidadePrazoTermino, unidadePrazoTermino);
            Assert.IsNull(criterioRestricaoModel.EventoFim);

            Assert.IsFalse(criterioRestricaoModel.PlanoClassificacao == null);
            Assert.IsTrue(criterioRestricaoModel.PlanoClassificacao.Id == planoClassificacaoModel.Id);

            Assert.IsFalse(criterioRestricaoModel.Documentos == null);
            Assert.IsTrue(criterioRestricaoModel.Documentos.Count == 1);
            Assert.IsTrue(criterioRestricaoModel.Documentos.Single().Id == documentoModel.Id);
        }

        //[TestMethod]
        //public async Task CriterioRestricaoTestSearch()
        //{
        //    CriterioRestricaoModel criterioRestricaoModel = await InsertCriterioRestricaoAsync();

        //    CriterioRestricaoModel criterioRestricaoModelSearched = await _core.SearchAsync(criterioRestricaoModel.Id);

        //    Assert.AreEqual(criterioRestricaoModel.Id, criterioRestricaoModelSearched.Id);
        //    Assert.AreEqual(criterioRestricaoModel.Codigo, criterioRestricaoModelSearched.Codigo);
        //    Assert.AreEqual(criterioRestricaoModel.Descricao, criterioRestricaoModelSearched.Descricao);
        //    Assert.AreEqual(criterioRestricaoModel.PrazoGuardaFaseCorrente, criterioRestricaoModelSearched.PrazoGuardaFaseCorrente);
        //    Assert.AreEqual(criterioRestricaoModel.UnidadePrazoGuardaFaseCorrente, criterioRestricaoModelSearched.UnidadePrazoGuardaFaseCorrente);
        //    Assert.AreEqual(criterioRestricaoModel.EventoFimFaseCorrente, criterioRestricaoModelSearched.EventoFimFaseCorrente);
        //    Assert.AreEqual(criterioRestricaoModel.PrazoGuardaFaseIntermediaria, criterioRestricaoModelSearched.PrazoGuardaFaseIntermediaria);
        //    Assert.AreEqual(criterioRestricaoModel.UnidadePrazoGuardaFaseIntermediaria, criterioRestricaoModelSearched.UnidadePrazoGuardaFaseIntermediaria);
        //    Assert.AreEqual(criterioRestricaoModel.EventoFimFaseIntermediaria, criterioRestricaoModelSearched.EventoFimFaseIntermediaria);
        //    Assert.AreEqual(criterioRestricaoModel.DestinacaoFinal, criterioRestricaoModelSearched.DestinacaoFinal);
        //    Assert.AreEqual(criterioRestricaoModel.Observacao, criterioRestricaoModelSearched.Observacao);
        //    Assert.AreEqual(criterioRestricaoModel.Documento.Id, criterioRestricaoModelSearched.Documento.Id);
        //}

        //[TestMethod]
        //public async Task CriterioRestricaoTestUpdate()
        //{
        //    CriterioRestricaoModel criterioRestricaoModel = await InsertCriterioRestricaoAsync();

        //    criterioRestricaoModel.Codigo = "02";
        //    criterioRestricaoModel.Descricao = "Descrição updated";
        //    criterioRestricaoModel.PrazoGuardaFaseCorrente = 1;
        //    criterioRestricaoModel.UnidadePrazoGuardaFaseCorrente = UnidadeTempo.Meses;
        //    //criterioRestricaoModel.EventoFimFaseCorrente = "Evento fim fase corrente update";
        //    criterioRestricaoModel.PrazoGuardaFaseIntermediaria = 2;
        //    criterioRestricaoModel.UnidadePrazoGuardaFaseIntermediaria = UnidadeTempo.Dias;
        //    //criterioRestricaoModel.EventoFimFaseIntermediaria = "Evento fim fase intermediária update";
        //    criterioRestricaoModel.DestinacaoFinal = DestinacaoFinal.GuardaPermanente;
        //    criterioRestricaoModel.Observacao = "Observação update";

        //    //TODO: Testar a mudança e documento

        //    await _core.UpdateAsync(criterioRestricaoModel);

        //    CriterioRestricaoModel criterioRestricaoModelSearched = await _core.SearchAsync(criterioRestricaoModel.Id);

        //    Assert.AreEqual(criterioRestricaoModel.Id, criterioRestricaoModelSearched.Id);
        //    Assert.AreEqual(criterioRestricaoModel.Codigo, criterioRestricaoModelSearched.Codigo);
        //    Assert.AreEqual(criterioRestricaoModel.Descricao, criterioRestricaoModelSearched.Descricao);
        //    Assert.AreEqual(criterioRestricaoModel.PrazoGuardaFaseCorrente, criterioRestricaoModelSearched.PrazoGuardaFaseCorrente);
        //    Assert.AreEqual(criterioRestricaoModel.UnidadePrazoGuardaFaseCorrente, criterioRestricaoModelSearched.UnidadePrazoGuardaFaseCorrente);
        //    Assert.AreEqual(criterioRestricaoModel.EventoFimFaseCorrente, criterioRestricaoModelSearched.EventoFimFaseCorrente);
        //    Assert.AreEqual(criterioRestricaoModel.PrazoGuardaFaseIntermediaria, criterioRestricaoModelSearched.PrazoGuardaFaseIntermediaria);
        //    Assert.AreEqual(criterioRestricaoModel.UnidadePrazoGuardaFaseIntermediaria, criterioRestricaoModelSearched.UnidadePrazoGuardaFaseIntermediaria);
        //    Assert.AreEqual(criterioRestricaoModel.EventoFimFaseIntermediaria, criterioRestricaoModelSearched.EventoFimFaseIntermediaria);
        //    Assert.AreEqual(criterioRestricaoModel.DestinacaoFinal, criterioRestricaoModelSearched.DestinacaoFinal);
        //    Assert.AreEqual(criterioRestricaoModel.Observacao, criterioRestricaoModelSearched.Observacao);
        //    Assert.AreEqual(criterioRestricaoModel.Documento.Id, criterioRestricaoModelSearched.Documento.Id);
        //}

        //[TestMethod]
        //public async Task CriterioRestricaoTestDelete()
        //{
        //    CriterioRestricaoModel criterioRestricaoModel = await InsertCriterioRestricaoAsync();

        //    await _core.DeleteAsync(criterioRestricaoModel.Id);

        //    bool ok = false;

        //    try
        //    {
        //        await _core.SearchAsync(criterioRestricaoModel.Id);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "CriterioRestricao não encontrado.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter encontrado o Siglo excluído.");
        //}
    }
}
