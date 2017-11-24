using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Business.Model
{
    public class TermoClassificacaoInformacaoModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public Guid GuidUnidade { get; set; }
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

        public DocumentoModel Documento { get; set; }
        public ItemPlanoClassificacaoModel ItemPlanoClassificacao { get; set; }
    }
}
