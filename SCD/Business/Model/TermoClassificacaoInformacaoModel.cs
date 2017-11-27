using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Business.Model
{
    public class TermoClassificacaoInformacaoModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public Guid GuidOrganizacao { get; set; }
        public GrauSigiloModel GrauSigilo { get; set; }
        public TipoSigiloModel TipoSigilo { get; set; }
        public string ConteudoSigilo { get; set; }
        public string IdentificadorDocumento { get; set; }
        public DateTime DataProducaoDocumento { get; set; }
        public string FundamentoLegal { get; set; }
        public string Justificativa { get; set; }
        public DateTime DataClassificacao { get; set; }
        public string CpfUsuario { get; set; }
        public string CpfIndicacaoAprovador { get; set; }

        public DocumentoModel Documento { get; set; }
        public CriterioRestricaoModel CriterioRestricao { get; set; }
        public ItemPlanoClassificacaoModel ItemPlanoClassificacao { get; set; }

        public enum TipoSigiloModel
        {
            Parcial = 1,
            Total = 2
        }
    }
}
