using System.Collections.Generic;

namespace Prodest.Scd.Business.Model
{
    public class ItemPlanoClassificacaoModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public NivelClassificacaoModel NivelClassificacao { get; set; }
        public PlanoClassificacaoModel PlanoClassificacao { get; set; }
        public ICollection<ItemPlanoClassificacaoModel> ItensPlanoClassificacaoChildren { get; set; }
        public ItemPlanoClassificacaoModel ItemPlanoClassificacaoParent { get; set; }
    }
}