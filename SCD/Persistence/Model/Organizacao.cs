using System;
using System.Collections.Generic;

namespace Prodest.Scd.Persistence.Model
{
    public partial class Organizacao
    {
        public Organizacao()
        {
            PlanosClassificacao = new HashSet<PlanoClassificacao>();
        }

        public int Id { get; set; }
        public Guid GuidOrganizacao { get; set; }

        public ICollection<PlanoClassificacao> PlanosClassificacao { get; set; }
    }
}
