using System.Collections.Generic;

namespace Prodest.Scd.Business.Model
{
    public class DocumentoModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public ItemPlanoClassificacaoModel ItemPlanoClassificacao { get; set; }
        public TipoDocumentalModel TipoDocumental { get; set; }
        public ICollection<CriterioRestricaoModel> CriteriosRestricao { get; set; }
        public ICollection<TemporalidadeModel> Temporalidades { get; set; }
    }
}