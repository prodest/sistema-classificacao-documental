using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Persistence.Model
{
    public partial class CriterioRestricao
    {
        public CriterioRestricao()
        {
            CriteriosRestricaoDocumento = new HashSet<CriterioRestricaoDocumento>();
            TermosClassificacaoInformacao = new HashSet<TermoClassificacaoInformacao>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int? PrazoTermino { get; set; }
        public string EventoFim { get; set; }
        public string Justificativa { get; set; }
        public string FundamentoLegal { get; set; }
        public bool Classificavel { get; set; }
        public int IdGrau { get; set; }
        public int? IdUnidadePrazoTermino { get; set; }
        public int IdPlanoClassificacao { get; set; }

        public PlanoClassificacao PlanoClassificacao { get; set; }
        public ICollection<CriterioRestricaoDocumento> CriteriosRestricaoDocumento { get; set; }
        public ICollection<TermoClassificacaoInformacao> TermosClassificacaoInformacao { get; set; }
    }
}
