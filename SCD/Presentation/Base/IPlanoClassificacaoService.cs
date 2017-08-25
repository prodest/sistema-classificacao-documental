using Prodest.Scd.Presentation.ViewModel;
using System.Collections.Generic;

namespace Prodest.Scd.Presentation.Base
{
    public interface IPlanoClassificacaoService
    {
        PlanoClassificacaoViewModel Insert(PlanoClassificacaoViewModel planoClassificacao);

        List<PlanoClassificacaoViewModel> Search(string guidOrganizacao);

        void Update(PlanoClassificacaoViewModel planoClassificacao);

        void Delete(int id);
    }
}