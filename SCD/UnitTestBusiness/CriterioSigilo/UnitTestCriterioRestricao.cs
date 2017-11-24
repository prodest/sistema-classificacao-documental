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
            GrauSigiloModel grauSigilo = GrauSigiloModel.Reservado;
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

        [TestMethod]
        public async Task CriterioRestricaoTestSearch()
        {
            CriterioRestricaoModel criterioRestricaoModel = await InsertCriterioRestricaoAsync();

            CriterioRestricaoModel criterioRestricaoModelSearched = await _core.SearchAsync(criterioRestricaoModel.Id);

            Assert.AreEqual(criterioRestricaoModel.Id, criterioRestricaoModelSearched.Id);
            Assert.AreEqual(criterioRestricaoModel.Codigo, criterioRestricaoModelSearched.Codigo);
            Assert.AreEqual(criterioRestricaoModel.Descricao, criterioRestricaoModelSearched.Descricao);
            Assert.AreEqual(criterioRestricaoModel.Justificativa, criterioRestricaoModelSearched.Justificativa);
            Assert.AreEqual(criterioRestricaoModel.FundamentoLegal, criterioRestricaoModelSearched.FundamentoLegal);
            Assert.AreEqual(criterioRestricaoModel.Classificavel, criterioRestricaoModelSearched.Classificavel);
            Assert.AreEqual(criterioRestricaoModel.Grau, criterioRestricaoModelSearched.Grau);
            Assert.AreEqual(criterioRestricaoModel.PrazoTermino, criterioRestricaoModelSearched.PrazoTermino);
            Assert.AreEqual(criterioRestricaoModel.UnidadePrazoTermino, criterioRestricaoModelSearched.UnidadePrazoTermino);
            Assert.IsNull(criterioRestricaoModel.EventoFim);

            Assert.IsFalse(criterioRestricaoModel.PlanoClassificacao == null);
            Assert.IsTrue(criterioRestricaoModel.PlanoClassificacao.Id == criterioRestricaoModelSearched.PlanoClassificacao.Id);

            Assert.IsFalse(criterioRestricaoModel.Documentos == null);
            Assert.IsTrue(criterioRestricaoModel.Documentos.Count == 1);
            Assert.IsTrue(criterioRestricaoModel.Documentos.Single().Id == criterioRestricaoModelSearched.Documentos.Single().Id);
        }

        [TestMethod]
        public async Task CriterioRestricaoTestUpdate()
        {
            CriterioRestricaoModel criterioRestricaoModel = await InsertCriterioRestricaoAsync();

            criterioRestricaoModel.Codigo = "02";
            criterioRestricaoModel.Descricao = "Descrição updated";
            criterioRestricaoModel.Justificativa = "Jutificativa Teste update";
            criterioRestricaoModel.FundamentoLegal= "Fundamento Legal Teste update";
            criterioRestricaoModel.Classificavel = false;
            criterioRestricaoModel.Grau = GrauSigiloModel.Secreto;
            criterioRestricaoModel.PrazoTermino = 1;
            criterioRestricaoModel.UnidadePrazoTermino = UnidadeTempo.Dias;

            await _core.UpdateAsync(criterioRestricaoModel);

            CriterioRestricaoModel criterioRestricaoModelSearched = await _core.SearchAsync(criterioRestricaoModel.Id);

            Assert.AreEqual(criterioRestricaoModel.Id, criterioRestricaoModelSearched.Id);
            Assert.AreEqual(criterioRestricaoModel.Codigo, criterioRestricaoModelSearched.Codigo);
            Assert.AreEqual(criterioRestricaoModel.Descricao, criterioRestricaoModelSearched.Descricao);
            Assert.AreEqual(criterioRestricaoModel.Justificativa, criterioRestricaoModelSearched.Justificativa);
            Assert.AreEqual(criterioRestricaoModel.FundamentoLegal, criterioRestricaoModelSearched.FundamentoLegal);
            Assert.AreEqual(criterioRestricaoModel.Classificavel, criterioRestricaoModelSearched.Classificavel);
            Assert.AreEqual(criterioRestricaoModel.Grau, criterioRestricaoModelSearched.Grau);
            Assert.AreEqual(criterioRestricaoModel.PrazoTermino, criterioRestricaoModelSearched.PrazoTermino);
            Assert.AreEqual(criterioRestricaoModel.UnidadePrazoTermino, criterioRestricaoModelSearched.UnidadePrazoTermino);
            Assert.IsNull(criterioRestricaoModel.EventoFim);

            Assert.IsFalse(criterioRestricaoModel.PlanoClassificacao == null);
            Assert.IsTrue(criterioRestricaoModel.PlanoClassificacao.Id == criterioRestricaoModelSearched.PlanoClassificacao.Id);

            Assert.IsFalse(criterioRestricaoModel.Documentos == null);
            Assert.IsTrue(criterioRestricaoModel.Documentos.Count == 1);
            Assert.IsTrue(criterioRestricaoModel.Documentos.Single().Id == criterioRestricaoModelSearched.Documentos.Single().Id);
        }

        [TestMethod]
        public async Task CriterioRestricaoTestDelete()
        {
            CriterioRestricaoModel criterioRestricaoModel = await InsertCriterioRestricaoAsync();

            await _core.DeleteAsync(criterioRestricaoModel.Id);

            bool ok = false;

            try
            {
                await _core.SearchAsync(criterioRestricaoModel.Id);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Critério de Restrição não encontrado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter encontrado o Critério de Restrição excluído.");
        }
    }
}
