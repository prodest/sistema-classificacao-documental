using System.Collections.Generic;

namespace Prodest.Scd.Business.Model
{
    public class NivelClassificacaoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public ICollection<ItemPlanoClassificacaoModel> ItensPlanoClassificacao { get; set; }
    }
}
