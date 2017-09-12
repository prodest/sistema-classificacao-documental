namespace Prodest.Scd.Business.Model
{
    public class TipoDocumentalModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public OrganizacaoModel Organizacao { get; set; }
    }
}