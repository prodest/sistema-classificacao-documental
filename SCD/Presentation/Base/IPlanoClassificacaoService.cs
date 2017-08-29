using Prodest.Scd.Presentation.ViewModel;
using System.Collections.Generic;

namespace Prodest.Scd.Presentation.Base
{
    public interface IPlanoClassificacaoService
    {
        PlanoClassificacaoViewModel Search();
        PlanoClassificacaoViewModel Search(Filtro filtro);
        void Delete(int id);
        PlanoClassificacaoEntidade Search(int id);
        void Update(PlanoClassificacaoEntidade entidade);
        PlanoClassificacaoEntidade Create(PlanoClassificacaoEntidade entidade);
    }
}
