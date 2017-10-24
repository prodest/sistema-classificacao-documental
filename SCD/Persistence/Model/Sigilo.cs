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

        public Documento IdDocumentoNavigation { get; set; }
    }
}