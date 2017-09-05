using System;
using System.Collections.Generic;

namespace Prodest.Scd.Persistence.Model
{
    public partial class Organizacao
    {
        public Organizacao()
        {
            NiveisClassificacao = new HashSet<NivelClassificacao>();
            PlanosClassificacao = new HashSet<PlanoClassificacao>();
        }

        public int Id { get; set; }
        public Guid GuidOrganizacao { get; set; }

        public ICollection<NivelClassificacao> NiveisClassificacao { get; set; }
        public ICollection<PlanoClassificacao> PlanosClassificacao { get; set; }
    }
}
