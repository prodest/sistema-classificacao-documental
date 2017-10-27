namespace Prodest.Scd.Business.Model
{
    public class SigiloModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int? PrazoTermino { get; set; }
        public string EventoFim { get; set; }
        public string Justificativa { get; set; }
        public string FundamentoLegal { get; set; }
        public bool GerarTermo { get; set; }
        public GrauSigilo Grau { get; set; }
        public UnidadePrazoTerminoSigilo? UnidadePrazoTermino { get; set; }

        public DocumentoModel Documento { get; set; }

        public enum GrauSigilo
        {
            Ostensivo = 1,
            InformacaoPessoal = 2,
            Reservado = 3,
            Secreto = 4,
            UltraSecreto = 5,
        }

        public enum UnidadePrazoTerminoSigilo
        {
            Dias = 1,
            Semanas = 2,
            Meses = 3,
            Anos = 4
        }
    }
}