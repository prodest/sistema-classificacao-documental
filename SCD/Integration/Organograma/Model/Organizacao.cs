using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Integration.Organograma.Model
{
    public class OrganogramaOrganizacao
    {
        public Guid Guid { get; set; }
        public string RazaoSocial { get; set; }
        public string Sigla { get; set; }
        public OrganogramaOrganizacao OrganizacaoParent { get; set; }
    }
}
