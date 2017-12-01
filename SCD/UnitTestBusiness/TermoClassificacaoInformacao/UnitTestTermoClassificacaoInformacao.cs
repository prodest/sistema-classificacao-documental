using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
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
            int prazoSigilo = 1;
            UnidadeTempoModel unidadePrazoSigilo = UnidadeTempoModel.Anos;

            DocumentoModel documento = await InsertDocumentoAsync();
            CriterioRestricaoModel criterioRestricao = await InsertCriterioRestricaoAsync(documento);

            TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel = new TermoClassificacaoInformacaoModel
            {
                Codigo = codigo,
                //GrauSigilo = grauSigilo,
                TipoSigilo = tipoSigilo,
                ConteudoSigilo = conteudoSigilo,
                IdentificadorDocumento = identificadorDocumento,
                DataProducaoDocumento = dataProducaoDocumento,
                FundamentoLegal = fundamentoLegal,
                Justificativa = justificativa,
                CpfIndicacaoAprovador = cpfIndicacaoAprovador,
                PrazoSigilo = prazoSigilo,
                UnidadePrazoSigilo = unidadePrazoSigilo,

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
            Assert.AreEqual(termoClassificacaoInformacaoModel.PrazoSigilo, prazoSigilo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.UnidadePrazoSigilo, unidadePrazoSigilo);

            Assert.IsNotNull(termoClassificacaoInformacaoModel.CpfUsuario);
            Assert.IsFalse(termoClassificacaoInformacaoModel.GuidOrganizacao.Equals(Guid.Empty));
            Assert.IsTrue(termoClassificacaoInformacaoModel.DataClassificacao >= dataTeste);

            Assert.IsFalse(termoClassificacaoInformacaoModel.Documento == null);
            Assert.AreEqual(termoClassificacaoInformacaoModel.Documento.Id, documento.Id);

            Assert.IsFalse(termoClassificacaoInformacaoModel.CriterioRestricao == null);
            Assert.AreEqual(termoClassificacaoInformacaoModel.CriterioRestricao.Id, criterioRestricao.Id);
        }

        [TestMethod]
        public async Task TermoClassificacaoInformacaoTestSearch()
        {
            TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel = await InsertTermoClassificacaoInformacaoAsync();

            TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModelSearched = await _core.SearchAsync(termoClassificacaoInformacaoModel.Id);

            Assert.IsTrue(termoClassificacaoInformacaoModel.Id > 0);
            Assert.AreEqual(termoClassificacaoInformacaoModel.Codigo, termoClassificacaoInformacaoModelSearched.Codigo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.GrauSigilo, termoClassificacaoInformacaoModelSearched.GrauSigilo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.TipoSigilo, termoClassificacaoInformacaoModelSearched.TipoSigilo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.ConteudoSigilo, termoClassificacaoInformacaoModelSearched.ConteudoSigilo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.IdentificadorDocumento, termoClassificacaoInformacaoModelSearched.IdentificadorDocumento);
            Assert.AreEqual(termoClassificacaoInformacaoModel.DataProducaoDocumento, termoClassificacaoInformacaoModelSearched.DataProducaoDocumento);
            Assert.AreEqual(termoClassificacaoInformacaoModel.FundamentoLegal, termoClassificacaoInformacaoModelSearched.FundamentoLegal);
            Assert.AreEqual(termoClassificacaoInformacaoModel.Justificativa, termoClassificacaoInformacaoModelSearched.Justificativa);
            Assert.AreEqual(termoClassificacaoInformacaoModel.CpfIndicacaoAprovador, termoClassificacaoInformacaoModelSearched.CpfIndicacaoAprovador);
            Assert.AreEqual(termoClassificacaoInformacaoModel.PrazoSigilo, termoClassificacaoInformacaoModelSearched.PrazoSigilo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.UnidadePrazoSigilo, termoClassificacaoInformacaoModelSearched.UnidadePrazoSigilo);

            Assert.IsNotNull(termoClassificacaoInformacaoModel.CpfUsuario);
            Assert.IsFalse(termoClassificacaoInformacaoModel.GuidOrganizacao.Equals(Guid.Empty));
            Assert.AreEqual(termoClassificacaoInformacaoModel.DataClassificacao, termoClassificacaoInformacaoModelSearched.DataClassificacao);

            Assert.IsFalse(termoClassificacaoInformacaoModel.Documento == null);
            Assert.AreEqual(termoClassificacaoInformacaoModel.Documento.Id, termoClassificacaoInformacaoModelSearched.Documento.Id);

            Assert.IsFalse(termoClassificacaoInformacaoModel.CriterioRestricao == null);
            Assert.AreEqual(termoClassificacaoInformacaoModel.CriterioRestricao.Id, termoClassificacaoInformacaoModelSearched.CriterioRestricao.Id);
        }

        [TestMethod]
        public async Task TermoClassificacaoInformacaoTestSearchByUser()
        {
            for (int i = 0; i < 10; i++)
            {
                await InsertTermoClassificacaoInformacaoAsync();
            }
 
            ICollection<TermoClassificacaoInformacaoModel> termosClassificacaoInformacaoModelSearched = await _core.SearchByUserAsync();

            Assert.IsNotNull(termosClassificacaoInformacaoModelSearched);
            Assert.IsTrue(termosClassificacaoInformacaoModelSearched.Count >= 10);
        }

        [TestMethod]
        public async Task TermoClassificacaoInformacaoTestUpdate()
        {
            DateTime dataTeste = DateTime.Now;

            TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel = await InsertTermoClassificacaoInformacaoAsync();

            termoClassificacaoInformacaoModel.Codigo = "02";
            //termoClassificacaoInformacaoModel.GrauSigilo = GrauSigiloModel.Secreto;
            termoClassificacaoInformacaoModel.TipoSigilo = TermoClassificacaoInformacaoModel.TipoSigiloModel.Total;
            termoClassificacaoInformacaoModel.ConteudoSigilo = "Conteúdo Sigilo update";
            termoClassificacaoInformacaoModel.IdentificadorDocumento = "Identificador Documento update";
            termoClassificacaoInformacaoModel.DataProducaoDocumento = dataTeste;
            termoClassificacaoInformacaoModel.FundamentoLegal = "Identificador Documento update";
            termoClassificacaoInformacaoModel.Justificativa = "Justificativa update";
            termoClassificacaoInformacaoModel.CpfIndicacaoAprovador = "33333333333";
            termoClassificacaoInformacaoModel.PrazoSigilo = 30;
            termoClassificacaoInformacaoModel.UnidadePrazoSigilo = UnidadeTempoModel.Meses;

            await _core.UpdateAsync(termoClassificacaoInformacaoModel);

            TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModelSearched = await _core.SearchAsync(termoClassificacaoInformacaoModel.Id);

            Assert.IsTrue(termoClassificacaoInformacaoModel.Id > 0);
            Assert.AreEqual(termoClassificacaoInformacaoModel.Codigo, termoClassificacaoInformacaoModelSearched.Codigo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.GrauSigilo, termoClassificacaoInformacaoModelSearched.GrauSigilo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.TipoSigilo, termoClassificacaoInformacaoModelSearched.TipoSigilo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.ConteudoSigilo, termoClassificacaoInformacaoModelSearched.ConteudoSigilo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.IdentificadorDocumento, termoClassificacaoInformacaoModelSearched.IdentificadorDocumento);
            Assert.AreEqual(termoClassificacaoInformacaoModel.DataProducaoDocumento, termoClassificacaoInformacaoModelSearched.DataProducaoDocumento);
            Assert.AreEqual(termoClassificacaoInformacaoModel.FundamentoLegal, termoClassificacaoInformacaoModelSearched.FundamentoLegal);
            Assert.AreEqual(termoClassificacaoInformacaoModel.Justificativa, termoClassificacaoInformacaoModelSearched.Justificativa);
            Assert.AreEqual(termoClassificacaoInformacaoModel.CpfIndicacaoAprovador, termoClassificacaoInformacaoModelSearched.CpfIndicacaoAprovador);
            Assert.AreEqual(termoClassificacaoInformacaoModel.PrazoSigilo, termoClassificacaoInformacaoModelSearched.PrazoSigilo);
            Assert.AreEqual(termoClassificacaoInformacaoModel.UnidadePrazoSigilo, termoClassificacaoInformacaoModelSearched.UnidadePrazoSigilo);

            Assert.IsNotNull(termoClassificacaoInformacaoModel.CpfUsuario);
            Assert.IsFalse(termoClassificacaoInformacaoModel.GuidOrganizacao.Equals(Guid.Empty));
            Assert.AreEqual(termoClassificacaoInformacaoModel.DataClassificacao, termoClassificacaoInformacaoModelSearched.DataClassificacao);

            Assert.IsFalse(termoClassificacaoInformacaoModel.Documento == null);
            Assert.AreEqual(termoClassificacaoInformacaoModel.Documento.Id, termoClassificacaoInformacaoModelSearched.Documento.Id);

            Assert.IsFalse(termoClassificacaoInformacaoModel.CriterioRestricao == null);
            Assert.AreEqual(termoClassificacaoInformacaoModel.CriterioRestricao.Id, termoClassificacaoInformacaoModelSearched.CriterioRestricao.Id);
        }

        [TestMethod]
        public async Task TermoClassificacaoInformacaoTestDelete()
        {
            TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel = await InsertTermoClassificacaoInformacaoAsync();

            await _core.DeleteAsync(termoClassificacaoInformacaoModel.Id);

            bool ok = false;

            try
            {
                await _core.SearchAsync(termoClassificacaoInformacaoModel.Id);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Termo de Classificação da Informação não encontrado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter encontrado o Termo de Classificação da Informação excluído.");
        }
    }
}
