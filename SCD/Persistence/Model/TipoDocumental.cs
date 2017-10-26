using System.Collections.Generic;

namespace Prodest.Scd.Persistence.Model
{
    public partial class TipoDocumental
    {
        public TipoDocumental()
        {
            Documentos = new HashSet<Documento>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public int IdOrganizacao { get; set; }

        public Organizacao Organizacao { get; set; }
        public ICollection<Documento> Documentos { get; set; }
    }
}