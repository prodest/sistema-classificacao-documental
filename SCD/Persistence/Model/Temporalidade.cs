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
        public int? FaseCorrentePrazoGuarda { get; set; }
        public int? IdFaseCorrentePrazoGuardaUnidade { get; set; }
        public string FaseCorrenteEventoFim { get; set; }
        public int? FaseIntermediariaPrazoGuarda { get; set; }
        public int? IdFaseIntermediariaPrazoGuardaUnidade { get; set; }
        public string FaseIntermediariaEventoFim { get; set; }
        public int IdDestinacaoFinal { get; set; }
        public string Observacao { get; set; }
        public int IdDocumento { get; set; }

        public Documento IdDocumentoNavigation { get; set; }
    }
}
