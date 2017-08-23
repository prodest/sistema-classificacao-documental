using System.Collections.Generic;

namespace Prodest.Scd.Persistence.Model
{
    public partial class NivelClassificacao
    {
        public NivelClassificacao()
        {
            ItemPlanoClassificacao = new HashSet<ItemPlanoClassificacao>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public ICollection<ItemPlanoClassificacao> ItemPlanoClassificacao { get; set; }
    }
}
