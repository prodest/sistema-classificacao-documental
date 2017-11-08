using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Persistence.Model
{
    public partial class Temporalidade
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int? PrazoGuardaFaseCorrente { get; set; }
        public int? IdUnidadePrazoGuardaFaseCorrente { get; set; }
        public string EventoFimFaseCorrente { get; set; }
        public int? PrazoGuardaFaseIntermediaria { get; set; }
        public int? IdUnidadePrazoGuardaFaseIntermediaria { get; set; }
        public string EventoFimFaseIntermediaria { get; set; }
        public int IdDestinacaoFinal { get; set; }
        public string Observacao { get; set; }
        public int IdDocumento { get; set; }

        public Documento Documento { get; set; }
    }
}
