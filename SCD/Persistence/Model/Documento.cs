using System.Collections.Generic;

namespace Prodest.Scd.Persistence.Model
{
    public partial class Documento
    {
        public Documento()
        {
            CriterioRestricaoDocumento = new HashSet<CriterioRestricaoDocumento>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdItemPlanoClassificacao { get; set; }
        public int IdTipoDocumental { get; set; }

        public ItemPlanoClassificacao ItemPlanoClassificacao { get; set; }
        public TipoDocumental TipoDocumental { get; set; }
        public ICollection<CriterioRestricaoDocumento> CriterioRestricaoDocumento { get; set; }
    }
}