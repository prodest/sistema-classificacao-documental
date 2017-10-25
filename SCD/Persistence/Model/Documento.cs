using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Persistence.Model
{
    public partial class Documento
    {
        public Documento()
        {
            Sigilo = new HashSet<Sigilo>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdItemPlanoClassificacao { get; set; }
        public int IdTipoDocumental { get; set; }

        public ItemPlanoClassificacao ItemPlanoClassificacao { get; set; }
        public TipoDocumental TipoDocumental { get; set; }
        public ICollection<Sigilo> Sigilo { get; set; }
    }
}