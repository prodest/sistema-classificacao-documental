using System;
using System.Collections.Generic;

namespace Prodest.Scd.Persistence.Model
{
    public partial class Organizacao
    {
        public Organizacao()
        {
            FundamentosLegais = new HashSet<FundamentoLegal>();
            NiveisClassificacao = new HashSet<NivelClassificacao>();
            PlanosClassificacao = new HashSet<PlanoClassificacao>();
            TiposDocumentais = new HashSet<TipoDocumental>();
        }

        public int Id { get; set; }
        public Guid GuidOrganizacao { get; set; }

        public ICollection<FundamentoLegal> FundamentosLegais { get; set; }
        public ICollection<NivelClassificacao> NiveisClassificacao { get; set; }
        public ICollection<PlanoClassificacao> PlanosClassificacao { get; set; }
        public ICollection<TipoDocumental> TiposDocumentais { get; set; }
    }
}
