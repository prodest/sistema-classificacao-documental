using System;

namespace Prodest.Scd.Persistence.Model
{
    public partial class TermoClassificacaoInformacao
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public Guid GuidOrganizacao { get; set; }
        public int IdGrauSigilo { get; set; }
        public int IdTipoSigilo { get; set; }
        public string ConteudoSigilo { get; set; }
        public int IdItemPlanoClassificacao { get; set; }
        public int IdDocumento { get; set; }
        public string IdentificadorDocumento { get; set; }
        public DateTime DataProducaoDocumento { get; set; }
        public string FundamentoLegal { get; set; }
        public string Justificativa { get; set; }
        public DateTime DataClassificacao { get; set; }
        public string CpfUsuario { get; set; }
        public string CpfIndicacaoAprovador { get; set; }

        public Documento Documento { get; set; }
        public ItemPlanoClassificacao ItemPlanoClassificacao { get; set; }
    }
}
