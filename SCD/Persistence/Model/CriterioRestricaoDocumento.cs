using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Persistence.Model
{
    public partial class CriterioRestricaoDocumento
    {
        public int Id { get; set; }
        public int IdCriterioRestricao { get; set; }
        public int IdDocumento { get; set; }

        public CriterioRestricao IdCriterioRestricaoNavigation { get; set; }
        public Documento IdDocumentoNavigation { get; set; }
    }
}
