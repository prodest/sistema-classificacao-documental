using System.Collections.Generic;

namespace Prodest.Scd.Business.Model
{
    public class DocumentoModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public ItemPlanoClassificacaoModel ItensPlanoClassificacao { get; set; }
        public TipoDocumentalModel TipoDocumental { get; set; }
        public ICollection<SigiloModel> Sigilo { get; set; }
    }
}