using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Persistence.Model
{
    public partial class Sigilo
    {
        public Sigilo()
        {
            Documentos = new HashSet<Documento>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public ICollection<Documento> Documentos { get; set; }
    }
}