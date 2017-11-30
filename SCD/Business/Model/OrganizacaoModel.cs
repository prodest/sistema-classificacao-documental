using System;
using System.Collections.Generic;

namespace Prodest.Scd.Business.Model
{
    public class OrganizacaoModel
    {
        public int Id { get; set; }
        public Guid GuidOrganizacao { get; set; }

        public ICollection<FundamentoLegalModel> FundamentosLegais { get; set; }
        public ICollection<NivelClassificacaoModel> NiveisClassificacao { get; set; }
        public ICollection<PlanoClassificacaoModel> PlanosClassificacao { get; set; }
        public ICollection<TipoDocumentalModel> TiposDocumentais { get; set; }
    }
}