using System.Collections.Generic;

namespace Prodest.Scd.Business.Model
{
    public class CriterioRestricaoModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int? PrazoTermino { get; set; }
        public string EventoFim { get; set; }
        public string Justificativa { get; set; }
        public string FundamentoLegal { get; set; }
        public bool Classificavel { get; set; }
        public GrauSigilo? Grau { get; set; }
        public UnidadeTempo? UnidadePrazoTermino { get; set; }

        public PlanoClassificacaoModel PlanoClassificacao { get; set; }
        public ICollection<DocumentoModel> Documentos { get; set; }

        public enum GrauSigilo
        {
            Reservado = 1,
            Secreto = 2,
            Ultrassecreto = 3,
        }
    }
}
