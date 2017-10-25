using System;
using System.Collections.Generic;

namespace Prodest.Scd.Business.Model
{
    public class PlanoClassificacaoModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public bool AreaFim { get; set; }
        public Guid GuidOrganizacao { get; set; }
        public DateTime? Aprovacao { get; set; }
        public DateTime? Publicacao { get; set; }
        public DateTime? InicioVigencia { get; set; }
        public DateTime? FimVigencia { get; set; }

        public OrganizacaoModel Organizacao { get; set; }
        public ICollection<ItemPlanoClassificacaoModel> ItensPlanoClassificacao { get; set; }
    }
}