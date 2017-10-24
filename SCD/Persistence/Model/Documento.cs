using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Persistence.Model
{
    public partial class Documento
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdItemPlanoClassificacao { get; set; }
        public int IdTipoDocumental { get; set; }
        public int? IdSigilo { get; set; }
        public int? IdTemporalidade { get; set; }

        public ItemPlanoClassificacao ItensPlanoClassificacao { get; set; }
        public Sigilo Sigilo { get; set; }
        public TipoDocumental TipoDocumental { get; set; }
    }
}
