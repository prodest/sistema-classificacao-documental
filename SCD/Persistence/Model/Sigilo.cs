using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Persistence.Model
{
    public partial class Sigilo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdDocumento { get; set; }
        public int? PrazoTermino { get; set; }
        public string EventoFim { get; set; }
        public string Justificativa { get; set; }
        public string FundamentoLegal { get; set; }
        public bool GerarTermo { get; set; }
        public int IdGrau { get; set; }
        public int? IdUnidadePrazoTermino { get; set; }

        public Documento Documento { get; set; }
    }
}