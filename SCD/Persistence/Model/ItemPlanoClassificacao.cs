using System.Collections.Generic;

namespace Prodest.Scd.Persistence.Model
{
    public partial class ItemPlanoClassificacao
    {
        public ItemPlanoClassificacao()
        {
            InverseIdItemPlanoClassificacaoPaiNavigation = new HashSet<ItemPlanoClassificacao>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdPlanoClassificacao { get; set; }
        public int IdNivelClassificacao { get; set; }
        public int? IdItemPlanoClassificacaoPai { get; set; }

        public ItemPlanoClassificacao IdItemPlanoClassificacaoPaiNavigation { get; set; }
        public NivelClassificacao IdNivelClassificacaoNavigation { get; set; }
        public PlanoClassificacao IdPlanoClassificacaoNavigation { get; set; }
        public ICollection<ItemPlanoClassificacao> InverseIdItemPlanoClassificacaoPaiNavigation { get; set; }
    }
}
