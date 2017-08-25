using System;
using System.Collections.Generic;

namespace Prodest.Scd.Persistence.Model
{
    public partial class PlanoClassificacao
    {
        public PlanoClassificacao()
        {
            ItensPlanoClassificacao = new HashSet<ItemPlanoClassificacao>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public bool AreaFim { get; set; }
        public int IdOrganizacao { get; set; }
        public Guid GuidOrganizacao { get; set; }
        public DateTime? Aprovacao { get; set; }
        public DateTime? Publicacao { get; set; }
        public DateTime? InicioVigencia { get; set; }
        public DateTime? FimVigencia { get; set; }

        public Organizacao Organizacao { get; set; }
        public ICollection<ItemPlanoClassificacao> ItensPlanoClassificacao { get; set; }
    }
}
