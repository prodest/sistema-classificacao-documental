using Prodest.Scd.Business.Base;
using Prodest.Scd.Presentation.Base;
using Prodest.Scd.Presentation.ViewModel;
using System.Collections.Generic;

namespace Prodest.Scd.Presentation
{
    public class PlanoClassificacaoService : IPlanoClassificacaoService
    {
        private IPlanoClassificacaoCore _core;

        public PlanoClassificacaoService(IPlanoClassificacaoCore core)
        {
            _core = core;
        }

        public List<PlanoClassificacaoViewModel> Listar()
        {
            _core.Listar();

            return new List<PlanoClassificacaoViewModel>();
        }
    }
}
