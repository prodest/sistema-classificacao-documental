using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.Temporalidade
{
    [TestClass]
    public class UnitTestTemporalidade : UnitTestTemporalidadeCommon
    {
        [TestMethod]
        public async Task TemporalidadeTestInsert()
        {
            string codigo = "01";
            string descricao = "TemporalidadeTestInsert";
            int prazoGuardaFaseCorrente = 5;
            UnidadeTempo unidadePrazoGuardaFaseCorrente = UnidadeTempo.Anos;
            //string eventoFimFaseCorrente = "Evento fim fase corrente";
            int prazoGuardaFaseIntermediaria = 10;
            UnidadeTempo unidadePrazoGuardaFaseIntermediaria = UnidadeTempo.Anos;
            //string eventoFimFaseIntermediaria = "Evento fim fase intermediária";
            DestinacaoFinal destinacaoFinal = DestinacaoFinal.Eliminacao;
            string observacao = "Observação";

            DocumentoModel documentoModel = await InsertDocumentoAsync();

            TemporalidadeModel temporalidadeModel = new TemporalidadeModel
            {
                Codigo = codigo,
                Descricao = descricao,
                PrazoGuardaFaseCorrente = prazoGuardaFaseCorrente,
                UnidadePrazoGuardaFaseCorrente = unidadePrazoGuardaFaseCorrente,
                //EventoFimFaseCorrente = eventoFimFaseCorrente,
                PrazoGuardaFaseIntermediaria = prazoGuardaFaseIntermediaria,
                UnidadePrazoGuardaFaseIntermediaria = unidadePrazoGuardaFaseIntermediaria,
                //EventoFimFaseIntermediaria = eventoFimFaseIntermediaria,
                DestinacaoFinal = destinacaoFinal,
                Observacao = observacao,
                Documento = documentoModel
            };

            temporalidadeModel = await _core.InsertAsync(temporalidadeModel);
            _idsTemporalidadesTestados.Add(temporalidadeModel.Id);

            Assert.IsTrue(temporalidadeModel.Id > 0);
            Assert.AreEqual(temporalidadeModel.Codigo, codigo);
            Assert.AreEqual(temporalidadeModel.Descricao, descricao);
            Assert.AreEqual(temporalidadeModel.PrazoGuardaFaseCorrente, prazoGuardaFaseCorrente);
            Assert.AreEqual(temporalidadeModel.UnidadePrazoGuardaFaseCorrente, unidadePrazoGuardaFaseCorrente);
            Assert.IsNull(temporalidadeModel.EventoFimFaseCorrente);
            Assert.AreEqual(temporalidadeModel.PrazoGuardaFaseIntermediaria, prazoGuardaFaseIntermediaria);
            Assert.AreEqual(temporalidadeModel.UnidadePrazoGuardaFaseIntermediaria, unidadePrazoGuardaFaseIntermediaria);
            Assert.IsNull(temporalidadeModel.EventoFimFaseIntermediaria);
            Assert.AreEqual(temporalidadeModel.DestinacaoFinal, destinacaoFinal);
            Assert.AreEqual(temporalidadeModel.Observacao, observacao);

            Assert.IsFalse(temporalidadeModel.Documento == null);
            Assert.IsTrue(temporalidadeModel.Documento.Id == documentoModel.Id);
        }

        [TestMethod]
        public async Task TemporalidadeTestSearch()
        {
            TemporalidadeModel temporalidadeModel = await InsertTemporalidadeAsync();

            TemporalidadeModel temporalidadeModelSearched = await _core.SearchAsync(temporalidadeModel.Id);

            Assert.AreEqual(temporalidadeModel.Id, temporalidadeModelSearched.Id);
            Assert.AreEqual(temporalidadeModel.Codigo, temporalidadeModelSearched.Codigo);
            Assert.AreEqual(temporalidadeModel.Descricao, temporalidadeModelSearched.Descricao);
            Assert.AreEqual(temporalidadeModel.PrazoGuardaFaseCorrente, temporalidadeModelSearched.PrazoGuardaFaseCorrente);
            Assert.AreEqual(temporalidadeModel.UnidadePrazoGuardaFaseCorrente, temporalidadeModelSearched.UnidadePrazoGuardaFaseCorrente);
            Assert.AreEqual(temporalidadeModel.EventoFimFaseCorrente, temporalidadeModelSearched.EventoFimFaseCorrente);
            Assert.AreEqual(temporalidadeModel.PrazoGuardaFaseIntermediaria, temporalidadeModelSearched.PrazoGuardaFaseIntermediaria);
            Assert.AreEqual(temporalidadeModel.UnidadePrazoGuardaFaseIntermediaria, temporalidadeModelSearched.UnidadePrazoGuardaFaseIntermediaria);
            Assert.AreEqual(temporalidadeModel.EventoFimFaseIntermediaria, temporalidadeModelSearched.EventoFimFaseIntermediaria);
            Assert.AreEqual(temporalidadeModel.DestinacaoFinal, temporalidadeModelSearched.DestinacaoFinal);
            Assert.AreEqual(temporalidadeModel.Observacao, temporalidadeModelSearched.Observacao);
            Assert.AreEqual(temporalidadeModel.Documento.Id, temporalidadeModelSearched.Documento.Id);
        }

        [TestMethod]
        public async Task TemporalidadeTestUpdate()
        {
            TemporalidadeModel temporalidadeModel = await InsertTemporalidadeAsync();

            temporalidadeModel.Codigo = "02";
            temporalidadeModel.Descricao = "Descrição updated";
            temporalidadeModel.PrazoGuardaFaseCorrente = 1;
            temporalidadeModel.UnidadePrazoGuardaFaseCorrente = UnidadeTempo.Meses;
            //temporalidadeModel.EventoFimFaseCorrente = "Evento fim fase corrente update";
            temporalidadeModel.PrazoGuardaFaseIntermediaria = 2;
            temporalidadeModel.UnidadePrazoGuardaFaseIntermediaria = UnidadeTempo.Dias;
            //temporalidadeModel.EventoFimFaseIntermediaria = "Evento fim fase intermediária update";
            temporalidadeModel.DestinacaoFinal = DestinacaoFinal.GuardaPermanente;
            temporalidadeModel.Observacao = "Observação update";

            //TODO: Testar a mudança e documento

            await _core.UpdateAsync(temporalidadeModel);

            TemporalidadeModel temporalidadeModelSearched = await _core.SearchAsync(temporalidadeModel.Id);

            Assert.AreEqual(temporalidadeModel.Id, temporalidadeModelSearched.Id);
            Assert.AreEqual(temporalidadeModel.Codigo, temporalidadeModelSearched.Codigo);
            Assert.AreEqual(temporalidadeModel.Descricao, temporalidadeModelSearched.Descricao);
            Assert.AreEqual(temporalidadeModel.PrazoGuardaFaseCorrente, temporalidadeModelSearched.PrazoGuardaFaseCorrente);
            Assert.AreEqual(temporalidadeModel.UnidadePrazoGuardaFaseCorrente, temporalidadeModelSearched.UnidadePrazoGuardaFaseCorrente);
            Assert.AreEqual(temporalidadeModel.EventoFimFaseCorrente, temporalidadeModelSearched.EventoFimFaseCorrente);
            Assert.AreEqual(temporalidadeModel.PrazoGuardaFaseIntermediaria, temporalidadeModelSearched.PrazoGuardaFaseIntermediaria);
            Assert.AreEqual(temporalidadeModel.UnidadePrazoGuardaFaseIntermediaria, temporalidadeModelSearched.UnidadePrazoGuardaFaseIntermediaria);
            Assert.AreEqual(temporalidadeModel.EventoFimFaseIntermediaria, temporalidadeModelSearched.EventoFimFaseIntermediaria);
            Assert.AreEqual(temporalidadeModel.DestinacaoFinal, temporalidadeModelSearched.DestinacaoFinal);
            Assert.AreEqual(temporalidadeModel.Observacao, temporalidadeModelSearched.Observacao);
            Assert.AreEqual(temporalidadeModel.Documento.Id, temporalidadeModelSearched.Documento.Id);
        }

        [TestMethod]
        public async Task TemporalidadeTestDelete()
        {
            TemporalidadeModel temporalidadeModel = await InsertTemporalidadeAsync();

            await _core.DeleteAsync(temporalidadeModel.Id);

            bool ok = false;

            try
            {
                await _core.SearchAsync(temporalidadeModel.Id);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Temporalidade não encontrado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter encontrado o Siglo excluído.");
        }
    }
}
