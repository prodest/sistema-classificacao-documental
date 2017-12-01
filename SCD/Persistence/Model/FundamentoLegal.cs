using System.Collections.Generic;

namespace Prodest.Scd.Persistence.Model
{
    public partial class FundamentoLegal
    {
        public FundamentoLegal()
        {
            CriteriosRestricao = new HashSet<CriterioRestricao>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdOrganizacao { get; set; }
        public bool Ativo { get; set; }

        public Organizacao Organizacao { get; set; }
        public ICollection<CriterioRestricao> CriteriosRestricao { get; set; }
    }
}