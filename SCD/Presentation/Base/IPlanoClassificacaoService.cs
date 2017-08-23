using Prodest.Scd.Presentation.ViewModel;
using System.Collections.Generic;

namespace Prodest.Scd.Presentation.Base
{
    public interface IPlanoClassificacaoService
    {
        List<PlanoClassificacaoViewModel> Listar();
    }
}
