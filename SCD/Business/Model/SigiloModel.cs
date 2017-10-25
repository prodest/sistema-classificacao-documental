namespace Prodest.Scd.Business.Model
{
    public class SigiloModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public DocumentoModel Documento { get; set; }
    }
}