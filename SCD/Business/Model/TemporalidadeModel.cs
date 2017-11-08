using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Business.Model
{
    public class TemporalidadeModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int? PrazoGuardaFaseCorrente { get; set; }
        public UnidadeTempo? UnidadePrazoGuardaFaseCorrente { get; set; }
        public string EventoFimFaseCorrente { get; set; }
        public int? PrazoGuardaFaseIntermediaria { get; set; }
        public UnidadeTempo? UnidadePrazoGuardaFaseIntermediaria { get; set; }
        public string EventoFimFaseIntermediaria { get; set; }
        public DestinacaoFinal DestinacaoFinal { get; set; }
        public string Observacao { get; set; }

        public DocumentoModel Documento { get; set; }
    }

    public enum DestinacaoFinal
    {
        GuardaPermanente = 1,
        Eliminacao = 2
    }
}
