using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Business.Model
{
    public class FundamentoLegalModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public OrganizacaoModel OrganizacaoPatriarca { get; set; }
        public ICollection<CriterioRestricaoModel> CriteriosRestricao { get; set; }
    }
}