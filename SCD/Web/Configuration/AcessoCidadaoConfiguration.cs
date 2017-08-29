using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Web.Configuration
{
    public class AcessoCidadaoConfiguration
    {
        public string Authority { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public List<string> AllowedScopes { get; set; }
        public bool AutomaticAuthenticate { get; set; }
    }
}
