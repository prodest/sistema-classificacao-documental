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

        public CriterioRestricao CriterioRestricao { get; set; }
        public Documento Documento { get; set; }
    }
}
