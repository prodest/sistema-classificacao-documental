using System.Collections.Generic;

namespace Prodest.Scd.Business.Model
{
    public class CriterioRestricaoModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Justificativa { get; set; }
        public bool Classificavel { get; set; }
        public int? PrazoTermino { get; set; }
        public string EventoFim { get; set; }
        public UnidadeTempoModel? UnidadePrazoTermino { get; set; }

        public FundamentoLegalModel FundamentoLegal { get; set; }
        public PlanoClassificacaoModel PlanoClassificacao { get; set; }
        public ICollection<DocumentoModel> Documentos { get; set; }
        public ICollection<TermoClassificacaoInformacaoModel> TermosClassificacaoInformacao { get; set; }
    }
}
